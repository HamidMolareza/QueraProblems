using System;

namespace Quera.Collector.Models;

public class Solution {
    private string _languageName = null!;

    public string LanguageName {
        get => _languageName;
        set {
            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentNullException($"{nameof(LanguageName)} can not be null or empty.");
            }

            _languageName = value;
        }
    }

    public DateTime LastCommitDate { get; set; }
}