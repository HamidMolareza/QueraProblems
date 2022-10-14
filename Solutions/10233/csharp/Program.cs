using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var number = GetInputs<string>(1).Single();

            var solution1 = Solution1.Solve(number);
            var solution2 = Solution2.Solve(number);

            Console.WriteLine(solution1);
            Console.WriteLine(solution2);
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