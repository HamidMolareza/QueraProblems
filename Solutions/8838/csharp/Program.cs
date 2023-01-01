using System;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = Console.ReadLine().Split(' ');

            var repeat = Convert.ToInt32(inputs[0]);
            for (var i = 0; i < repeat; i++)
                Console.Write("copy of ");
            Console.WriteLine(inputs[1]);
        }
    }
}