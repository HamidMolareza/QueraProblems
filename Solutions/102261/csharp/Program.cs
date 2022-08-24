using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfQuestions = GetInputs<int>(1).Single();
            for (var i = 0; i < numOfQuestions; i++) {
                GetInputs<int>(' ')
                    .GetNumOfSquareNumber()
                    .PrintLine();
            }
        }

        private static int GetNumOfSquareNumber(this IReadOnlyList<int> range) {
            var begin = Math.Ceiling(Math.Sqrt(range[0]));
            var end = Math.Floor(Math.Sqrt(range[1]));
            return (int) (end - begin + 1);
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

        private static void PrintLine<T>(this T source) => Console.WriteLine(source);
    }
}