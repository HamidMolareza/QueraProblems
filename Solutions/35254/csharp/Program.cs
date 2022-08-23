using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Quera {
    public static class Program {
        private static readonly Regex ThievesRegex = new Regex("[H]+");

        public static void Main() {
            GetTargetPathFromConsole()
                .GetSteps()
                .Print();
        }

        private static string GetTargetPathFromConsole() {
            var _ = Console.ReadLine(); //ignore
            var path = GetInputs<string>(1).Single();
            var points = GetInputs<int>(' ')
                .Select(point => point - 1) //Convert to zero base
                .ToList();

            points.Sort();
            return path.Substring(points[0], points[1] - points[0] + 1);
        }

        private static int GetSteps(this string targetPath) {
            var numOfSteps = 0;
            foreach (Match match in ThievesRegex.Matches(targetPath))
                numOfSteps += GetSteps(match.Value.Length);
            return numOfSteps;
        }

        private static int GetSteps(int valueLength) {
            if (valueLength < 2)
                return valueLength >= 0 ? valueLength : 0;

            var count = 0;
            while (valueLength > 0) {
                var numOf2 = GetNumOf2(valueLength);
                valueLength -= (int) Math.Pow(2, numOf2);
                count++;
            }

            return count;
        }

        private static int GetNumOf2(int valueLength) {
            var count = 0;
            while (valueLength > 1) {
                valueLength /= 2;
                count++;
            }

            return count;
        }
        
        private static void Print(this int result) => Console.WriteLine(result);

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