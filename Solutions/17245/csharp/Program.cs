using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const int NumOfLines = 3;
        private const int BlockWidth = 5;

        public static void Main() {
            GetInputs<string>(NumOfLines)
                .SelectChars()
                .Translate()
                .CombineChars()
                .PrintLine();
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static List<string> SelectChars(this IEnumerable<string> source) {
            var lines = source
                .Select(line => line.Split(BlockWidth))
                .Select(line => line.ToList())
                .ToList();

            var result = new List<string>(lines.Count);
            for (var column = 0; column < lines[0].Count; column++) {
                var character = lines.Select(line => line[column]);
                result.Add(string.Join(string.Empty, character));
            }

            return result;
        }

        private static IEnumerable<char> Translate(this IEnumerable<string> chars) => chars.Select(Translate);

        private static char Translate(this string character) {
            switch (character) {
                case "*****oo*oooo*oo":
                    return 'T';
                case "oo*ooo***o*ooo*":
                    return 'A';
                case "*ooo*oo*oo*ooo*":
                    return 'X';
                case "**o***o*o**ooo*":
                    return 'M';
                case "*ooo**o*o**ooo*":
                    return 'N';
                default:
                    throw new ArgumentOutOfRangeException(nameof(character));
            }
        }

        private static string CombineChars(this IEnumerable<char> chars) => string.Join(string.Empty, chars);

        private static void PrintLine<T>(this T source) => Console.WriteLine(source);

        private static IEnumerable<string> Split(this string source, int chunkSize) =>
            Enumerable.Range(0, source.Length / chunkSize)
                .Select(i => source.Substring(i * chunkSize, chunkSize));
    }
}