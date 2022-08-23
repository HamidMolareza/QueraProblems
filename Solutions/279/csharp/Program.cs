using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(' '); //a x n

            var sum = 0.0;
            for (var k = 0; k <= inputs[2]; k++)
                sum += Combinations(inputs[2], k) * Math.Pow(inputs[1], k) * Math.Pow(inputs[0], inputs[2] - k);

            Console.WriteLine(sum);
        }

        //Combinations(n,k) = n! / (n - k)! * k!
        private static double Combinations(int n, int k) {
            var result = 1.0;
            for (var i = n; i > n - k; i--)
                result *= i;
            return result / Factorial(k);
        }

        private static double Factorial(int number) {
            var result = 1.0;
            for (var i = 1; i <= number; i++)
                result *= i;
            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}