using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;
using OnRail.ResultDetails;

namespace Quera.Configs;

public static class ConfigsService {
    public static Task<Result<ConfigsModel>>
        LoadAsync(string programDirectory, string configFileName, int numOfTry = 2) =>
        TryExtensions.Try(() => Path.Combine(programDirectory, configFileName))
            .OnSuccess(path => File.ReadAllTextAsync(path), numOfTry)
            .OnSuccess(configFile => JsonSerializer.Deserialize<ConfigsModel>(configFile))
            .OnSuccessFailWhen(configsFile => configsFile is null,
                new ErrorDetail("Can not load configs file", $"Can not map data to {typeof(ConfigsModel)} model."))!;
}