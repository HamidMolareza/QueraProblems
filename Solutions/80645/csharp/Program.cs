using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var board = GetInputs<int>(2);
            var eraser = GetInputs<int>(2);

            var minBoard = board.Min();
            var maxEraser = eraser.Max();

            Console.WriteLine(Math.Ceiling(1.0 * minBoard / maxEraser));
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