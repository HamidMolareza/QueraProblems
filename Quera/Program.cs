using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInputs();

            var numOfNimbo = 0;
            for (var row = 1; row < input.NumOfRows - 1; row++) {
                for (var column = 1; column < input.NumOfColumns - 1; column++) {
                    if (IsNimbo(input.Table, row, column))
                        numOfNimbo++;
                }
            }

            Console.WriteLine(numOfNimbo);
        }

        private static bool IsNimbo(IReadOnlyList<List<int>> table, int row, int column) {
            if (row == 0 || row == table.Count || column == 0 || column == table[0].Count)
                return false;

            var targetCell = table[row][column];
            if ((targetCell > table[row - 1][column] && targetCell > table[row + 1][column])
                && (targetCell < table[row][column - 1] && targetCell < table[row][column + 1]))
                return true;

            return (targetCell < table[row - 1][column] && targetCell < table[row + 1][column])
                   && (targetCell > table[row][column - 1] && targetCell > table[row][column + 1]);
        }

        private static Input GetInputs() {
            var tableData = GetInputs<int>(' ');

            var table = new List<List<int>>(tableData[0]);
            for (var i = 0; i < tableData[0]; i++)
                table.Add(GetInputs<int>(' '));

            return new Input {
                NumOfRows = tableData[0],
                NumOfColumns = tableData[1],
                Table = table
            };
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Input {
        public int NumOfRows { get; set; }
        public int NumOfColumns { get; set; }
        public List<List<int>> Table { get; set; } = new List<List<int>>();
    }
}