using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(2);

            PrintSquare(inputs[0], inputs[1]);
        }

        private static void PrintSquare(int outerSquare, int innerSquare) {
            if (innerSquare >= outerSquare) {
                Console.WriteLine("Wrong order!");
                return;
            }

            var diff = outerSquare - innerSquare;
            if (diff % 2 != 0) {
                Console.WriteLine("Wrong difference!");
                return;
            }

            var line = string.Concat(Enumerable.Repeat("* ", outerSquare));
            var innerLineStars = string.Concat(Enumerable.Repeat("* ", diff / 2));
            var innerLine = innerLineStars + string.Concat(Enumerable.Repeat("  ", innerSquare)) + innerLineStars;

            for (var i = 0; i < diff / 2; i++)
                Console.WriteLine(line);
            for (var i = 0; i < innerSquare; i++)
                Console.WriteLine(innerLine);
            for (var i = 0; i < diff / 2; i++)
                Console.WriteLine(line);
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