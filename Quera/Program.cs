using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInputs();

            var sumOfJams = input.Jams.Sum() * 1.0;
            var result = input.NumOfJars - Math.Ceiling(sumOfJams / input.Capacity);

            Console.WriteLine(result);
        }

        private static Input GetInputs() {
            var inputs = GetInputs<int>(' ');
            return new Input {
                NumOfJars = inputs[0],
                Capacity = inputs[1],
                Jams = GetInputs<int>(' ')
            };
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Input {
        public int NumOfJars { get; set; }
        public int Capacity { get; set; }
        public List<int> Jams { get; set; }
    }
}