using System;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = Console.ReadLine();
            var n = Convert.ToInt32(input);

            Console.Write("W");
            for (var i = 0; i < n; i++) {
                Console.Write("o");
            }

            Console.Write("w!");
        }
    }
}