using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = Console.ReadLine(); //ignore

            GetInputs<int>(' ')
                .GetList()
                .Print();
        }

        private static List<int> GetList(this List<int> numbers) {
            var result = new List<int>(numbers.Count);
            numbers.Sort();

            for (var i = 0; i < numbers.Count / 2; i++) {
                result.Add(numbers[numbers.Count - i - 1]);
                result.Add(numbers[i]);
            }

            if (numbers.Count % 2 != 0)
                result.Add(numbers[numbers.Count / 2]);

            return result;
        }

        private static void Print(this IEnumerable<int> numbers) =>
            Console.WriteLine(string.Join(" ", numbers));

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}