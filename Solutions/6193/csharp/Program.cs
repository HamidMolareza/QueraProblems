using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quera {
    public static class Program {
        public static void Main() {
            var text = GetInputs<string>(1).Single();

            var result = Code(text);

            Console.WriteLine(result);
        }

        private static string Code(string text) {
            var result = new StringBuilder(text.Length);
            
            foreach (var targetChar in text) {
                var count = text.Count(c => char.ToLower(c) == char.ToLower(targetChar));
                var charIndex = char.ToLower(targetChar) - 'a';
                var replaceCharIndex = (count * charIndex + 1) % 26;
                var replaceChar = (char) (char.IsLower(targetChar)
                    ? replaceCharIndex + 'a'
                    : replaceCharIndex + 'A');
                result.Append(replaceChar);
            }

            return result.ToString();
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