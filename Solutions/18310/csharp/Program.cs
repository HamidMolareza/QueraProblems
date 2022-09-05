using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs()
                .CanWinPrize()
                .PrintLine(canWin => canWin ? "Yes" : "No");
        }

        private static Input GetInputs() => new Input {
            CircleLetters = GetInputs<string>(1).Single(),
            TargetString = GetInputs<string>(1).Single()
        };

        private static bool CanWinPrize(this Input input) =>
            FindStartIndexes(input)
                .Any(startIndex => IsMatch(input, startIndex));

        private static IEnumerable<int> FindStartIndexes(Input input) {
            var indexes = new List<int>();
            for (var i = 0; i < input.CircleLetters.Length; i++) {
                if (input.CircleLetters[i] == input.TargetString[0])
                    indexes.Add(i);
            }

            return indexes;
        }

        private static bool IsMatch(Input input, int startIndex) {
            for (var targetIndex = 0; targetIndex < input.TargetString.Length; targetIndex++) {
                var circleIndex = (targetIndex + startIndex) % input.CircleLetters.Length;
                if (input.CircleLetters[circleIndex] != input.TargetString[targetIndex])
                    return false;
            }

            return true;
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static void PrintLine<TSource, TResult>(
            this TSource source,
            Func<TSource, TResult> func
        ) => Console.WriteLine(func(source));
    }

    public class Input {
        public string CircleLetters { get; set; }
        public string TargetString { get; set; }
    }
}