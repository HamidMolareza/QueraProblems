using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            Console.ReadLine()
                .Trim()
                .Split(' ')
                .Select(number => Convert.ToInt32(number))
                .ToList()
                .IsTriangle()
                .Print();
        }

        private static bool IsTriangle(this IReadOnlyCollection<int> numbers) =>
            numbers.Sum() == 180 && numbers.All(num => num > 0);

        private static void Print(this bool @this) => Console.WriteLine(@this ? "Yes" : "No");
    }
}