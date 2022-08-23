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

            if (uniqueNumbers.Any()) {
                var xor = uniqueNumbers.Aggregate((number, result) => result ^ number);
                Console.WriteLine(xor);
            }
            else {
                Console.WriteLine("0");
            }
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}