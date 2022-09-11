using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            Console.ReadLine(); //Skip line
            var claims = GetInputs<int>(' ');

            var gcd = claims.GetGcd();
            var numOfObjects = claims.Sum(claim => claim / gcd);

            Console.WriteLine(numOfObjects);
        }

        private static long GetGcd(this IReadOnlyList<int> numbers) {
            long gcd = numbers[0];
            for (var i = 1; i < numbers.Count; i++)
                gcd = GetGcd(gcd, numbers[i]);
            return gcd;
        }

        private static long GetGcd(long num1, long num2) {
            var biggerNumber = num1 >= num2 ? num1 : num2;
            var smallerNumber = num1 >= num2 ? num2 : num1;

            while (smallerNumber > 0) {
                var remain = biggerNumber % smallerNumber;
                biggerNumber = smallerNumber;
                smallerNumber = remain;
            }

            return biggerNumber;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList()
            ?? new List<T>();
    }
}