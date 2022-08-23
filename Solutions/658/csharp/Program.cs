using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = Console.ReadLine(); //skip

            var result = GetInputs<int>(' ')
                .FindBestSubcategory()
                .Sum();

            result = result < 0 ? 0 : result;
            Console.WriteLine(result);
        }

        private static List<int> FindBestSubcategory(this IReadOnlyCollection<int> items) {
            var bestSubcategory = new List<int>();
            var bestSum = -1;

            for (var startIndex = 0; startIndex < items.Count; startIndex++) {
                for (var takeCount = 1; takeCount <= items.Count - startIndex; takeCount++) {
                    var subcategory = items.Skip(startIndex).Take(takeCount).ToList();
                    var sum = subcategory.Sum();
                    if (sum > bestSum) {
                        bestSum = sum;
                        bestSubcategory = subcategory;
                    }
                }
            }

            return bestSubcategory;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}