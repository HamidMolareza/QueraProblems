using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;
using OnRail.ResultDetails;
using Quera.Collector.Models;
using Quera.Configs;
using Serilog;

namespace Quera.Cache;

public class CacheRepository(AppSettings settings) {
    public Task<Result> SaveNewItemsAsync(List<Problem> problems) =>
        TryExtensions.Try(() => problems
                .Select(problem => new CacheModel.QueraProblem(problem.QueraId.ToString(), problem.QueraTitle!))
                .ToList())
            .OnSuccess(queraProblems =>
                SaveAsync(new CacheModel { QueraProblems = queraProblems }, settings.CacheFilePath))
            .OnSuccessTee(()=> Log.Information("{Count} problems cached.", problems.Count))
            .OnFailAddMoreDetails(new { ProblemsCount = problems.Count });

    public Task<Result<List<Problem>>> JoinAsync(List<Problem?> problems) =>
        LoadAsync(settings.CacheFilePath)
            .OnSuccess(cache => {
                if (cache is null || cache.QueraProblems.Count == 0) {
                    Log.Warning("No cache data for joining to problems.");
                    return problems.Where(p => p is not null).ToList();
                }
                Log.Debug("{CacheCount} cache data loaded for joining to {ProblemCount} problems", cache.QueraProblems.Count, problems.Count);
                
                var query = from problem in problems
                    join cacheProblem in cache.QueraProblems on problem.QueraId.ToString() equals cacheProblem.QueraId
                        into g
                    from qProblem in g.DefaultIfEmpty()
                    select new Problem {
                        QueraId = problem.QueraId,
                        LastSolutionsCommit = problem.LastSolutionsCommit,
                        Contributors = problem.Contributors,
                        Solutions = problem.Solutions,
                        QueraTitle = qProblem?.Title
                    };
                return query.ToList();
            }).OnFailAddMoreDetails(new { ProblemsCount = problems.Count })!;

    private static Task<Result<CacheModel?>> LoadAsync(string cacheFilePath) =>
        TryExtensions.Try(async () => {
            if (!File.Exists(cacheFilePath)) {
                Log.Warning("Cache file in '{Path}' not found.", cacheFilePath);
                return Result<CacheModel?>.Ok(null);
            }

            var text = await File.ReadAllTextAsync(cacheFilePath);
            var cache = JsonSerializer.Deserialize<CacheModel>(text);
            if (cache is null) {
                return Result<CacheModel?>.Fail(new ErrorDetail("Can not load cache file.",
                    $"Can not map {cacheFilePath} file to {typeof(CacheModel)} model."));
            }
            
            Log.Debug("Cache deserialized and {count} problems found.", cache.QueraProblems.Count);

            return Result<CacheModel?>.Ok(cache);
        }).OnFailAddMoreDetails(new { cacheFilePath });

    private static Task<Result> SaveAsync(CacheModel cache, string cacheFilePath) =>
        TryExtensions.Try(() => {
            var json = JsonSerializer.Serialize(cache);
            return File.WriteAllTextAsync(cacheFilePath, json);
        }).OnFailAddMoreDetails(new { cacheFilePath, QueraProblemsCount = cache.QueraProblems.Count });
}