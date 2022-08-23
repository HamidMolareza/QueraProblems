using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = Console.ReadLine(); //Ignore
            var inputs = GetInputs<int>(' ');

            var orderedBaseCount = inputs.GroupBy(n => n)
                .OrderBy(g => g.Count())
                .ToList();
            var minimumCount = orderedBaseCount.First().Count();

            var orderedBaseKey = orderedBaseCount
                .Where(g => g.Count() == minimumCount)
                .OrderBy(g => g.Key);

            var target = orderedBaseKey.First().Key;

            Console.WriteLine(target);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}