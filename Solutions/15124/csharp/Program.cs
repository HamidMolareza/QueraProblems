using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(' ');

            var bag1 = GetDivisors(inputs[0]);
            var bag2 = GetDivisors(inputs[1]);

            var query = from b1 in bag1
                from b2 in bag2
                where b1 + b2 <= inputs[2]
                select b1;

            Console.WriteLine(query.Count());
        }

        private static List<int> GetDivisors(int number) {
            var result = new List<int>();

            for (var i = 1; i <= number; i++) {
                if (number % i == 0)
                    result.Add(i);
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