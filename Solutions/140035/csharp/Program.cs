using System;

namespace Quera {
    public static class Program {
        public static void Main() {
            var number = Convert.ToInt32(Console.ReadLine());

            if (IsOdd(number) && IsPrime(number)) {
                Console.WriteLine("zoj");
            }
            else {
                Console.WriteLine("fard");
            }
        }

        private static bool IsPrime(int number) {
            if (number < 2) return false;
            if (number < 4) return true;

            for (var i = 2; i < Math.Sqrt(number) + 1; i++) {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private static bool IsOdd(int number) => number % 2 != 0;
    }
}