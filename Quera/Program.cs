using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<int>(2)
                .GetPrimeNumbers()
                .Print();
        }

        private static IEnumerable<int> GetPrimeNumbers(this IReadOnlyList<int> range) {
            var result = new List<int>();
            for (var i = range[0] + 1; i < range[1]; i++) {
                if (IsPrime(i)) {
                    result.Add(i);
                }
            }

            return result;
        }

        private static bool IsPrime(int number) {
            if (number < 2) return false; // ... 0 1
            if (number < 4) return true; //2 3

            for (var i = 2; i < Math.Sqrt(number) + 1; i++) {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private static void Print(this IEnumerable<int> result) {
            Console.WriteLine(string.Join(",", result));
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