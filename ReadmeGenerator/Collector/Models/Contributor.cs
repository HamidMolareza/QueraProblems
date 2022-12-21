using System;

namespace Quera.Collector.Models;

public abstract class Contributor {
    public Contributor(string name, string email, int numOfChangeFiles) {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));

        if (numOfChangeFiles < 0)
            throw new ArgumentOutOfRangeException(nameof(numOfChangeFiles));
        NumOfChangeFiles = numOfChangeFiles;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public int NumOfChangeFiles { get; set; }
}