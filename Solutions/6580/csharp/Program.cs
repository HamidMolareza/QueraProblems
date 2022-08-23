using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var squareCoordinate = GetInputs<int>(' '); //x y
            var squareLength = GetInputs<int>(1).Single();
            var glassCoordinate = GetInputs<int>(' '); //x y

            var minimumX = squareCoordinate[0];
            var maximumX = squareCoordinate[0] + squareLength;
            var minimumY = squareCoordinate[1] - squareLength;
            var maximumY = squareCoordinate[1];

            if (glassCoordinate[0] >= minimumX && glassCoordinate[0] <= maximumX
                                               && glassCoordinate[1] >= minimumY && glassCoordinate[1] <= maximumY) {
                Console.WriteLine("Mahdi");
            }
            else {
                Console.WriteLine("Parsa");
            }
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
}