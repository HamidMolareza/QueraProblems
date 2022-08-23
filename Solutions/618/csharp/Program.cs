using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var n = GetInputs<int>(1).Single();
            var maximumStar = (2 * n) + 1;

            for (var i = 0; i < maximumStar; i++) {
                var standardDeviation = Math.Abs(n - i);
                var numOfStar = maximumStar - (standardDeviation * 2);
                PrintLine(maximumStar, numOfStar);
            }
        }

        private static void PrintLine(int maximumStar, int numOfStar) {
            var space = (maximumStar - numOfStar) / 2;
            var spaceStr = new string(' ', space);

            Console.Write(spaceStr);
            Console.Write(new string('*', numOfStar));
            Console.WriteLine(spaceStr);
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