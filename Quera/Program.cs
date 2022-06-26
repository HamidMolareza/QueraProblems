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

            var readme = (await GetBranches())
                .Where(branch => branch.Name != "master"
                                 && branch.Name != "Utility")
                .CreateReadme();

            await File.WriteAllTextAsync(Path.Combine(outputDir, Configs.ReadmeFileName), readme);
        }

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

        private static string CreateReadme(this IEnumerable<GitBranch> branches) {
            var result = new StringBuilder();

            foreach (var branch in branches) {
                Console.Write($"Processing branch {branch.Name}...");

                var link = string.Format(Configs.QueraQuestionsUrlFormat, branch.Name);
                var title = GetQuestionTitle(link);
                var branchUrl = string.Format(Configs.GithubBranchUrlFormat, branch.Name);
                
                result.AppendLine($"### {branch.Name}")
                    .AppendLine($"Title: {title}\n")
                    .AppendLine($"Question Link: {link}")
                    .AppendLine()
                    .AppendLine($"Solution: [{branch.Name}]({branchUrl})")
                    .AppendLine();

                Console.WriteLine("Done");
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

        private static async Task<List<GitBranch>> GetBranches() {
            var cmd = await Cli.Wrap("git")
                .WithArguments("branch")
                .ExecuteBufferedAsync();

            return cmd.StandardOutput
                .Split('\n')
                .Where(branch => !string.IsNullOrWhiteSpace(branch))
                .Select(ParseBranchName)
                .ToList();
        }

        private static GitBranch ParseBranchName(this string branch) {
            var result = new GitBranch(branch, false);
            if (branch.StartsWith("*")) {
                result.Name = result.Name.Remove(0, 1);
                result.IsCurrentBranch = true;
            }

            result.Name = result.Name.Trim();
            return result;
        }
    }
}