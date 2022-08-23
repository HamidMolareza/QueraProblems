using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var k = GetInputs<int>(1).Single();

            foreach (var goodNumber in GoodNumbers()) {
                if (GetNumOfDivisors(goodNumber) >= k) {
                    Console.WriteLine(goodNumber);
                    break;
                }
            }
        }

        private static IEnumerable<int> GoodNumbers() {
            for (var i = 1; ; i++) 
                yield return (i * (i + 1)) / 2;
        }

        private static int GetNumOfDivisors(int number) {
            var counter = 0;
            for (var i = 1; i <= number; i++) {
                if (number % i == 0)
                    counter++;
            }

            return counter;
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}