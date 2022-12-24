using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;
using Quera.Collector.Models;

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

    public static Task<Result<List<Contributor>>> GetContributorsAsync(string path) =>
        TryExtensions.Try(async () => (await Cli.Wrap("git").WithArguments($"shortlog HEAD -nse {path}")
                .ExecuteBufferedAsync()).StandardOutput)
            .OnSuccess(shortLog => shortLog.Split("\n")
                .Select(line => {
                    var data = line.Trim().Split('\t');
                    if (data.Length != 2)
                        return null;
                    var numOfCommits = Convert.ToInt32(data[0]);

                    data = data[1].Split(" <");
                    if (data.Length != 2)
                        return null;
                    var name = data[0];
                    var email = data[1][..^1];

                    return new Contributor(name: name, email: email, numOfCommits: numOfCommits);
                }).Where(contributor => contributor is not null).ToList())
            .OnFailAddMoreDetails(new {path})!;
}