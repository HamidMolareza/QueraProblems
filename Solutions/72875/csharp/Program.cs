using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            Console.ReadLine(); //ignore n
            var cells = GetInputs<int>(' ');

            for (var i = 1; i < cells.Count - 1; i++) {
                if (cells[i] <= cells[i - 1] || cells[i] <= cells[i + 1])
                    continue;
                Console.WriteLine("Ey baba :(");
                return;
            }

            Console.WriteLine("Bah Bah! Ajab jooji!");
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}