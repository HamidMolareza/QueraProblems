using System;
using System.Collections.Generic;

namespace Quera.Models {
    public class Problem {
        public string QueraId { get; set; }
        public List<Solution> Solutions { get; set; }
        public DateTime LastSolutionsCommit { get; set; }
    }
}