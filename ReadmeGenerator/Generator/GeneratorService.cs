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

        problemsList = problemsList.OrderByDescending(problem => problem.LastSolutionsCommit)
            .ThenBy(problem => problem.QueraId)
            .ToList();

        readme.AppendLine("<table>")
            .AppendLine("  <tr>")
            .AppendLine("    <th>Question</th>")
            .AppendLine("    <th>Title</th>")
            .AppendLine("    <th>Solutions</th>")
            .AppendLine("    <th>Last commit</th>")
            .AppendLine("    <th>Contributors</th>")
            .AppendLine("  </tr>");

        foreach (var problem in problemsList) {
            Console.Write($"Processing problem {problem.QueraId}... ");

            var result = readme.AppendProblemData(problem, configs.SolutionUrlFormat, configs.ProblemUrlFormat);
            result.OnFailThrowException();

            Console.WriteLine("Done");
        }

        readme.AppendLine("</table>");

        var readmeTemplate =
            await File.ReadAllTextAsync(Path.Combine(programDirectory, configs.ReadmeTemplateName));
        return readmeTemplate.Replace("{__REPLACE_WITH_PROGRAM_0__}", readme.ToString());
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

                    return $"<a href=\"{solutionUrl}\">{new FileInfo(solution.LanguageName).Name}</a>";
                });
            var solutionsSection = string.Join(" - ", solutionLinks);

            var lastCommitFormatted = problem.LastSolutionsCommit.ToString("dd-MM-yyyy");
            var url = string.Format(problemUrlFormat, problem.QueraId);

            var contributorLinks = problem.Contributors
                .OrderByDescending(contributor => contributor.NumOfCommits)
                .Select(contributor =>
                    $"<a href=\"{contributor.ProfileUrl}\" title=\"{contributor.NumOfCommits} commits\"><img src=\"{contributor.AvatarUrl}\" alt=\"{contributor.Name}\" style=\"border-radius:100%\" width=\"32px\" height=\"32px\"></a>");
            var contributorDiv =
                $"<div style=\"display: flex; flex-direction: row; gap: 2px;\">{string.Join(" ", contributorLinks)}</div>";

            var questionLink = $"<a href=\"{url}\">{problem.QueraId}</a>";

            source.AppendLine("  <tr>")
                .AppendLine($"    <td>{questionLink}</td>")
                .AppendLine($"    <td>{problem.QueraTitle}</td>")
                .AppendLine($"    <td>{solutionsSection}</td>")
                .AppendLine($"    <td>{lastCommitFormatted}</td>")
                .AppendLine($"    <td>{contributorDiv}</td>")
                .AppendLine("  </tr>");
        });
}