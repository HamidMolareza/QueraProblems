using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.Try;
using OnRail.ResultDetails;

namespace Quera.Helpers;

using System.Diagnostics;

public static class BashExecutor {
    public static Task<Result<string>> RunCommandAsync(string command, string? workingDirectory = null) =>
        TryExtensions.Try(async () => {
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
        }).OnFailAddMoreDetails(new { command, workingDirectory });
}