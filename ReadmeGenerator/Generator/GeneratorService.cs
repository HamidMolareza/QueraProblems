using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnRail.Extensions.OnFail;
using Quera.Cache;
using Quera.Collector;
using Quera.Collector.Models;
using Quera.Configs;
using Quera.ErrorDetails;

namespace Quera.Generator;

public static class Generator {
    public static async Task<string> GenerateReadmeAsync(IEnumerable<Problem> problems, string programDirectory,
        ConfigsModel configs, CacheModel cache) {
        var problemsList = problems.ToList();
        var result = new StringBuilder();

        var numOfQuestionsSolved = problemsList.Count;
        result.AppendLine($"Number of questions solved: {problemsList.Count}\n");

        var numOfSolutions = problemsList.Sum(problem => problem.Solutions.Count);
        if (numOfQuestionsSolved != numOfSolutions)
            result.AppendLine($"Number of solutions: {numOfSolutions}\n");

        result.AppendLine("| Question | Title | Solutions | Last commit |")
            .AppendLine("| ----- | ----- | ----- | ----- |");

        problemsList = problemsList.OrderByDescending(problem => problem.LastSolutionsCommit)
            .ThenBy(problem => problem.QueraId)
            .ToList();

        foreach (var problem in problemsList) {
            Console.Write($"Processing problem {problem.QueraId}... ");

            await result.AppendProblemData(problem, configs, cache)
                .OnFailOperateWhen(failedResult => failedResult.Detail is TooManyRequestErrorDetail,
                    failedResult => {
                        Console.WriteLine(
                            $"Warning: Too many request detected: {nameof(TooManyRequestErrorDetail)} error received from server. It is better if the delay be longer.");
                        return failedResult;
                    })
                .OnFail(failedResult => failedResult.OnFailThrowException());

            Console.WriteLine("Done");
        }

        var readmeTemplate =
            await File.ReadAllTextAsync(Path.Combine(programDirectory, configs.ReadmeTemplateName));
        return readmeTemplate.Replace("{__REPLACE_FROM_PROGRAM_0__}", result.ToString());
    }
}