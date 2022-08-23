using System.Collections.Generic;

namespace Quera {
    public class Configs {
        public const string ConfigFile = @"Data/configs.json";
        public int DelayToRequestQueraInMilliSeconds { get; set; }
        public string SolutionUrlFormat { get; set; }
        public string QueraQuestionsUrlFormat { get; set; }
        public string ReadmeFileName { get; set; }
        public string ReadmeTemplateName { get; set; }
        public List<string> IgnoreBranchList { get; set; } = new();
    }
}