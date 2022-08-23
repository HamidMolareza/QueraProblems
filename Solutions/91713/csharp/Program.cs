using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfInputs = GetInputs<int>(1).Single();
            var inputs = GetInputs<string>(numOfInputs);

            var results = inputs.Select(IsRound);

            foreach (var result in results)
                Console.WriteLine(result);
        }

        private static string IsRound(string number) {
            if (number == string.Join(string.Empty, number.Reverse()))
                return "Ronde!";

            var repeatNumber = number
                .GroupBy(digit => digit)
                .Any(g => g.Count() >= 4);
            if (repeatNumber)
                return "Ronde!";

            var consecutiveDigitCount = 0;
            var consecutiveDigit = number[0];
            foreach (var digit in number) {
                if (digit == consecutiveDigit) {
                    consecutiveDigitCount++;
                }
                else if (consecutiveDigitCount >= 3) {
                    break;
                }
                else {
                    consecutiveDigitCount = 1;
                    consecutiveDigit = digit;
                }
            }

            return consecutiveDigitCount >= 3
                ? "Ronde!"
                : "Rond Nist";
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