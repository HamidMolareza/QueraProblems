using System;
using System.Collections.Generic;

namespace Quera.Collector.Models;

public class Problem {
    public long QueraId { get; set; }

    public List<Solution> Solutions { get; set; } = new();

    public DateTime LastSolutionsCommit { get; set; }

    public List<Contributor> Contributors { get; set; }
}