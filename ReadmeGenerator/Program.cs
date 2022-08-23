using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using HtmlAgilityPack;
using Quera.Models;

namespace Quera {
    public static class Program {
        private static Configs _configs;

        public static async Task Main(string[] args) {
            _configs = JsonSerializer.Deserialize<Configs>(await File.ReadAllTextAsync(Configs.ConfigFile));

            var outputDir = GetOutputDir(args);

            var problems = await GetProblemsAsync();
            var readme = await CreateReadmeAsync(problems);

            await SaveDataAsync(outputDir, readme);
        }

        private static Task<List<Problem>> GetProblemsAsync() {
            var problemDirs = Directory.GetDirectories("Solutions"); //TODO: Get from input
            return problemDirs.Select(async queraId => {
                var languages = Directory.GetDirectories(queraId);
                var problem = new Problem {
                    QueraId = new FileInfo(queraId).Name,
                    Solutions = await languages.Select(async language => new Solution {
                        LanguageName = language,
                        LastCommitDate = await GetLastCommitDateAsync(language)
                    }).MapTasks()
                };
                problem.LastSolutionsCommit = problem.Solutions.Select(solution => solution.LastCommitDate)
                    .OrderByDescending(dateTime => dateTime)
                    .Last();
                return problem;
            }).MapTasks();
        }

        private static async Task<List<T>> MapTasks<T>(this IEnumerable<Task<T>> source) {
            var result = new List<T>();
            foreach (var task in source)
                result.Add(await task);

            return result;
        }

        private static Task SaveDataAsync(string outputDir, string readme) =>
            File.WriteAllTextAsync(Path.Combine(outputDir, _configs.ReadmeFileName), readme);

        private static string GetOutputDir(IReadOnlyList<string> args) {
            if (args.Any())
                return args[0];

            do {
                try {
                    Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
                    Console.Write("Output directory: ");
                    var outputDir = Console.ReadLine();

                    return outputDir;
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (true);
        }

        private static async Task<string> CreateReadmeAsync(IEnumerable<Problem> problems) {
            var problemsList = problems.ToList();
            var result = new StringBuilder();

            var numOfQuestionsSolved = problemsList.Count;
            result.AppendLine($"Number of questions solved: {problemsList.Count}\n");

            var numOfSolutions = problemsList.Sum(problem => problem.Solutions.Count);
            if (numOfQuestionsSolved != numOfSolutions)
                result.AppendLine($"Number of solutions: {numOfSolutions}\n");

            result.AppendLine("| Question | Title | Solutions | Last commit |")
                .AppendLine("| ----- | ----- | ----- | ----- |");

            foreach (var problem in problemsList.OrderByDescending(problem => problem.LastSolutionsCommit)) {
                Console.Write($"Processing problem {problem.QueraId}...");

                var link = string.Format(_configs.QueraQuestionsUrlFormat, problem.QueraId);
                var title = GetQuestionTitle(link);

                var solutions = problem.Solutions.Select(solution => {
                    var solutionUrl = string.Format(_configs.SolutionUrlFormat, problem.QueraId);
                    solutionUrl = Path.Combine(solutionUrl, solution.LanguageName);

                    return $"[{new FileInfo(solution.LanguageName).Name}]({solutionUrl})";
                });
                var solutionLinks = string.Join(" | ", solutions);

                result.AppendLine(
                    $"| [{problem.QueraId}]({link}) | {title} | {solutionLinks} | {problem.LastSolutionsCommit} |");

                Console.WriteLine("Done");
                await Task.Delay(_configs.DelayToRequestQueraInMilliSeconds);
            }

            var readmeTemplate = await File.ReadAllTextAsync(_configs.ReadmeTemplateName);
            return readmeTemplate.Replace("{__REPLACE_FROM_PROGRAM_0__}", result.ToString());
        }

        private static string GetQuestionTitle(string link) {
            var web = new HtmlWeb();
            var doc = web.Load(link);

            return doc.DocumentNode
                .Descendants("div")
                .Single(div => div.GetAttributeValue("class", "") == "ui segment qu-problem-segment")
                .Descendants("h1")
                .First()
                .InnerText;
        }

        private static async Task<DateTime> GetLastCommitDateAsync(string path) {
            var cmd = await (Cli.Wrap("git").WithArguments($"log -1 --date=iso {path}")
                             | Cli.Wrap("grep").WithArguments("^Date"))
                .ExecuteBufferedAsync();

            var result = cmd.StandardOutput.Remove(0, 8);
            return DateTime.Parse(result[..^7]);
        }
    }
}