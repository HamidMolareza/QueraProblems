using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;
using OnRail.ResultDetails;
using Quera.Collector.Models;
using Quera.Configs;

namespace Quera.Cache;

public class CacheRepository(AppSettings settings) {
    public Task<Result> SaveNewItemsAsync(List<Problem> problems) =>
        TryExtensions.Try(() => problems
                .Select(problem => new CacheModel.QueraProblem(problem.QueraId.ToString(), problem.QueraTitle!))
                .ToList())
            .OnSuccess(queraProblems =>
                SaveAsync(new CacheModel { QueraProblems = queraProblems }, settings.CacheFilePath));

    public Task<Result<List<Problem>>> JoinAsync(List<Problem?> problems) =>
        LoadAsync(settings.CacheFilePath)
            .OnSuccess(cache => {
                if (cache?.QueraProblems.Count == 0)
                    return problems.Where(p => p is not null).ToList();
                var query = from problem in problems
                    join cacheProblem in cache!.QueraProblems on problem.QueraId.ToString() equals cacheProblem.QueraId
                        into g
                    from qProblem in g.DefaultIfEmpty()
                    select new Problem {
                        QueraId = problem.QueraId,
                        LastSolutionsCommit = problem.LastSolutionsCommit,
                        Contributors = problem.Contributors,
                        Solutions = problem.Solutions,
                        QueraTitle = qProblem.Title
                    };
                return query.ToList();
            })!;

    private static Task<Result<CacheModel?>> LoadAsync(string cacheFilePath) =>
        TryExtensions.Try(async () => {
            if (!File.Exists(cacheFilePath))
                return Result<CacheModel?>.Ok(null);

            var text = await File.ReadAllTextAsync(cacheFilePath);
            var cache = JsonSerializer.Deserialize<CacheModel>(text);
            if (cache is null) {
                return Result<CacheModel?>.Fail(new ErrorDetail("Can not load cache file.",
                    $"Can not map {cacheFilePath} file to {typeof(CacheModel)} model."));
            }

            return Result<CacheModel?>.Ok(cache);
        });

    private static Task<Result> SaveAsync(CacheModel cache, string cacheFilePath) =>
        TryExtensions.Try(() => {
            var json = JsonSerializer.Serialize(cache);
            return File.WriteAllTextAsync(cacheFilePath, json);
        });
}