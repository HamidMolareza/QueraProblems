using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using HtmlAgilityPack;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.SelectResults;
using OnRail.Extensions.Try;
using Quera.Cache;
using Quera.Collector.Models;
using Quera.Configs;
using Quera.ErrorDetails;
using Quera.Helpers;

namespace Quera.Collector;

public static class CollectorService {
    public static Task<Result<List<Problem>>> GetProblemsAsync(string solutionDirectory, int numOfTry) =>
        TryExtensions.Try(() => Directory.GetDirectories(solutionDirectory), numOfTry)
            .OnSuccess(problemDirs => problemDirs.SelectResults(problemDir => GetProblemAsync(problemDir, numOfTry)))
            .OnSuccess(problems => problems.Where(problem => problem is not null).ToList())!;

    private static Task<Result<Problem?>> GetProblemAsync(string problemDir, int numOfTry) =>
        TryExtensions.Try(() => Directory.GetDirectories(problemDir), numOfTry)
            .OnSuccessFailWhen(solutionsDir => !solutionsDir.Any(),
                new ProblemDirectoryIsEmptyError(title: "ProblemDir is not valid",
                    message: "There is no solution in the problem folder."))
            .OnSuccess(GetSolutionsAsync)
            .OnSuccess(solutions => {
                if (!solutions.Any())
                    return null;

                return new Problem {
                    QueraId = new FileInfo(problemDir).Name.ConvertTo<string, long>(),
                    Solutions = solutions,
                    LastSolutionsCommit = solutions.GetLastCommitDateTime()
                };
            }).OnFailAddMoreDetails(new {problemDir});

    private static Task<Result<List<Solution>>> GetSolutionsAsync(string[] languageDirs) =>
        languageDirs.SelectResults(async languageDir =>
            await GetLastCommitDateAsync(languageDir)
                .OnSuccess(lastCommitDate => new Solution {
                    LanguageName = new FileInfo(languageDir).Name,
                    LastCommitDate = lastCommitDate
                }).OnFail(e => {
                    Console.WriteLine($"Warning! Error while get last commit of {languageDir}");
                    Console.WriteLine($"More Data: {e.Detail?.GetHeaderOfError()}");
                    Console.WriteLine("We skipped this error.\n");
                    return Result<Solution>.Ok(new Solution());
                })
        ).OnSuccess(solutions =>
            solutions.Where(solution => !string.IsNullOrEmpty(solution.LanguageName))
                .ToList());

    private static DateTime GetLastCommitDateTime(this IEnumerable<Solution> solutions) =>
        solutions.Select(solution => solution.LastCommitDate).MaxBy(dateTime => dateTime);

    public static Task<Result> AppendProblemData(this StringBuilder source, Problem problem, ConfigsModel configs,
        CacheModel cache) =>
        TryExtensions.Try<string>(() => string.Format(configs.QueraQuestionsUrlFormat, problem.QueraId))
            .OnSuccess(link => GetProblemTitleAsync(problem.QueraId.ToString(), link, cache,
                    configs.DelayToRequestQueraInMilliSeconds, configs.NumOfTry)
                .OnSuccess<(string title, bool wasCache), string>(async result => {
                    if (!result.wasCache)
                        await Task.Delay(configs.DelayToRequestQueraInMilliSeconds);
                    return result.title;
                })
                .OnSuccess(title => {
                    var solutions = problem.Solutions
                        .OrderByDescending(solution => solution.LastCommitDate)
                        .ThenBy(solution => solution.LanguageName)
                        .Select(solution => {
                            var solutionUrl = string.Format(configs.SolutionUrlFormat, problem.QueraId);
                            solutionUrl = Path.Combine(solutionUrl, solution.LanguageName);

                            return $"[{new FileInfo(solution.LanguageName).Name}]({solutionUrl})";
                        });
                    var solutionLinks = string.Join(" - ", solutions);

                    var lastCommitFormatted = problem.LastSolutionsCommit.ToString("dd-MM-yyyy");
                    source.AppendLine(
                        $"| [{problem.QueraId}]({link}) | {title} | {solutionLinks} | {lastCommitFormatted} |");
                }));

    private static async Task<Result<(string title, bool wasCache)>> GetProblemTitleAsync(string queraId,
        string link, CacheModel cache, int delayToRequestQueraInMilliSeconds, int numOfTry) {
        var cachedTitle = cache.ProblemTitles.FirstOrDefault(problems =>
                string.Equals(problems.QueraId, queraId, StringComparison.CurrentCultureIgnoreCase))
            ?.Title;
        if (cachedTitle is not null)
            return Result<(string, bool)>.Ok((cachedTitle, true));


        return await GetProblemTitleFromWebAsync(link, numOfTry, delayToRequestQueraInMilliSeconds)
            .OnSuccess(title => {
                cache.ProblemTitles.Add(new CacheModel.Titles(queraId, title));
                return (title, false);
            });
    }

    private static Task<Result<string>> GetProblemTitleFromWebAsync(string link, int delayToRequestQueraInMilliSeconds,
        int numOfTry,
        bool delayInTooManyRequest = true) =>
        TryExtensions.Try(() => new HtmlWeb())
            .OnSuccess<HtmlWeb, string>(async web => {
                    if (web.StatusCode == HttpStatusCode.TooManyRequests && delayInTooManyRequest) {
                        await Task.Delay(delayToRequestQueraInMilliSeconds);
                        return Result<string>.Fail(new TooManyRequestErrorDetail());
                    }

                    var title = web.Load(link)
                        .DocumentNode
                        .Descendants("div")
                        .Single(div => div.GetAttributeValue("class", "") == "ui segment qu-problem-segment")
                        .Descendants("h1")
                        .First()
                        .InnerText;
                    return Result<string>.Ok(title);
                },
                numOfTry)
            .OnFailAddMoreDetails(new {link, numOfTry});

    private static Task<Result<DateTime>> GetLastCommitDateAsync(string path) =>
        TryExtensions.Try(async () => await (Cli.Wrap("git").WithArguments($"log -1 --date=iso {path}")
                                             | Cli.Wrap("grep").WithArguments("^Date"))
                .ExecuteBufferedAsync())
            .OnSuccess(cmd => {
                var result = cmd.StandardOutput.Remove(0, 8);
                return DateTime.Parse(result[..^7]);
            }).OnFailAddMoreDetails(new {path});
}