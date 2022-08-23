using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(3); //a b c

            var numberInTargetBase = inputs[0].ConvertBase(inputs[1], inputs[2]);
            var isPalindrome = IsPalindrome(numberInTargetBase);
            
            Console.WriteLine(isPalindrome ? "YES" : "NO");
        }

        private static int ConvertBase(this int number, int from, int to) =>
            number.ConvertToBase10(from)
                .ConvertFromBase10To(to);

        private static int ConvertToBase10(this int number, int from) {
            if (from == 10) return number;

            var result = 0;
            for (var i = 0; number > 0; i++) {
                var lastDigit = number % 10;
                number /= 10;
                
                result += lastDigit * (int) Math.Pow(from, i);
            }

            return result;
        }

        private static int ConvertFromBase10To(this int number, int to) {
            var result = 0;

            for (var i = 0; number > 0; i++) {
                var remain = number % to;
                number /= to;
                result += remain * (int) Math.Pow(10, i);
            }

            return result;
        }

        private static bool IsPalindrome(int number) {
            var numStr = number.ToString();
            return numStr == string.Join(string.Empty, numStr.Reverse());
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