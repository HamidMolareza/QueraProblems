using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var vowelChars = GetInputs<string>(1).Single()
                .Count(IsVowel);

            Console.WriteLine(Math.Pow(2, vowelChars));
        }

        private static bool IsVowel(char c) =>
            c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}