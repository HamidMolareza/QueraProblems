using System.Threading.Tasks;
using OnRail;
using OnRail.ResultDetails;

namespace Quera.Helpers;

using System;
using System.Diagnostics;

public static class BashExecutor {
    public static async Task<Result<string>> RunCommandAsync(string command, string? workingDirectory = null) {
        try {
            var process = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            if (workingDirectory is not null)
                process.StartInfo.WorkingDirectory = workingDirectory;

            process.Start();

            var result = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            return string.IsNullOrEmpty(error)
                ? Result<string>.Ok(result)
                : Result<string>.Fail(
                    new ErrorDetail(title: "Bash execute failed.", message: $"Command Error: {error}"));
        }
        catch (Exception ex) {
            return Result<string>.Fail(new ErrorDetail(title: "Bash execute failed.", exception: ex));
        }
    }
}