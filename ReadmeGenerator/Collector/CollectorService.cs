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

public class CollectorService(AppSettings settings, CacheRepository cache) {
    public Task<Result<List<Problem>>> CollectProblemsAsync() =>
        TryExtensions.Try(() => Directory.GetDirectories(settings.SolutionsPath))
            .OnSuccess(problemDirs => problemDirs.SelectResults(CollectProblemAsync)).OnSuccess(cache.JoinAsync)
            .OnSuccess(async problems => {
                foreach (var problem in problems.Where(problem => problem.QueraTitle is null)) {
                    problem.QueraTitle = await GetProblemTitleAsync(problem.QueraId.ToString());
                }

                return problems;
            });

    private Task<Result<Problem?>> CollectProblemAsync(string problemDir) =>
        GetValidSolutionDirs(problemDir, settings.IgnoreSolutions, settings.NumberOfTry)
            .OnSuccess(CollectSolutionsAsync)
            .OnSuccess(async solutions => {
                if (solutions.Count == 0)
                    return Result<Problem?>.Ok(null);

                var queraId = new FileInfo(problemDir).Name;
                return await GitHelper.GetLastCommitDateAsync(problemDir)
                    .OnSuccess(lastCommitDate => CollectContributorsAsync(problemDir, settings.Users)
                        .OnSuccess(contributors =>
                            Result<Problem?>.Ok(new Problem {
                                QueraId = queraId.ConvertTo<string, long>(),
                                LastSolutionsCommit = lastCommitDate,
                                Solutions = solutions,
                                Contributors = contributors
                            })));
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
        => !solutionName.StartsWith('.') && ignoreSolutions.All(ignoreSolution => ignoreSolution != solutionName);

    private static Task<Result<List<Solution>>> CollectSolutionsAsync(IEnumerable<string> languageDirs) =>
        languageDirs.SelectResults(languageDir =>
            GitHelper.GetLastCommitDateAsync(languageDir)
                .OnSuccess(lastCommitDate => new Solution {
                    LanguageName = new FileInfo(languageDir).Name,
                    LastCommitDate = lastCommitDate
                })
        );

    private async Task<string> GetProblemTitleAsync(string queraId) {
        var url = string.Format(settings.ProblemUrlFormat, queraId);
        var web = new HtmlWeb();
        return (await web.LoadFromWebAsync(url))
            .DocumentNode
            .SelectSingleNode("//aside/div/div[1]/div[1]/div/div[1]/h1")
            .InnerText;
    }
}