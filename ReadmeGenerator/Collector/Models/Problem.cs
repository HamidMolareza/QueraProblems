using System;
using System.Collections.Generic;

namespace Quera.Collector.Models;

public class Problem {
    public long QueraId { get; init; }
    public string? QueraTitle { get; set; }
    public DateTime LastSolutionsCommit { get; init; }
    public List<Solution> Solutions { get; init; } = [];
    public List<Contributor> Contributors { get; init; } = [];
}