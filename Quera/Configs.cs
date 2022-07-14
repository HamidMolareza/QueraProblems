using System.Collections.Generic;

namespace Quera {
    public static class Configs {
        public const int DelayToRequestQueraInMilliSeconds = 1500;

        public const string SolutionUrlFormat =
            "https://github.com/HamidMolareza/QueraProblems/blob/{0}/Quera/Program.cs";

        public const string QueraQuestionsUrlFormat = "https://quera.org/problemset/{0}/";
        public const string ReadmeFileName = "README.md";
        public static readonly List<string> IgnoreBranchList = new() {"master", "Utility"};
        public const string ReadmeTemplateName = "ReadmeTemplate.md";
    }
}