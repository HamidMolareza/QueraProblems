using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInputs<int>(' ');
            var numOfBottle = input[0];
            var liquidVolume = input[1];

            var bottleVolumes = GetInputs<int>(numOfBottle);
            var sumOfBottleVolumes = bottleVolumes.Sum();

            Console.WriteLine(liquidVolume <= sumOfBottleVolumes ? "YES" : "NO");
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