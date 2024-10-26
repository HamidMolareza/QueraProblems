using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;
using Serilog.Events;

namespace Quera.Helpers;

public static class Utility {
    public static TResult ConvertTo<TSource, TResult>(this TSource source) =>
        (TResult)Convert.ChangeType(source, typeof(TResult))! ??
        throw new Exception($"Can not convert to {typeof(TResult)}");

    public static Task<Result> SaveDataAsync(string path, string data, int numOfTry) =>
        TryExtensions.Try(() =>
                    File.WriteAllTextAsync(path, data),
                numOfTry)
            .OnFailAddMoreDetails(new { path });

    public static LogEventLevel ParseLogLevel(string levelName,
        LogEventLevel defaultLevel = LogEventLevel.Information) {
        if (Enum.TryParse(levelName, true, out LogEventLevel logLevel) &&
            Enum.IsDefined(typeof(LogEventLevel), logLevel)) {
            return logLevel;
        }

        Console.WriteLine($"Invalid log level: {levelName}. Using default level: {defaultLevel}.");
        return defaultLevel;
    }

    public static Task<Result<string>>
        UseTemplateAsync(this string newValue, string templateFilePath, string oldValue) =>
        TryExtensions.Try(() => File.ReadAllTextAsync(templateFilePath))
            .OnSuccess(template => template.Replace(oldValue, newValue));

    public static string CombineStrings(string separator, params string?[] strings) {
        return string.Join(separator, strings.Where(s => !string.IsNullOrWhiteSpace(s)));
    }
}