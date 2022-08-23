using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfProblematicLetters = GetInputs<string>(1).Single()
                .Count(c => c is 'T' or 'D' or 'L' or 'F');

            var result = Math.Pow(2, numOfProblematicLetters);

            Console.WriteLine(result);
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