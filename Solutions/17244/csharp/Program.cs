using System;

namespace Quera {
    public static class Program {
        public static void Main() {
            var n = Convert.ToInt32(Console.ReadLine());

            var result = n * (n + 1) / 2;
            Console.WriteLine(result);
        }
    }
}