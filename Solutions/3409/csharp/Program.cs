using System;

namespace Quera {
    public static class Program {
        public static void Main() =>
            Console.ReadLine()
                .PrintTable();

        private static void PrintTable(this string str) {
            var number = Convert.ToInt32(str);
            for (var row = 1; row <= number; row++) {
                for (var column = 1; column <= number; column++) {
                    Console.Write(row * column);
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }
    }
}