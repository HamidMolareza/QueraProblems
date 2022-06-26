using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using HtmlAgilityPack;
using Quera.Models;

namespace Quera {
    public static class Program {
        public static async Task Main() {
            var outputDir = GetOutputDir();

            var branches = await GetBranchesAsync();
            var readme = await CreateReadmeAsync(branches);

            await SaveDataAsync(outputDir, readme);
        }

        private static Task SaveDataAsync(string outputDir, string readme) =>
            File.WriteAllTextAsync(Path.Combine(outputDir, Configs.ReadmeFileName), readme);

        private static string GetOutputDir() {
            do {
                try {
                    Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
                    Console.Write("Output directory: ");
                    var outputDir = Console.ReadLine();
                    if (!Directory.Exists(outputDir)) {
                        Directory.CreateDirectory(outputDir!);
                    }

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

        private static async Task<string> CreateReadmeAsync(IEnumerable<GitBranch> branches) {
            var result = new StringBuilder();

            foreach (var branch in branches.OrderByDescending(branch => branch.LastCommitDate)) {
                Console.Write($"Processing branch {branch.Name}...");

                var link = string.Format(Configs.QueraQuestionsUrlFormat, branch.Name);
                var title = GetQuestionTitle(link);
                var solutionUrl = string.Format(Configs.SolutionUrlFormat, branch.Name);

                result.AppendLine($"### {branch.Name}")
                    .AppendLine($"Title: {title}\n")
                    .AppendLine($"Last commit: {branch.LastCommitDate}\n")
                    .AppendLine($"Question Link: {link}\n")
                    .AppendLine($"Solution: [{branch.Name}]({solutionUrl})")
                    .AppendLine();

                Console.WriteLine("Done");
                await Task.Delay(Configs.DelayToRequestQueraInMilliSeconds);
            }

            return Configs.ReadmeTemplate.Replace("{__REPLACE_FROM_PROGRAM_0__}", result.ToString());
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

        private static async Task<List<GitBranch>> GetBranchesAsync() {
            var cmd = await Cli.Wrap("git")
                .WithArguments("branch")
                .ExecuteBufferedAsync();

            var resultTasks = cmd.StandardOutput
                .Split('\n')
                .Where(branch => !string.IsNullOrWhiteSpace(branch))
                .Select(ParseBranchName)
                .ToArray();

            Task.WaitAll(resultTasks);
            return resultTasks.Select(task => task.Result)
                .Where(branch => branch.Name != "master"
                                 && branch.Name != "Utility")
                .ToList();
        }

        private static async Task<DateTime> GetLastBranchCommitDateAsync(string branchName) {
            var cmd = await (Cli.Wrap("git").WithArguments($"log -1 --date=iso {branchName}")
                             | Cli.Wrap("grep").WithArguments("^Date"))
                .ExecuteBufferedAsync();

            var result = cmd.StandardOutput.Remove(0, 8);
            return DateTime.Parse(result[..^7]);
        }

        private static async Task<GitBranch> ParseBranchName(this string branch) {
            var result = new GitBranch(branch, false);
            if (branch.StartsWith("*")) {
                result.Name = result.Name.Remove(0, 1);
                result.IsCurrentBranch = true;
            }

            result.Name = result.Name.Trim();
            result.LastCommitDate = await GetLastBranchCommitDateAsync(result.Name);
            return result;
        }
    }
}