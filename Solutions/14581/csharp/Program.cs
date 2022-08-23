using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<int>(1).Single()
                .Calculate()
                .Print();
        }

        private static double Calculate(this int n) {
            var half = n / 2;
            if (n % 2 == 0)
                half -= 1;

            var sum = (Math.Pow(half, 2) + half) / 2;
            sum *= 2;

            if (n % 2 == 0)
                sum += n / 2.0;

            return sum / (n + 1);
        }

        private static void Print(this double result) => Console.WriteLine(result);

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}