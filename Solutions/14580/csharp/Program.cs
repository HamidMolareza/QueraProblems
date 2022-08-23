using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var result = GetInput()
                .CalculateNextChannel();

            Console.WriteLine(result);
        }

        private static string CalculateNextChannel(this Input input) {
            var sum = input.CurrentChannel + input.NumOfPressKey;
            var remain = sum % input.NumOfChannels;

            return remain == 0
                ? input.NameOfChannels.Last()
                : input.NameOfChannels[remain - 1];
        }

        private static Input GetInput() {
            var line = GetInputs<int>(' ');
            return new Input {
                NumOfChannels = line[0],
                CurrentChannel = line[1],
                NumOfPressKey = line[2],
                NameOfChannels = GetInputs<string>(line[0])
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
        public int NumOfChannels { get; set; }
        public int CurrentChannel { get; set; }
        public int NumOfPressKey { get; set; }
        public List<string> NameOfChannels { get; set; }
    }
}