using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.Map;
using OnRail.Extensions.OnSuccess;
using Quera.Cache;
using Quera.Collector;
using Quera.Configs;
using Quera.Generator;
using Quera.Helpers;
using Serilog;

namespace Quera;

public class AppRunner(
    AppSettings settings,
    CollectorService collector,
    GeneratorService generator,
    CacheRepository cacheRepository) {
    public async Task<Result> RunAsync() {
        // Collect problems and solutions
        var problemsResult = await collector.CollectProblemsAsync();
        if (!problemsResult.IsSuccess)
            return problemsResult.Map();
        if (problemsResult.Value is null) {
            Log.Warning("No problem and solution found!");
            return Result.Ok();
        }

        Log.Information("{Count} problems found.", problemsResult.Value.Count);

        // Generate readme file and save it
        var result = await problemsResult
            .OnSuccess(generator.GenerateReadmeAsync)
            .OnSuccess(readme => Utility.SaveDataAsync(
                settings.ReadmeOutputPath, readme, settings.NumberOfTry));
        if (!result.IsSuccess) return result;
        Log.Information("The readme file saved in {Path}", settings.ReadmeOutputPath);

        // Cache new data
        return await cacheRepository.SaveNewItemsAsync(problemsResult.Value)
            .OnSuccess(() => Log.Information("The cache ({Path}) updated.", settings.CacheFilePath));
    }
}