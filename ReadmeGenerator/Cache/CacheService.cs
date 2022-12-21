using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.Try;
using OnRail.ResultDetails;

namespace Quera.Cache;

public static class CacheService {
    public static Task<Result<CacheModel?>> LoadAsync(string programDirectory, string cacheFileName) =>
        TryExtensions.Try(async () => {
            var filePath = Path.Combine(programDirectory, cacheFileName);
            if (!File.Exists(filePath))
                return Result<CacheModel?>.Ok(null);

            var text = await File.ReadAllTextAsync(filePath);
            var cache = JsonSerializer.Deserialize<CacheModel>(text);
            if (cache is null) {
                return Result<CacheModel?>.Fail(new ErrorDetail("Can not load cache file.",
                    $"Can not map {cacheFileName} file to {typeof(CacheModel)} model."));
            }

            return Result<CacheModel?>.Ok(cache);
        });

    /// <returns>File path</returns>
    public static Task<Result<string>> SaveAsync(CacheModel cache, string programDirectory, string cacheFileName) =>
        TryExtensions.Try(async () => {
            var json = JsonSerializer.Serialize(cache);
            var filePath = Path.Combine(programDirectory, cacheFileName);
            await File.WriteAllTextAsync(filePath, json);
            return filePath;
        });
}