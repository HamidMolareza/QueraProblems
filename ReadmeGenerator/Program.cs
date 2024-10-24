using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using Quera.Cache;
using Quera.Collector;
using Quera.Configs;
using Quera.Helpers;
using Serilog;

namespace Quera;

public static class Program {
    private static ConfigsModel _configs = null!;
    private static Arguments _arguments = null!;
    private static CacheModel _cache = null!;

    public static Task Main(string[] args) {
        ConfigSerilog();

        return InnerMainAsync(args)
            .OnSuccess(() => Log.Information("The operation was completed successfully."))
            .OnFail(result => {
                Log.Error("{result}", result.ToStr());
                Environment.ExitCode = -1; //for Github action: https://github.com/HamidMolareza/QueraProblems/issues/10
                return result;
            });
    }

    private static void ConfigSerilog() {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }

    private static async Task<Result> InnerMainAsync(IReadOnlyCollection<string> args) {
        var result1 = await Arguments.Get(args)
            .OnSuccess(arguments => _arguments = arguments)
            .OnSuccess(() => ConfigsService.LoadAsync(_arguments.ProgramDirectory, ConfigsModel.ConfigFile)
                .OnSuccess(configs => _configs = configs))
            .OnSuccess(() => CacheService.LoadAsync(_arguments.ProgramDirectory, _configs.CacheFileName)
                .OnSuccess(cache => {
                    if (cache is null) {
                        Log.Warning("Warning! The cache file was not found.");
                        cache = new CacheModel();
                    }

                    _cache = cache;
                }))
            .OnSuccess(() => CollectorService.CollectProblemsAsync(_arguments.SolutionsDirectory, _cache,
                _configs.ProblemUrlFormat, _configs.DelayToRequestQueraInMilliSeconds, _configs.IgnoreSolutions,
                _configs.Users, _configs.NumOfTry))
            .OnSuccess(problems =>
                Generator.Generator.GenerateReadmeAsync(problems, _arguments.ProgramDirectory, _configs))
            .OnSuccess(readme => Utility.SaveDataAsync(
                Path.Combine(_arguments.OutputDirectory, _configs.ReadmeFileName), readme, _configs.NumOfTry));

        var result2 = await CacheService.SaveAsync(_cache, _arguments.ProgramDirectory, _configs.CacheFileName)
            .OnSuccess(filePath => Log.Information("Save cache data in {filePath}.", filePath));

        return !result1.IsSuccess ? result1 : result2;
    }
}