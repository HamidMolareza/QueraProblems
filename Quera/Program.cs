using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<int>(1); //Ignore
            var watermelons = GetInputs<int>(' ')
                .Select((watermelon, index) => new KeyValuePair<int, int>(index, watermelon))
                .ToList();

            var result = Eat(watermelons);

            Console.WriteLine(result.Key + 1);
        }

        private static KeyValuePair<int, int> Eat(IList<KeyValuePair<int, int>> watermelons) {
            while (true) {
                if (watermelons.Count == 1)
                    return watermelons.First();

                watermelons.RemoveAt(watermelons[0].Value > watermelons[1].Value ? 1 : 0);
            }
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