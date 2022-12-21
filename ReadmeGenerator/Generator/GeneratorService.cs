using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.Try;
using Quera.Collector.Models;
using Quera.Configs;

namespace Quera.Generator;

public static class Generator {
    public static async Task<string> GenerateReadmeAsync(IEnumerable<Problem> problems, string programDirectory,
        ConfigsModel configs) {
        var problemsList = problems.ToList();
        var readme = new StringBuilder();

        var numOfQuestionsSolved = problemsList.Count;
        readme.AppendLine($"Number of problems solved: **{problemsList.Count}**\n");

        var numOfSolutions = problemsList.Sum(problem => problem.Solutions.Count);
        if (numOfQuestionsSolved != numOfSolutions)
            readme.AppendLine($"Number of solutions: **{numOfSolutions}**\n");

        readme.AppendLine("| Question | Title | Solutions | Last commit |")
            .AppendLine("| ----- | ----- | ----- | ----- |");

        problemsList = problemsList.OrderByDescending(problem => problem.LastSolutionsCommit)
            .ThenBy(problem => problem.QueraId)
            .ToList();

        foreach (var problem in problemsList) {
            Console.Write($"Processing problem {problem.QueraId}... ");

            var result = readme.AppendProblemData(problem, configs.SolutionUrlFormat, configs.ProblemUrlFormat);
            result.OnFailThrowException();

            Console.WriteLine("Done");
        }

        var readmeTemplate =
            await File.ReadAllTextAsync(Path.Combine(programDirectory, configs.ReadmeTemplateName));
        return readmeTemplate.Replace("{__REPLACE_FROM_PROGRAM_0__}", readme.ToString());
    }

    private static Result AppendProblemData(this StringBuilder source,
        Problem problem, string solutionUrlFormat, string problemUrlFormat) =>
        TryExtensions.Try(() => {
            var solutionLinks = problem.Solutions
                .OrderByDescending(solution => solution.LastCommitDate)
                .ThenBy(solution => solution.LanguageName)
                .Select(solution => {
                    var solutionUrl = string.Format(solutionUrlFormat, problem.QueraId);
                    solutionUrl = Path.Combine(solutionUrl, solution.LanguageName);

                    return $"[{new FileInfo(solution.LanguageName).Name}]({solutionUrl})";
                });
            var solutionsSection = string.Join(" - ", solutionLinks);

            var lastCommitFormatted = problem.LastSolutionsCommit.ToString("dd-MM-yyyy");
            var url = string.Format(problemUrlFormat, problem.QueraId);
            source.AppendLine(
                $"| [{problem.QueraId}]({url}) | {problem.QueraTitle} | {solutionsSection} | {lastCommitFormatted} |");
        });
}