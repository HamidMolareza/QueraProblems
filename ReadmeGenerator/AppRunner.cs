using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.Map;
using OnRail.Extensions.OnSuccess;
using OnRail.ResultDetails.Errors;
using Quera.Cache;
using Quera.Collector;
using Quera.Collector.Models;
using Quera.Configs;
using Quera.Crawler;
using Quera.Generator;
using Quera.Helpers;
using Serilog;

namespace Quera;

public class AppRunner(
    AppSettings settings,
    CollectorService collector,
    GeneratorService generator,
    CacheRepository cacheRepository,
    CrawlerService crawler
) {
    public async Task<Result> RunAsync() {
        if (!EnsureInputsAreValid(out var validationResult))
            return validationResult;
        Log.Debug("App setting values checked.");

        // Collect problems and solutions
        var problemsResult = await collector.CollectProblemsFromDiskAsync();
        if (!problemsResult.IsSuccess)
            return problemsResult.Map();
        if (problemsResult.Value is null) {
            Log.Warning("No problem and solution found!");
            return Result.Ok();
        }

        Log.Information("{Count} problems collected from disk.", problemsResult.Value.Count);

        // Crawl new problems
        await foreach (var newProblem in crawler.CompleteProblemTitlesAsync(problemsResult.Value)) {
            await cacheRepository.SaveAsync(new CacheProblem {
                Id = newProblem.QueraId.ToString(),
                Title = newProblem.QueraTitle!
            });
            Log.Debug("{QueraId} cached.", newProblem.QueraId);
        }

        // Order problems
        var problems = problemsResult.Value
            .OrderByDescending(problem => problem.LastSolutionsCommit)
            .ThenBy(problem => problem.QueraId)
            .ToList();

        // Generate readme files and save it
        return await GenerateReadmeFiles(problems);
    }

    private async Task<Result> GenerateReadmeFiles(List<Problem> problems) {
        // MainPage Readme
        var mainPageReadmeResult = generator.GenerateReadmeSection(problems, settings.MainPageLimit)
            .OnSuccessOperateWhen(() => !string.IsNullOrWhiteSpace(settings.MainPageFooter),
                section => section.AppendLine($"\n{settings.MainPageFooter}"))
            .OnSuccess(section =>
                section.ToString().UseTemplateAsync(settings.ReadmeTemplatePath, "{__REPLACE_WITH_PROGRAM_0__}"))
            .OnSuccess(readme => Utility.SaveDataAsync(
                settings.ReadmeOutputPath, readme, settings.NumberOfTry));

        //CompleteList Readme
        var completeListReadmeResult = generator.GenerateReadmeSection(problems)
            .OnSuccess(section =>
                section.ToString().UseTemplateAsync(settings.CompleteListTemplatePath, "{__REPLACE_WITH_PROGRAM_0__}"))
            .OnSuccess(readme => Utility.SaveDataAsync(
                settings.CompleteListOutputPath, readme, settings.NumberOfTry));

        // Wait to all tasks done
        Task.WaitAll(mainPageReadmeResult, completeListReadmeResult);

        // Return combined results
        return ResultHelpers.CombineResults(await mainPageReadmeResult, await completeListReadmeResult);
    }

    private bool EnsureInputsAreValid(out Result result) {
        if (!File.Exists(settings.ReadmeTemplatePath)) {
            result = Result.Fail(
                new ValidationError(
                    message: $"The readme template path is not valid. ({settings.ReadmeTemplatePath})"));
            return false;
        }

        if (!Directory.Exists(settings.SolutionsPath)) {
            result = Result.Fail(
                new ValidationError(
                    message: $"The solutions directory is not valid. ({settings.SolutionsPath})"));
            return false;
        }

        result = Result.Ok();
        return true;
    }
}