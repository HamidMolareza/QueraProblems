using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInput(3);

            var uniqueX = input.XValues
                .GetUniqueValue();
            var uniqueY = input.YValues
                .GetUniqueValue();

            Console.WriteLine($"{uniqueX} {uniqueY}");
        }

        private static T GetUniqueValue<T>(this IEnumerable<T> items) =>
            items.GroupBy(t => t)
                .Single(grouping => grouping.Count() == 1).Key;

        private static Input GetInput(int count) {
            var result = new Input();

            for (var i = 0; i < count; i++) {
                var input = GetInputs<int>(' ');
                result.XValues.Add(input[0]);
                result.YValues.Add(input[1]);
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
        public List<int> XValues { get; set; } = new List<int>();
        public List<int> YValues { get; set; } = new List<int>();
    }
}