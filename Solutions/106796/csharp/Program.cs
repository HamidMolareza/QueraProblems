using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = Console.ReadLine(); //ignore string length
            var k = GetInputs<int>(1).Single();
            var text = GetInputs<string>(1).Single();

            for (var i = 0; i < k; i++)
                text = Encode(text);

            Console.WriteLine(text);
        }

        private static string Encode(string text) {
            if (text.Length < 2)
                throw new ArgumentException("Input length must more than 1", nameof(text));

            var result = new StringBuilder(text.Length);
            result.Append(text.Last())
                .Append(text.Substring(0, text.Length - 1));

            for (var i = 0; i < result.Length; i++)
                result[i] = GetNextChar(result[i]);

            return result.ToString();
        }

        private static char GetNextChar(char c) {
            if (c == 'z' || c == 'Z')
                return 'a';
            return (char) (c + 1);
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