using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs()
                .CanOpenDoor()
                .PrintLine(can => can ? "Boro joloo :)" : "Gir oftadi :(");
        }

        private static Input GetInputs() => new Input {
            Disk1 = GetInputs<int>(' '),
            Disk2 = GetInputs<int>(' '),
        };

        private static bool CanOpenDoor(this Input input) {
            foreach (var state1 in input.Disk1.Move()) {
                var canOpenDoor = input.Disk2.Move()
                    .Any(state2 => CanOpenDoor(state1, state2));
                if (canOpenDoor)
                    return true;
            }

            return false;
        }

        private static IEnumerable<List<int>> Move(this IReadOnlyList<int> disk) {
            for (var i = 0; i < disk.Count; i++)
                yield return disk.Move(i);
        }

        private static List<int> Move(this IReadOnlyList<int> disk, int move) =>
            disk.Select((_, i) => (i + move) % disk.Count)
                .Select(index => disk[index]).ToList();

        private static bool CanOpenDoor(IReadOnlyList<int> disk1, IReadOnlyList<int> disk2) {
            var sum = DigitSum(disk1.GetTargetNumbers(), disk2.GetTargetNumbers());
            return sum % 6 == 0;
        }

        private static List<int> GetTargetNumbers(this IReadOnlyList<int> disk) =>
            new List<int> {disk[1], disk[2], disk[3]};

        private static int DigitSum(IReadOnlyList<int> num1, IReadOnlyList<int> num2) {
            var sum = 0;
            for (var i = 0; i < num1.Count; i++) {
                var digit = (num1[i] + num2[i]) % 10;
                sum = sum * 10 + digit;
            }

            return sum;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();

        private static void PrintLine<TSource, TResult>(
            this TSource source,
            Func<TSource, TResult> func
        ) => Console.WriteLine(func(source));
    }

    internal class Input {
        public List<int> Disk1 { get; set; }
        public List<int> Disk2 { get; set; }
    }
}