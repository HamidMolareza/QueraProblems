using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<int>(1).Single()
                .PrintTriangles(2);
        }

        private static void PrintTriangles(this int n, int repeat) {
            for (var i = 1; i <= n; i++) {
                var middle = Math.Ceiling(n * 1.0 / 2);
                var numOfStar = (int)(n - (Math.Abs(middle - i) * 2));
                var numOfSpaces = n - numOfStar;

                for (var j = 0; j < repeat; j++) {
                    Print(numOfStar, numOfSpaces);
                }
                Console.WriteLine();
            }
        }

        private static void Print(int numOfStar, int numOfSpaces) {
            var space = numOfSpaces / 2;
            Print(' ', space);
            Print('*', numOfStar);
            Print(' ', space);
        }

        private static void Print(char c, int count) {
            for (var i = 0; i < count; i++) {
                Console.Write(c);
            }
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