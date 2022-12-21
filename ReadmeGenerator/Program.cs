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

namespace Quera;

public static class Program {
    private static ConfigsModel _configs = null!;
    private static Arguments _arguments = null!;
    private static CacheModel _cache = null!;

    public static Task Main(string[] args) =>
        InnerMainAsync(args)
            .OnSuccess(() => Console.WriteLine("The operation was completed successfully."))
            .OnFail(result => {
                result.Detail?.Log();
                Environment.ExitCode = -1; //for Github action: https://github.com/HamidMolareza/QueraProblems/issues/10
                return result;
            });

    private static async Task<Result> InnerMainAsync(IReadOnlyCollection<string> args) {
        var result1 = await Arguments.Get(args)
            .OnSuccess(arguments => _arguments = arguments)
            .OnSuccess(() => ConfigsService.LoadAsync(_arguments.ProgramDirectory, ConfigsModel.ConfigFile)
                .OnSuccess(configs => _configs = configs))
            .OnSuccess(() => CacheService.LoadAsync(_arguments.ProgramDirectory, _configs.CacheFileName)
                .OnSuccess(cache => {
                    if (cache is null) {
                        Console.WriteLine("Warning! The cache file was not found.");
                        cache = new CacheModel();
                    }

                    _cache = cache;
                }))
            .OnSuccess(() => CollectorService.CollectProblemsAsync(_arguments.SolutionsDirectory, _cache,
                _configs.ProblemUrlFormat, _configs.DelayToRequestQueraInMilliSeconds, _configs.IgnoreSolutions,
                _configs.NumOfTry))
            .OnSuccess(problems =>
                Generator.Generator.GenerateReadmeAsync(problems, _arguments.ProgramDirectory, _configs))
            .OnSuccess(readme => Utility.SaveDataAsync(
                Path.Combine(_arguments.OutputDirectory, _configs.ReadmeFileName), readme, _configs.NumOfTry));

        var result2 = await CacheService.SaveAsync(_cache, _arguments.ProgramDirectory, _configs.CacheFileName)
            .OnSuccess(filePath => Console.WriteLine($"Save cache data in {filePath}."));

        return !result1.IsSuccess ? result1 : result2;
    }
}