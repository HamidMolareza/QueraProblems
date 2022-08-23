using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var n = GetInputs<int>(1).Single();
            var states = GetInputs<int>(n);

            var lastState = states.First();
            var numOfChanges = 0;
            for (var i = 1; i < states.Count; i++) {
                if (lastState == states[i])
                    continue;
                lastState = states[i];
                numOfChanges++;
            }

            Console.WriteLine(numOfChanges);
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