using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private static readonly List<Coordinate> Operations = new List<Coordinate> {
            new Coordinate(1, 0),
            new Coordinate(0, 1),
            new Coordinate(-1, 0),
            new Coordinate(0, -1)
        };

        public static void Main() {
            var n = GetInputs<int>(1).Single();

            var currentCoordinate = new Coordinate(0, 0);
            for (var i = 0; i < n - 1; i++) {
                var operation = Operations[i % Operations.Count]  * ((i / 2) + 1);
                currentCoordinate += operation;
            }

            Print(currentCoordinate);
        }

        private static void Print(Coordinate coordinate) {
            Console.WriteLine($"{coordinate.X} {coordinate.Y}");
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }

    public class Coordinate {
        public Coordinate(int x, int y) {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static Coordinate operator +(Coordinate a, Coordinate b) =>
            new Coordinate(a.X + b.X, a.Y + b.Y);
        
        public static Coordinate operator *(Coordinate a, int number) =>
            new Coordinate(a.X * number, a.Y * number);
    }
}