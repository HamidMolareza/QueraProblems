using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInput();

            var cycleTime = input.WaitAfterArAr + input.WaitAfterMaMa + 2; // (a + 1) + (b + 1) = a + b + 2

            var numOfCycleRepetitions = input.Time / cycleTime;
            var numOfArAr = numOfCycleRepetitions;
            var numOfMaMa = numOfCycleRepetitions;

            var remain = input.Time % cycleTime;
            if (remain > 0) {
                //The cycle has been completed, so even if remain = 1, we will hear the sound of the next ArAr.
                numOfArAr++;
            }

            if (remain - 1 - input.WaitAfterArAr > 0)
                numOfMaMa++;

            Console.WriteLine($"{numOfArAr} {numOfMaMa}");
        }

        private static Input GetInput() {
            var inputs = GetInputs<int>(' '); //t a b
            return new Input {
                Time = inputs[0],
                WaitAfterArAr = inputs[1],
                WaitAfterMaMa = inputs[2]
            };
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList()
            ?? new List<T>();
    }

    public class Input {
        public int Time { get; set; }
        public int WaitAfterArAr { get; set; }
        public int WaitAfterMaMa { get; set; }
    }
}