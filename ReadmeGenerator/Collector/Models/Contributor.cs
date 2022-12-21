using System;

namespace Quera.Collector.Models;

public abstract class Contributor {
    public Contributor(string name, string email, int numOfChangeLines) {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));

        if (numOfChangeLines < 0)
            throw new ArgumentOutOfRangeException(nameof(numOfChangeLines));
        NumOfChangeLines = numOfChangeLines;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public int NumOfChangeLines { get; set; }
}