using System;
using System.Linq;

class Program {
    static void Main(string[] args) {
        string input = Console.ReadLine();

        var vowelCount = input.Count(c => "aeiou".Contains(c));

        Console.WriteLine(vowelCount);
    }
}
