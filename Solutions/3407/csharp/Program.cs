using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const int BombNumber = -1;

        private static readonly List<XY> Neighbors = new List<XY>() {
            new XY(-1, -1),
            new XY(0, -1),
            new XY(1, -1),
            new XY(1, 0),
            new XY(1, 1),
            new XY(0, 1),
            new XY(-1, 1),
            new XY(-1, 0)
        };

        public static void Main() {
            var input = GetInput();

            var resultMatrix = CreateMatrixWithBombs(input)
                .CalculateBombs(input.MatrixSize);

            Print(resultMatrix, input.MatrixSize);
        }

        private static int[,] CreateMatrixWithBombs(Input input) {
            var result = new int[input.MatrixSize.X, input.MatrixSize.Y];
            foreach (var bomb in input.Bombs) {
                result[bomb.X - 1, bomb.Y - 1] = BombNumber;
            }

            return result;
        }

        private static int[,] CalculateBombs(this int[,] matrix, XY matrixSize) {
            for (var row = 0; row < matrixSize.X; row++) {
                for (var column = 0; column < matrixSize.Y; column++) {
                    if (matrix[row, column] == BombNumber)
                        continue;
                    matrix[row, column] = CalculateNeighborBombs(matrix, matrixSize, row, column);
                }
            }

            return matrix;
        }

        private static int CalculateNeighborBombs(int[,] matrix, XY matrixSize, int row, int column) =>
            Neighbors.Select(neighbor => new XY(row + neighbor.X, column + neighbor.Y))
                .Where(neighborCoordinate => IsNeighborExist(matrixSize, neighborCoordinate))
                .Count(neighborCoordinate => matrix[neighborCoordinate.X, neighborCoordinate.Y] == BombNumber);

        private static bool IsNeighborExist(XY matrixSize, XY neighborCoordinate) =>
            neighborCoordinate.X >= 0 && neighborCoordinate.Y >= 0 &&
            neighborCoordinate.X < matrixSize.X && neighborCoordinate.Y < matrixSize.Y;

        private static void Print(int[,] matrix, XY matrixSize) {
            for (var row = 0; row < matrixSize.X; row++) {
                for (var column = 0; column < matrixSize.Y; column++) {
                    Console.Write(matrix[row, column] == BombNumber
                        ? "* "
                        : $"{matrix[row, column]} ");
                }

                Console.WriteLine();
            }
        }

        private static Input GetInput() {
            var result = new Input();

            var matrixSize = GetInputs<int>(' ');
            result.MatrixSize = new XY(matrixSize[0], matrixSize[1]);

            var numOfBombs = GetInputs<int>(1).Single();
            for (var i = 0; i < numOfBombs; i++) {
                var bomb = GetInputs<int>(' ');
                result.Bombs.Add(new XY(bomb[0], bomb[1]));
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

    public class Input {
        public XY MatrixSize { get; set; }
        public List<XY> Bombs { get; set; } = new List<XY>();
    }

    public class XY {
        public XY(int x, int y) {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}