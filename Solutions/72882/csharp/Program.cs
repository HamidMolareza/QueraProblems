using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfRows = GetInputs<int>(' ').First();

            var dish1 = GetInputs<string>(numOfRows)
                .GetNumOfMeats();
            var dish2 = GetInputs<string>(numOfRows)
                .GetNumOfMeats();

            Console.WriteLine($"{dish1} {dish2}");
        }

        private static int GetNumOfMeats(this IEnumerable<string> dish) =>
            dish.Sum(d => d.Count(c => c == '*'));

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