using System;
using System.Collections.Generic;

namespace Quera {
    public static class Program {
        public static void Main() =>
            GetInputs<string>(2)
                .Compair();

        private static void Compair(this IReadOnlyList<string> numbers) {
            for (var i = 2; i >= 0; i--) {
                if (numbers[0][i] < numbers[1][i]) {
                    Console.WriteLine($"{numbers[0]} < {numbers[1]}");
                    return;
                }
                if (numbers[0][i] > numbers[1][i]) {
                    Console.WriteLine($"{numbers[1]} < {numbers[0]}");
                    return;
                }
            }

            Console.WriteLine($"{numbers[0]} = {numbers[1]}");
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