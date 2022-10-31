using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using HtmlAgilityPack;
using OnRail;
using OnRail.Extensions.Map;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.SelectResults;
using OnRail.Extensions.Try;
using OnRail.ResultDetails;
using Quera.ErrorDetails;
using Quera.Models;

namespace Quera {
    public static class Program {
        private static Configs _configs = null!;
        private static Arguments _arguments = null!;
        private static Cache _cache = null!;

        public static Task Main(string[] args) =>
            InnerMainAsync(args)
                .OnSuccess(() => Console.WriteLine("The operation was completed successfully."))
                .OnFail(result => {
                    result.Detail?.Log();
                    Environment.ExitCode = -1;
                    return result;
                });

        private static async Task<Result> InnerMainAsync(IReadOnlyCollection<string> args) {
            var result1 = await LoadOrGetArguments(args)
                .OnSuccess(() => LoadConfigFileAsync())
                .OnSuccess(LoadCacheAsync)
                .OnSuccess(GetProblemsAsync)
                .OnSuccess(CreateReadmeAsync)
                .OnSuccess(readme => SaveDataAsync(_arguments.OutputDirectory, readme, _configs.NumOfTry));
            var result2 = await SaveCacheDataAsync();

            return !result1.IsSuccess ? result1 : result2;
        }

        private static Result LoadOrGetArguments(IReadOnlyCollection<string> args) =>
            Arguments.GetProgramDirectory(args.FirstOrDefault())
                .OnSuccess(programDirectory => Arguments.GetOutputDirectory(args.Skip(1).FirstOrDefault())
                    .OnSuccess(outputDirectory => Arguments.GetSolutionsDirectory(args.Skip(2).FirstOrDefault())
                        .OnSuccess(solutionsDirectory => _arguments = new Arguments {
                            ProgramDirectory = programDirectory,
                            OutputDirectory = outputDirectory,
                            SolutionsDirectory = solutionsDirectory
                        })).Map());

        private static Task<Result> LoadConfigFileAsync(int numOfTry = 2) =>
            TryExtensions.Try(() => Path.Combine(_arguments.ProgramDirectory, Configs.ConfigFile))
                .OnSuccess(path => File.ReadAllTextAsync(path), numOfTry)
                .OnSuccess(configFile => JsonSerializer.Deserialize<Configs>(configFile))
                .OnSuccessFailWhen(configsFile => configsFile is null,
                    new ErrorDetail("Can not load configs file", $"Can not map data to {typeof(Configs)} model."))
                .OnSuccess(configs => _configs = configs!)
                .Map();

        private static Task<Result> LoadCacheAsync() =>
            TryExtensions.Try(async () => {
                var filePath = Path.Combine(_arguments.ProgramDirectory, _configs.CacheFileName);
                if (!File.Exists(filePath)) {
                    _cache = new Cache();
                    return Result.Ok();
                }

                var text = await File.ReadAllTextAsync(filePath);
                var cache = JsonSerializer.Deserialize<Cache>(text);
                if (cache is null) {
                    return Result.Fail(new ErrorDetail("Can not load cache file.",
                        $"Can not map {_configs.CacheFileName} file to {typeof(Cache)} model."));
                }

                _cache = cache;
                return Result.Ok();
            });

        private static Task<Result<List<Problem>>> GetProblemsAsync() =>
            TryExtensions.Try(() => Directory.GetDirectories(_arguments.SolutionsDirectory), _configs.NumOfTry)
                .OnSuccess(problemDirs => problemDirs.SelectResults(GetProblemAsync))
                .OnSuccess(problems => problems.Where(problem => problem is not null).ToList())!;

        private static Task<Result<Problem?>> GetProblemAsync(string problemDir) =>
            TryExtensions.Try(() => Directory.GetDirectories(problemDir), _configs.NumOfTry)
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
            solutions.Select(solution => solution.LastCommitDate)
                .OrderByDescending(dateTime => dateTime)
                .First();

        private static Task SaveDataAsync(string outputDir, string readme, int numOfTry) =>
            TryExtensions.Try(() =>
                    File.WriteAllTextAsync(Path.Combine(outputDir, _configs.ReadmeFileName), readme), numOfTry)
                .OnFailAddMoreDetails(new {outputDir});

        private static Task<Result> SaveCacheDataAsync() =>
            TryExtensions.Try(() => {
                var json = JsonSerializer.Serialize(_cache);
                var filePath = Path.Combine(_arguments.ProgramDirectory, _configs.CacheFileName);
                Console.WriteLine($"Save cache data in {filePath}...");
                return File.WriteAllTextAsync(filePath, json);
            });

        private static async Task<string> CreateReadmeAsync(IEnumerable<Problem> problems) {
            var problemsList = problems.ToList();
            var result = new StringBuilder();

            var numOfQuestionsSolved = problemsList.Count;
            result.AppendLine($"Number of questions solved: {problemsList.Count}\n");

            var numOfSolutions = problemsList.Sum(problem => problem.Solutions.Count);
            if (numOfQuestionsSolved != numOfSolutions)
                result.AppendLine($"Number of solutions: {numOfSolutions}\n");

            result.AppendLine("| Question | Title | Solutions | Last commit |")
                .AppendLine("| ----- | ----- | ----- | ----- |");

            problemsList = problemsList.OrderByDescending(problem => problem.LastSolutionsCommit)
                .ThenBy(problem => problem.QueraId)
                .ToList();

            foreach (var problem in problemsList) {
                Console.Write($"Processing problem {problem.QueraId}... ");

                await result.AppendProblemData(problem)
                    .OnFailOperateWhen(failedResult => failedResult.Detail is TooManyRequestErrorDetail,
                        failedResult => {
                            Console.WriteLine(
                                $"Warning: Too many request detected: {nameof(TooManyRequestErrorDetail)} error received from server. It is better if the delay be longer.");
                            return failedResult;
                        })
                    .OnFail(failedResult => failedResult.OnFailThrowException());

                Console.WriteLine("Done");
            }

            var readmeTemplate =
                await File.ReadAllTextAsync(Path.Combine(_arguments.ProgramDirectory, _configs.ReadmeTemplateName));
            return readmeTemplate.Replace("{__REPLACE_FROM_PROGRAM_0__}", result.ToString());
        }

        private static Task<Result> AppendProblemData(this StringBuilder source, Problem problem) =>
            TryExtensions.Try(() => string.Format(_configs.QueraQuestionsUrlFormat, problem.QueraId))
                .OnSuccess(link => GetProblemTitleAsync(problem.QueraId.ToString(), link, _configs.NumOfTry)
                    .OnSuccess(async result => {
                        if (!result.wasCache)
                            await Task.Delay(_configs.DelayToRequestQueraInMilliSeconds);
                        return result.title;
                    })
                    .OnSuccess(title => {
                        var solutions = problem.Solutions
                        .OrderByDescending(solution=> solution.LastCommitDate)
                        .ThenBy(solution=> solution.LanguageName)
                        .Select(solution => {
                            var solutionUrl = string.Format(_configs.SolutionUrlFormat, problem.QueraId);
                            solutionUrl = Path.Combine(solutionUrl, solution.LanguageName);

                            return $"[{new FileInfo(solution.LanguageName).Name}]({solutionUrl})";
                        });
                        var solutionLinks = string.Join(" - ", solutions);

                        var lastCommitFormatted = problem.LastSolutionsCommit.ToString("dd-MM-yyyy");
                        source.AppendLine(
                            $"| [{problem.QueraId}]({link}) | {title} | {solutionLinks} | {lastCommitFormatted} |");
                    }));

        private static async Task<Result<(string title, bool wasCache)>> GetProblemTitleAsync(string queraId,
            string link, int numOfTry,
            bool delayInTooManyRequest = true) {
            var cache = _cache.ProblemTitles.FirstOrDefault(problems =>
                string.Equals(problems.QueraId, queraId, StringComparison.CurrentCultureIgnoreCase));
            if (cache != null) {
                return Result<(string, bool)>.Ok((cache.Title, true));
            }


            return await GetProblemTitleFromWebAsync(link, numOfTry, delayInTooManyRequest)
                .OnSuccess(title => {
                    _cache.ProblemTitles.Add(new Cache.Titles(queraId, title));
                    return (title, false);
                });
        }

        private static Task<Result<string>> GetProblemTitleFromWebAsync(string link, int numOfTry,
            bool delayInTooManyRequest = true) =>
            TryExtensions.Try(() => new HtmlWeb())
                .OnSuccess(async web => {
                        if (web.StatusCode == HttpStatusCode.TooManyRequests && delayInTooManyRequest) {
                            await Task.Delay(_configs.DelayToRequestQueraInMilliSeconds);
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
}