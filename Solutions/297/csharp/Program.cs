using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var x = GetInputs<int>(1).Single();
            var n = GetInputs<int>(1).Single();

            var sum = 1.0;
            for (var i = 1; i < n; i++)
                sum += Math.Pow(x, i) / Factorial(i);
            
            Console.WriteLine(sum.ToString("0.000"));
        }

        private static double Factorial(int number) {
            var result = 1.0;
            for (var i = number; i > 1; i--)
                result *= i;
            return result;
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}