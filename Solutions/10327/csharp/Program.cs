using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInput();
            
            var validChars = input.ValidCode.ToCharArray().Distinct();
            var validations = input.Codes.Select(code => code.IsCodeValid(validChars));

            foreach (var isValid in validations) {
                Console.WriteLine(isValid ? "Yes" : "No");
            }
        }

        private static bool IsCodeValid(this string code, IEnumerable<char> validChars) =>
            code.All(validChars.Contains);

        private static Input GetInput() {
            var firstLine = GetInputs<string>(' ');
            var n = Convert.ToInt32(firstLine[0]);
            
            return new Input {
                ValidCode = firstLine[1],
                Codes = GetInputs<string>(n)
            };
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Input {
        public string ValidCode { get; set; }
        public List<string> Codes { get; set; }
    }
}