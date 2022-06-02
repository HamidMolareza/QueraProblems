using System;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = Console.ReadLine();
            var n = Convert.ToInt32(input);

            var result = 1;
            for (var i= n; i > 1; i--) {
                result *= i;
            }

            Console.WriteLine(result);
        }
    }
}