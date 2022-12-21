using System.Collections.Generic;
using System.IO;

namespace Quera.Configs;

public class ConfigsModel {
    public const string DataDirectory = "Data";
    public static readonly string ConfigFile = Path.Combine(DataDirectory, "configs.json");
    public int DelayToRequestQueraInMilliSeconds { get; set; }
    public string SolutionUrlFormat { get; set; } = null!;
    public string ProblemUrlFormat { get; set; } = null!;
    public string ReadmeFileName { get; set; } = null!;
    public string ReadmeTemplateName { get; set; } = null!;
    public string CacheFileName { get; set; } = null!;
    public int NumOfTry { get; set; }
    public List<string> IgnoreSolutions { get; set; } = new();
}