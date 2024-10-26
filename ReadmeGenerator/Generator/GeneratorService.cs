using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;
using Quera.Collector.Models;
using Quera.Configs;

namespace Quera.Generator;

public class GeneratorService(AppSettings settings) {
    public Result<StringBuilder> GenerateReadmeSection(List<Problem> problems, int? limit = null) =>
        TryExtensions.Try(() => limit is null or < 1 ? problems : problems.Take((int)limit).ToList())
            .OnSuccess(targetProblems =>
                GenerateReadme(targetProblems, problems.Count, GetAllSolutions(problems))
            );

    private static int GetAllSolutions(IEnumerable<Problem> problems) =>
        problems.Sum(p => p.Solutions.Count);

    private StringBuilder GenerateReadme(List<Problem> problems, int? allProblems = null,
        int? allSolutions = null) {
        var readme = new StringBuilder();

        var numberOfProblemsSolved = allProblems ?? problems.Count;
        readme.AppendLine($"Number of problems solved: **{numberOfProblemsSolved}**\n");

        var numberOfSolutions = allSolutions ?? GetAllSolutions(problems);
        if (numberOfProblemsSolved != numberOfSolutions)
            readme.AppendLine($"Number of solutions: **{numberOfSolutions}**\n");

        readme.AppendLine("<table>")
            .AppendLine("  <tr>")
            .AppendLine("    <th>Question</th>")
            .AppendLine("    <th>Title</th>")
            .AppendLine("    <th>Solutions</th>")
            .AppendLine("    <th>Last commit</th>")
            .AppendLine("    <th>Contributors</th>")
            .AppendLine("  </tr>");

        foreach (var problem in problems) {
            var result = AppendProblemData(readme, problem, settings.SolutionUrlFormat, settings.ProblemUrlFormat);
            result.OnFailThrowException();
        }

        readme.AppendLine("</table>");

        return readme;
    }

    private static Result AppendProblemData(StringBuilder source,
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