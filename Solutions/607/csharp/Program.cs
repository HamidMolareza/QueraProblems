using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = Utility.GetInputs<int>(' ');

            var matrix1 = new Matrix(inputs[0], inputs[1])
                .GetFromConsole();
            var matrix2 = new Matrix(inputs[1], inputs[2])
                .GetFromConsole();

            var result = matrix1 * matrix2;

            result.Print();
        }
    }

    public class Matrix {
        public Matrix(int row, int column) {
            Row = row;
            Column = column;
            Data = new double[Row, Column];
        }

        public int Row { get; }
        public int Column { get; }
        public double[,] Data { get; set; }

        public Matrix GetFromConsole() {
            for (var row = 0; row < Row; row++) {
                var rowData = Utility.GetInputs<double>(' ');
                for (var column = 0; column < Column; column++)
                    Data[row, column] = rowData[column];
            }

            return this;
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2) {
            var result = new Matrix(matrix1.Row, matrix2.Column);

            for (var r = 0; r < matrix1.Row; r++) {
                for (var c = 0; c < matrix2.Column; c++) {
                    var sum = 0.0;
                    for (var k = 0; k < matrix1.Column; k++)
                        sum += matrix1.Data[r, k] * matrix2.Data[k, c];
                    result.Data[r, c] = sum;
                }
            }

            return result;
        }

        public void Print() {
            for (var row = 0; row < Row; row++) {
                for (var column = 0; column < Column; column++)
                    Console.Write($"{Data[row, column]} ");

                Console.WriteLine();
            }
        }
    }

    public static class Utility {
        public static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}