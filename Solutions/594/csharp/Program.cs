using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(' ');

            var convertedNumber = ConvertToBase(inputs[0], inputs[1]);

            var sum1 = 0;
            var sum2 = 0;
            for (var i = 0; i < convertedNumber.Length; i++) {
                var digit = convertedNumber[i] - '0';
                if (i % 2 == 0)
                    sum1 += digit;
                else
                    sum2 += digit;
            }

            Console.WriteLine(sum1 == sum2 ? "Yes" : "No");
        }

        private static string ConvertToBase(int number, int toBase) {
            if (toBase == 10) return number.ToString();

            var converted = 0;
            for (var i = 0; number > 0; i++) {
                converted += (int)Math.Pow(10, i) * (number % toBase);
                number /= toBase;
            }

            return converted.ToString();
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}