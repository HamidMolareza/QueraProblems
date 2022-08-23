using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfRooms = GetInputs<int>(1).Single();

            const string separator = "########.......########";
            var result = new StringBuilder(9 * separator.Length); // 9 is number of lines

            var roomNumber = 1;
            for (var i = 0; i < 4; i++) {
                result.AppendLine(separator)
                    .Append('#')
                    .Append(GetRoom(roomNumber, numOfRooms))
                    .Append(".......")
                    .Append(GetRoom(roomNumber + 1, numOfRooms))
                    .AppendLine("#");
                roomNumber += 2;
            }

            result.AppendLine("#######################");

            Console.WriteLine(result);
        }

        private static string GetRoom(int roomNumber, int numOfRooms) =>
            roomNumber > numOfRooms ? "......." : $"ghorfe{roomNumber}";

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}