using System;
using System.Collections.Generic;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<decimal>(2);
            var biggerNumber = inputs[0] > inputs[1] ? inputs[0] : inputs[1];
            var smallerNumber = inputs[0] > inputs[1] ? inputs[1] : inputs[0];

            var gcd = GCD(biggerNumber, smallerNumber);

            Console.WriteLine(gcd);
        }

        private static decimal GCD(decimal biggerNumber, decimal smallerNumber) {
            var remain = biggerNumber % smallerNumber;
            return remain == 0
                ? smallerNumber
                : GCD(smallerNumber, remain);
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