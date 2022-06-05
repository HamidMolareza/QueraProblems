using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var score = GetNumbers(2)
                .CalculateScore();
            Console.WriteLine(score);
        }

        private static List<int> GetNumbers(int count) {
            var result = new List<int>(count);
            for (var i = 0; i < count; i++) {
                result.Add(Convert.ToInt32(Console.ReadLine()));
            }

            return result;
        }

        private static int CalculateScore(this IReadOnlyList<int> numbers) {
            switch (numbers[1]) {
                case 0:
                    return 20;
                case 7:
                    return numbers[0];
                default:
                    var result = numbers[0] - numbers[1];
                    return result >= 0 ? result : 0;
            }
        }
    }
}