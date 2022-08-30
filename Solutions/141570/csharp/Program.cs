using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<int>(1).Single()
                .GetArea()
                .PrintLine();
        }

        private static string GetArea(this int point) {
            if (point < 1)
                return "out";
            return point < 7
                ? "white"
                : "black";
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static void PrintLine<T>(this T source) => Console.WriteLine(source);
    }
}