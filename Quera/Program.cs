using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = GetInputs<int>(1).Single(); //numOfQuestions - Ignore
            var numOfNafasGir = GetInputs<int>(' ');
            var numOfSolver = GetInputs<int>(' ');

            var sum = numOfNafasGir.Select((t, i) => t * numOfSolver[i]).Sum();
            Console.WriteLine(sum);
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
}