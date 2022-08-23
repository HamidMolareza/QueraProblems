using System;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() =>
            Console.ReadLine()
                .ConvertToSingleDigit()
                .Print();

        private static int ConvertToSingleDigit(this string input) {
            while (input.Length > 1) {
                input = input.Select(digit=> digit - '0')
                    .Sum().ToString();
            }

            return Convert.ToInt32(input);
        }

        private static void Print(this int input) => Console.WriteLine(input);
    }
}