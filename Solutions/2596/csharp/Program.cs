using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = Console.ReadLine(); //ignore num of inputs
            var divisors = GetInputs<int>(' ');

            var counter = 0;
            for (var number = 1; number <= 1000; number++) {
                if (divisors.All(divisor => number % divisor == 0))
                    counter++;
            }

            Console.WriteLine(counter);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}