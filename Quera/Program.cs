using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using Quera.Models;

namespace Quera {
    public static class Program {
        public static async Task Main() {
            var branches = (await GetBranches())
                .Where(branch => branch.Name != "master"
                                 && branch.Name != "Utility");
        }
        
        private static async Task<List<GitBranch>> GetBranches() {
            var cmd = await Cli.Wrap("git")
                .WithArguments("branch")
                .ExecuteBufferedAsync();

            return cmd.StandardOutput
                .Split('\n')
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