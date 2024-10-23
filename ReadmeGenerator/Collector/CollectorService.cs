using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.SelectResults;
using OnRail.Extensions.Try;
using Quera.Cache;
using Quera.Collector.Models;
using Quera.Configs;
using Quera.Helpers;

namespace Quera.Collector;

public static class CollectorService {
    public static Task<Result<List<Problem>>> CollectProblemsAsync(string solutionsDirectory, CacheModel cache,
        string problemUrlFormat, int delayToRequestQueraInMilliSeconds,
        IEnumerable<string> ignoreSolutions, IEnumerable<UserModel> users, int numOfTry) =>
        TryExtensions.Try(() => Directory.GetDirectories(solutionsDirectory), numOfTry)
            .OnSuccess(problemDirs =>
                problemDirs.SelectResults(problemDir =>
                        CollectProblemAsync(problemDir, cache, delayToRequestQueraInMilliSeconds, problemUrlFormat,
                            ignoreSolutions, users, numOfTry))
                    .OnSuccess(problems => problems.Where(problem => problem is not null).ToList())
            )!;

    private static Task<Result<Problem?>> CollectProblemAsync(string problemDir, CacheModel cache,
        int delayToRequestQueraInMilliSeconds, string problemUrlFormat, IEnumerable<string> ignoreSolutions,
        IEnumerable<UserModel> users, int numOfTry) =>
        GetValidSolutionDirs(problemDir, ignoreSolutions, numOfTry)
            .OnSuccess(CollectSolutionsAsync)
            .OnSuccess(async solutions => {
                if (!solutions.Any())
                    return Result<Problem?>.Ok(null);

                var queraId = new FileInfo(problemDir).Name;
                return await GetProblemTitleAsync(queraId, cache, problemUrlFormat, numOfTry)
                    .OnSuccess(async result => {
                        if (!result.wasCache)
                            await Task.Delay(delayToRequestQueraInMilliSeconds);

                        return await GitHelper.GetLastCommitDateAsync(problemDir)
                            .OnSuccess(lastCommitDate => CollectContributorsAsync(problemDir, users)
                                .OnSuccess(contributors => Result<Problem?>.Ok(new Problem(
                                    queraId: queraId.ConvertTo<string, long>(),
                                    queraTitle: result.title,
                                    lastSolutionsCommit: lastCommitDate
                                ) {
                                    Solutions = solutions,
                                    Contributors = contributors
                                })));
                    });
            }).OnFailAddMoreDetails(new { problemDir });

    private static Task<Result<List<Contributor>>>
        CollectContributorsAsync(string problemDir, IEnumerable<UserModel> users) =>
        GitHelper.GetContributorsAsync(problemDir)
            .OnSuccess(contributors => contributors.Select(
                contributor => {
                    var user = users.SingleOrDefault(user =>
                        string.Equals(user.Email, contributor.Email, StringComparison.CurrentCultureIgnoreCase));
                    if (user is null)
                        return contributor;
                    if (user.Ignore)
                        return null;
                    contributor.AvatarUrl = user.AvatarUrl;
                    contributor.ProfileUrl = user.ProfileUrl;
                    return contributor;
                }).Where(contributor => contributor is not null).ToList())!;

    private static Result<IEnumerable<string>> GetValidSolutionDirs(string problemDir,
        IEnumerable<string> ignoreSolutions, int numOfTry) =>
        TryExtensions.Try(() => Directory.GetDirectories(problemDir), numOfTry)
            .OnSuccess(solutions =>
                solutions.Where(solution => IsSolutionNameValid(new FileInfo(solution).Name, ignoreSolutions)));

    private static bool IsSolutionNameValid(string solutionName, IEnumerable<string> ignoreSolutions)
        => !solutionName.StartsWith(".") && ignoreSolutions.All(ignoreSolution => ignoreSolution != solutionName);

    private static Task<Result<List<Solution>>> CollectSolutionsAsync(IEnumerable<string> languageDirs) =>
        languageDirs.SelectResults(languageDir =>
            GitHelper.GetLastCommitDateAsync(languageDir)
                .OnSuccess(lastCommitDate => new Solution {
                    LanguageName = new FileInfo(languageDir).Name,
                    LastCommitDate = lastCommitDate
                })
        );

    private static async Task<Result<(string title, bool wasCache)>> GetProblemTitleAsync(string queraId,
        CacheModel cache, string problemUrlFormat, int numOfTry) {
        var cachedTitle = cache.ProblemTitles.FirstOrDefault(problems =>
            string.Equals(problems.QueraId, queraId, StringComparison.CurrentCultureIgnoreCase)
        )?.Title;
        if (cachedTitle is not null)
            return Result<(string, bool)>.Ok((cachedTitle, true));


        return await GetProblemTitleFromWebAsync(queraId, problemUrlFormat, numOfTry)
            .OnSuccess(title => {
                cache.ProblemTitles.Add(new CacheModel.Titles(queraId, title));
                return (title, false);
            });
    }

    private static Task<Result<string>> GetProblemTitleFromWebAsync(string queraId, string problemUrlFormat,
        int numOfTry) =>
        TryExtensions.Try(() => {
                var url = string.Format(problemUrlFormat, queraId);
                var web = new HtmlWeb();
                return web.LoadFromWebAsync(url);
            }, numOfTry)
            .OnSuccess(html => html
                .DocumentNode
                .SelectSingleNode("//aside/div/div[1]/div[1]/div/div[1]/h1")
                .InnerText
            )
            .OnFailAddMoreDetails(new {
                queraId, problemUrlFormat
            });
}