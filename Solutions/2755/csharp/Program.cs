using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInput()
                .GetKeysMustBeBroken()
                .Print();
        }

        private static Result GetKeysMustBeBroken(this Keyboard keyboard) {
            if (keyboard.BrokenKeys.Count.IsOdd())
                return new Result {IsPossible = true}; //It is not necessary to break a new key
            if (keyboard.Rows * keyboard.Columns - keyboard.BrokenKeys.Count < 1)
                return new Result {IsPossible = false}; //The key must be broken, but it is not possible.

            //A key must be broken. Now we have to find its position.
            for (var row = 0; row < keyboard.Rows; row++) {
                for (var column = 0; column < keyboard.Columns; column++) {
                    if (!IsFree(row + 1, column + 1, keyboard.BrokenKeys))
                        continue;
                    return new Result {
                        IsPossible = true,
                        Positions = new List<Position> {
                            new Position {
                                X = row + 1,
                                Y = column + 1
                            }
                        }
                    };
                }
            }

            return new Result {IsPossible = false};
        }

        private static bool IsFree(int row, int column, IEnumerable<Position> keyboardBrokenKeys) =>
            !keyboardBrokenKeys.Any(brokenKey => brokenKey.X == row && brokenKey.Y == column);

        private static bool IsOdd(this int number) => number % 2 != 0;

        private static void Print(this Result result) {
            if (!result.IsPossible) {
                Console.WriteLine("-1");
                return;
            }

            Console.WriteLine(result.Positions.Count);
            foreach (var position in result.Positions)
                Console.WriteLine($"{position.X} {position.Y}");
        }

        private static Keyboard GetInput() {
            var line = GetInputs<int>(' '); //row, column, numOfBrokenKeys
            var brokenKeys = new List<Position>(line[2]);

            for (var i = 0; i < line[2]; i++) {
                var position = GetInputs<int>(' ');
                brokenKeys.Add(new Position {
                    X = position[0],
                    Y = position[1]
                });
            }

            return new Keyboard {
                Rows = line[0],
                Columns = line[1],
                BrokenKeys = brokenKeys
            };
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Keyboard {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<Position> BrokenKeys { get; set; } = new List<Position>();
    }

    public class Position {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Result {
        public bool IsPossible { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
    }
}