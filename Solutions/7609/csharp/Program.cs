using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var text = GetInputs<string>(1).Single();

            var isGood = IsGood(text);

            Console.WriteLine(isGood ? "khoob" : "bad");
        }

        private static bool IsGood(string text) {
            if (IsOdd(text.Length)) {
                //The sum of even numbers is always even.
                //So if the sum of the words is odd, it means we had a word of odd length.
                return false;
            }

            var lastChar = text.First();
            var count = 0;
            foreach (var c in text) {
                if (c != lastChar) {
                    if (IsOdd(count))
                        return false;
                    count = 1;
                    lastChar = c;
                }
                else {
                    count++;
                }
            }

            return true;
        }

        private static bool IsOdd(int number) => number % 2 != 0;

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}