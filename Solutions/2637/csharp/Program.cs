using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var n = GetInputs<int>(1).Single();

            Console.WriteLine($"Solution1: {Solution1.GetMaximumSquares(n)}");
            Console.WriteLine($"Solution2: {Solution2.GetMaximumSquares(n)}");
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}