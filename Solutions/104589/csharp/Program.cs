using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var n = GetInputs<int>(1).Single();

            for (var i = 2; i <= n; i++) {
                if (n % i == 0) {
                    Console.WriteLine(n / i);
                    break;
                }
            }
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