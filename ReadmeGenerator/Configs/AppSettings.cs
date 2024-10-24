using System.Collections.Generic;

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
}