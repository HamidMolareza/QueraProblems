using System;
using System.Collections.Generic;

namespace Quera.Models;

public class Cache {
    public List<Titles> ProblemTitles { get; set; } = new();

    public class Titles {
        public Titles(string queraId, string title) {
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