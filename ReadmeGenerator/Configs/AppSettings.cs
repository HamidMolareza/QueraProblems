using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Quera.Configs;

public class AppSettings {
    public int DelayToRequestQueraInMilliSeconds { get; set; }
    public string SolutionUrlFormat { get; set; } = default!;
    public string ProblemUrlFormat { get; set; } = default!;
    public string ReadmeTemplatePath { get; set; } = default!;
    public string ReadmeOutputPath { get; set; } = default!;
    public int NumberOfTry { get; set; }
    public List<string> IgnoreSolutions { get; init; } = [];

    public string WorkingDirectory { get; set; } = default!;
    public string SolutionsPath { get; set; } = default!;

    public List<UserModel> Users { get; init; } = [];
    public string CacheFilePath { get; set; } = default!;
    public string LogLevel { get; set; } = default!;
    public int MainPageLimit { get; set; }
    public string? MainPageFooter { get; set; }
    public string CompleteListTemplatePath { get; set; } = default!;
    public string CompleteListOutputPath { get; set; } = default!;


    public override string ToString() {
        var sb = new StringBuilder();
        var type = GetType();
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties) {
            var value = prop.GetValue(this);
            var formattedValue = FormatValue(value);
            sb.AppendLine($"{prop.Name}: {formattedValue}");
        }

        return sb.ToString();
    }

    private static string FormatValue(object? value) {
        return value switch {
            null => "null",
            string s => $"\"{s}\"",
            IEnumerable<string> list => $"[{string.Join(", ", list.Select(s => $"\"{s}\""))}]",
            IEnumerable<object> objList => $"[{string.Join(", ", objList)}]",
            _ => value.ToString() ?? "null"
        };
    }
}