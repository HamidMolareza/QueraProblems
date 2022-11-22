using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = Console.ReadLine(); //Ignore
            var numbers = GetInputs<int>(' ');

            var uniqueNumbers = numbers.GroupBy(number => number)
                .Where(group => group.Count() == 1)
                .Select(group => group.Key)
                .ToList();

            var xor = uniqueNumbers.Aggregate(0, (result, nextNumber) => nextNumber ^ result);
            Console.WriteLine(xor);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}
