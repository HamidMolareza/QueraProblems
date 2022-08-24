using System.IO;

namespace Quera {
    public class Configs {
        public const string DataDirectory = "Data";
        public static readonly string ConfigFile = Path.Combine(DataDirectory, "configs.json");
        public int DelayToRequestQueraInMilliSeconds { get; set; }
        public string SolutionUrlFormat { get; set; }
        public string QueraQuestionsUrlFormat { get; set; }
        public string ReadmeFileName { get; set; }
        public string ReadmeTemplateName { get; set; }
        public int NumOfTry { get; set; }
    }
}