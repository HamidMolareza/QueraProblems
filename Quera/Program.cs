using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<int>(1).Single()
                .Calculate()
                .Print();
        }

        private static List<int> Calculate(this int n) {
            var result = new List<int>();
            for (var i = 1; i <= n; i++) {
                result.AddRange(Divisor(i));
            }

            return result;
        }

        private static IEnumerable<int> Divisor(int number) {
            if (number < 1) return new List<int>();
            if (number < 2) return new List<int> {1};

            var result = new List<int> {1, number};
            for (var i = 2; i < (number / 2) + 1; i++) {
                if (number % i == 0) {
                    result.Add(i);
                }
            }

            return result;
        }

        private static void Print(this IReadOnlyCollection<int> numbers) {
            var sum = numbers.Sum();
            Console.WriteLine($"{numbers.Count} {sum}");
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