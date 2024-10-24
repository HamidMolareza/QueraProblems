using System;
using System.Collections.Generic;

namespace Quera.Cache;

public class CacheModel {
    public List<QueraProblem> QueraProblems { get; set; } = [];

    public class QueraProblem {
        public QueraProblem(string queraId, string title) {
            if (string.IsNullOrEmpty(queraId))
                throw new ArgumentNullException(nameof(queraId));
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            QueraId = queraId;
            Title = title;
        }

        public string QueraId { get; }
        public string Title { get; }
    }
}