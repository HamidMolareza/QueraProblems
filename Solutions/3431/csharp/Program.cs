using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfRows = GetInputs<int>(' ').First();
            var tableRows = GetInputs<string>(numOfRows);
            var target = GetInputs<string>(1).Single();

            var count1 = tableRows.Count(target);
            var count2 = tableRows.GetTableColumns().Count(target);

            Console.WriteLine(count1 + count2);
        }

        private static List<string> GetTableColumns(this IReadOnlyList<string> tableRows) {
            var result = new List<string>(tableRows.Count);
            for (var column = 0; column < tableRows[0].Length; column++) {
                var chars = tableRows.Select(row => row[column]);
                result.Add(string.Join(string.Empty, chars));
            }

            return result;
        }

        private static int Count(this IEnumerable<string> strings, string target) =>
            strings.Sum(str => str.Count(target));

        private static int Count(this string str, string target) {
            var count = 0;
            var index = -1;
            while (true) {
                index = str.IndexOf(target, index + 1, StringComparison.OrdinalIgnoreCase);
                if (index >= 0) {
                    count++;
                }
                else {
                    return count;
                }
            }
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}