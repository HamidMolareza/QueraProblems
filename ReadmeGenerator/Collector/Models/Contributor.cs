using System;

namespace Quera.Collector.Models;

public class Contributor {
    public Contributor(string name, string email, int numOfCommits) {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email));
        Name = name;
        Email = email;

        if (numOfCommits < 0)
            throw new ArgumentOutOfRangeException(nameof(numOfCommits));
        NumOfCommits = numOfCommits;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public int NumOfCommits { get; set; }
    public string? AvatarUrl { get; set; }
    public string? ProfileUrl { get; set; }
}