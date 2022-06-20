using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const int ErrorCode = -1;

        private struct Input {
            public int Distance { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        private struct Result {
            public Result(int x, int y) {
                X = x;
                Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }
        }

        public static void Main() {
            var input = GetInputs();

            var result = input.X > input.Y
                ? Solve(input.Distance, input.X, input.Y)
                : Solve(input.Distance, input.Y, input.X);

            if (input.X > input.Y) {
                Print(result.X, result.Y);
            }
            else {
                Print(result.Y, result.X);
            }
        }

        private static Result Solve(int distance, int maximum, int minimum) {
            if (distance % minimum == 0) {
                return new Result(0, distance / minimum);
            }

            var maxTry = distance / maximum;
            for (var i = 1; i <= maxTry; i++) {
                var remain = distance - (maximum * i);
                if (remain % minimum == 0) {
                    return new Result(i, remain / minimum);
                }
            }

            return new Result(ErrorCode, ErrorCode);
        }

        private static void Print(int x, int y) {
            if (x == ErrorCode || y == ErrorCode) {
                Console.WriteLine("-1");
            }
            else {
                Console.WriteLine($"{x} {y}");
            }
        }

        private static Input GetInputs() {
            var inputs = GetInputs<int>(' ');
            return new Input {
                Distance = inputs[0],
                X = inputs[1],
                Y = inputs[2]
            };
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}