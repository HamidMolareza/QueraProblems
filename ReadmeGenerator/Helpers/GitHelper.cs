using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;
using Quera.Collector.Models;

namespace Quera.Helpers;

public static class GitHelper {
    public static Task<Result<string>> MakeDirectorySafe(string path, string? workingDirectory = null) =>
        TryExtensions.Try(() => {
            if (workingDirectory is not null)
                path = Path.Combine(workingDirectory, path);
            var fullPath = Path.GetFullPath(path);
            return fullPath;
        }).OnSuccess(fullPath =>
            BashExecutor.RunCommandAsync($"git config --global --add safe.directory {fullPath}")
                .OnSuccess(result => $"'{fullPath}' path was added to git safe path. {result}")
        );

    public static Task<Result<DateTime>> GetLastCommitDateAsync(string path, string? workingDirectory = null) =>
        BashExecutor.RunCommandAsync($"git log -1 --date=iso --pretty=format:'%cd' {path}", workingDirectory)
            .OnSuccess(dateStr => {
                // ISO 8601 format: "yyyy-MM-dd HH:mm:ss zzz"
                if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss zzz",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var dateTime)) {
                    return dateTime;
                }

                throw new FormatException($"Failed to parse date: {dateStr}");
            })
            .OnFailAddMoreDetails(new { path, workingDirectory });

    public static Task<Result<List<Contributor>>> GetContributorsAsync(string path, string? workingDirectory = null) =>
        BashExecutor.RunCommandAsync($"git shortlog HEAD -nse {path}", workingDirectory)
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
            .OnFailAddMoreDetails(new { path, workingDirectory })!;
}