namespace Quera {
    public class Configs {
        public const string ConfigFile = @"ReadmeGenerator/Data/configs.json"; //TODO: Get path from input
        public int DelayToRequestQueraInMilliSeconds { get; set; }
        public string SolutionUrlFormat { get; set; }
        public string QueraQuestionsUrlFormat { get; set; }
        public string ReadmeFileName { get; set; }
        public string ReadmeTemplateName { get; set; }
    }
}