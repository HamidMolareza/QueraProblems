using System;
using System.Collections.Generic;

namespace Quera.Models {
    public class Problem {
        public string QueraId { get; set; }
        public List<Solution> Solutions { get; set; }
        //TODO: https://github.com/HamidMolareza/QueraProblems/issues/8
        public DateTime LastSolutionsCommit { get; set; }
    }
}