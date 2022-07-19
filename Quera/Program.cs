using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInput();

            foreach (var goatWord in input.GoatWords) {
                var hairdresserAnswer = input.HairdresserAnswers
                    .Where(answers => answers.Key == goatWord)
                    .Select(answer => answer.Value)
                    .SingleOrDefault();

                if (hairdresserAnswer != null)
                    Console.Write($"{hairdresserAnswer} ");
                Console.Write("kachal! ");
            }
        }

        private static Input GetInput() {
            var n = GetInputs<int>(' ').First(); //ignore m

            var result = new Input {HairdresserAnswers = new List<KeyValuePair<string, string>>(n)};
            for (var i = 0; i < n; i++) {
                var line = GetInputs<string>(' ');
                result.HairdresserAnswers.Add(new KeyValuePair<string, string>(line[0], line[1]));
            }

            result.GoatWords = GetInputs<string>(' ');
            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Input {
        public List<KeyValuePair<string, string>> HairdresserAnswers { get; set; }
        public List<string> GoatWords { get; set; }
    }
}