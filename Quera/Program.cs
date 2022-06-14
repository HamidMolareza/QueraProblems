using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<int>(1).Single()
                .ProcessKhayamPaskal()
                .Print();
        }

        private static List<List<int>> ProcessKhayamPaskal(this int n) {
            var result = new List<List<int>> {
                new List<int>() {1},
                new List<int>() {1, 1}
            };

            if (n == 1)
                return new List<List<int>> {result[0]};

            for (var i = 3; i <= n; i++) {
                result.Add(ProcessNewRow(result[i - 2]));
            }

            return result;
        }

        private static List<int> ProcessNewRow(List<int> prevRow) {
            var result = new List<int>(prevRow.Count + 1);
            result.Add(1);

            for (var i = 0; i < prevRow.Count - 1; i++) {
                result.Add(prevRow[i] + prevRow[i + 1]);
            }

            result.Add(1);
            return result;
        }

        private static void Print(this List<List<int>> result) {
            foreach (var row in result) {
                foreach (var column in row) {
                    Console.Write($"{column} ");
                }

                Console.WriteLine();
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