using System;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;

namespace Quera.Helpers;

public static class GitHelper {
    public static Task<Result<DateTime>> GetLastCommitDateAsync(string path) =>
        TryExtensions.Try(
                async () => await (Cli.Wrap("git").WithArguments($"log -1 --date=iso {path}")
                                   | Cli.Wrap("grep").WithArguments("^Date")).ExecuteBufferedAsync())
            .OnSuccess(cmd => {
                var result = cmd.StandardOutput.Remove(0, 8);
                return DateTime.Parse(result[..^7]);
            }).OnFailAddMoreDetails(new {path});
}