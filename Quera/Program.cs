using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInputs<string>(1).Single();
            Console.WriteLine(input);

            for (var i = 1; i < input.Length; i++) {
                var targetChar = Convert.ToChar(input.Substring(i, 1));
                var text = new string(targetChar, i); //Repeat char i times.
                text += input.Substring(i); //Adds remain chars
                Console.WriteLine(text);
            }
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