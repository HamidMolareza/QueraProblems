using System.IO;

namespace Quera {
    public class Configs {
        public const string DataDirectory = "Data";
        public static readonly string ConfigFile = Path.Combine(DataDirectory, "configs.json");
        public int DelayToRequestQueraInMilliSeconds { get; set; }
        public string SolutionUrlFormat { get; set; } = null!;
        public string QueraQuestionsUrlFormat { get; set; } = null!;
        public string ReadmeFileName { get; set; } = null!;
        public string ReadmeTemplateName { get; set; } = null!;
        public string CacheFileName { get; set; } = null!;
        public int NumOfTry { get; set; }
    }
}