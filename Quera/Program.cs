using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var n = GetInputs<int>(1).Single();

            GetInputs<string>(n)
                .ConvertToCamelCase()
                .Print();
        }

        private static void Print(this IEnumerable<string> lines) {
            foreach (var line in lines) {
                Console.WriteLine(line);
            }
        }

        private static IEnumerable<string> ConvertToCamelCase(this IEnumerable<string> lines) =>
            lines.Select(ConvertToCamelCase);

        private static string ConvertToCamelCase(string line) {
            var camelCase = line.Split(' ')
                .Select(name => char.ToUpper(name[0]) + name.Substring(1).ToLower());

            return string.Join(" ", camelCase);
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