using System;
using System.Collections.Generic;

namespace Quera.Collector.Models;

public class Problem {
    public Problem(long queraId, string queraTitle, DateTime lastSolutionsCommit) {
        if (string.IsNullOrEmpty(queraTitle))
            throw new ArgumentNullException(nameof(queraTitle));

        QueraId = queraId;
        QueraTitle = queraTitle;
        LastSolutionsCommit = lastSolutionsCommit;
    }

    public long QueraId { get; set; }
    public string QueraTitle { get; set; }

    public List<Solution> Solutions { get; set; } = new();

    public DateTime LastSolutionsCommit { get; set; }
}