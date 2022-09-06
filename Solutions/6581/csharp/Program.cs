using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInput(); //length, numOfHints

            /*
             * if w(numOfHints) = 3
             * d + d/2 + d/4 = length
             * => 4d/4 + 2d/4 + d/4 = length
             * => (4 + 2 + 1)d/4 = length
             * => (4 + 2 + 1)d = 4 * length
             * => (4 + 2 + 1)d = kmm * length
             * => d = (kmm * length) / (4 + 2 + 1)
             * => d = (kmm * length) / (2 ^ numOfHints - 1)
             * 
             * if w(numOfHints) = 3
             * => kmm(4,2,1) = 4
             * => kmm(4,2,1) = 2 ^ (3-1)
             * => kmm(4,2,1) = 2 ^ (numOfHints-1)
             */

            var kmm = Math.Pow(2, input.NumOfHints - 1);
            var d = (kmm * input.Length) / (Math.Pow(2, input.NumOfHints) - 1);

            Console.WriteLine(d.ToString("0.0000"));
        }

        private static Input GetInput() {
            var inputs = GetInputs<int>(' '); // t(length) w(numOfHits)
            return new Input {
                Length = inputs[0],
                NumOfHints = inputs[1]
            };
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Input {
        public int Length { get; set; }
        public int NumOfHints { get; set; }
    }
}