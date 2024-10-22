using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        // Input the size of the arrays
        var n = int.Parse(Console.ReadLine());

        // Input the first array of integers
        var n1 = Console.ReadLine().Split().Select(int.Parse).ToArray();

        // Input the second array of integers
        var n2 = Console.ReadLine().Split().Select(int.Parse).ToArray();

        // Use LINQ to filter elements based on the condition
        var m = n1.Where((value, index) => n2[index] == 1).ToList();

        // Sort the filtered list
        m.Sort();

        // Print the elements separated by a space
        Console.WriteLine(string.Join(" ", m));
    }
}