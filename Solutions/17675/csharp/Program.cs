using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var maximumNumber = GetInputs<int>(1).Single();

            maximumNumber
                .CalculateFibonachi()
                .Print(maximumNumber);
        }

        private static List<int> CalculateFibonachi(this int maximumNumber) {
            if (maximumNumber < 1)
                return new List<int>();
            if (maximumNumber < 2) return new List<int> {1};

            var result = new List<int> {1, 1, 2};
            if (maximumNumber < 3)
                return result;

            while (true) {
                var currentIndex = result.Count;
                var newResult = result[currentIndex - 2] + result[currentIndex - 1];
                if (newResult > maximumNumber) {
                    break;
                }

                result.Add(newResult);
            }

            return result;
        }

        private static void Print(this List<int> fibonachi, int maximumNumber) {
            for (var i = 1; i <= maximumNumber; i++) {
                Console.Write(fibonachi.Exists(number => number == i) ? "+" : "-");
            }
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