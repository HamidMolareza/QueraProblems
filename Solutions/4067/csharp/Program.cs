using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        //The object moves in a square path. With the help of these operations, we can find the path of the object.
        private static readonly List<Coordinate> Operations = new List<Coordinate> {
            new Coordinate(1, 1),
            new Coordinate(1, -1),
            new Coordinate(1, 1),
            new Coordinate(-1, 1),
        };

        public static void Main() {
            var inputs = GetInputs();

            var maximumXToCalculate = inputs.Max(coordinate => coordinate.X);
            var maximumYToCalculate = inputs.Max(coordinate => coordinate.Y);

            var route = CalculateRoute(maximumXToCalculate, maximumYToCalculate);

            //If the coordinates are on the path, it returns the number as time, otherwise -1
            var result = inputs.Select(input =>
                route.FindIndex(coordinate => coordinate.X == input.X && coordinate.Y == input.Y));

            Console.WriteLine(string.Join("\n", result));
        }

        private static List<Coordinate> CalculateRoute(int maximumX, int maximumY) {
            var result = new List<Coordinate>() {new Coordinate(0, 0)}; //The starting point

            do {
                foreach (var operation in Operations) {
                    var lastCoordinate = result[result.Count - 1]; //Quera not support result[^1]
                    result.Add(new Coordinate(lastCoordinate.X + operation.X,
                        lastCoordinate.Y + operation.Y));

                    lastCoordinate = result[result.Count - 1];
                    if (lastCoordinate.X >= maximumX && lastCoordinate.Y >= maximumY) {
                        return result;
                    }
                }
            } while (true);
        }

        private static List<Coordinate> GetInputs() {
            var numOfInputs = GetInputs<int>(1).Single();

            var result = new List<Coordinate>(numOfInputs);
            for (var i = 0; i < numOfInputs; i++) {
                var input = GetInputs<int>(' ');
                result.Add(new Coordinate(input[0], input[1]));
            }

            return result;
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

    public class Coordinate {
        public Coordinate(int x, int y) {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}