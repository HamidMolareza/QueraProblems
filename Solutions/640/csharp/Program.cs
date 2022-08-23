using System;
using System.Collections.Generic;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<long>(2);

            var result = Gcd(inputs[0], inputs[1]);

            Console.WriteLine(result);
        }

        private static long Gcd(long a, long b) {
            var inputs = StandardizeInputs(a, b);

            return inputs[1] == 0
                ? inputs[0]
                : Gcd(inputs[1], inputs[0] % inputs[1]);
        }

        private static List<long> StandardizeInputs(long a, long b) {
            a = Math.Abs(a);
            b = Math.Abs(b);
            return a >= b
                ? new List<long> {a, b}
                : new List<long> {b, a};
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