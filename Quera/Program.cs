using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<decimal>(' ')
                .OrderByDescending(n => n)
                .ToList();

            var gcd = Gcd(inputs[0], inputs[1]);
            var lcm = Lcm(inputs[0], inputs[1], gcd);

            Console.WriteLine($"{gcd} {lcm}");
        }

        private static decimal Lcm(decimal a, decimal b, decimal gcd) =>
            (a * b) / gcd;

        private static decimal Gcd(decimal a, decimal b) {
            while (true) {
                var remain = a % b;
                if (remain == 0) return b;
                a = b;
                b = remain;
            }
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}