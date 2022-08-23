using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(2);
            var n = inputs[0];
            var m = inputs[1];

            var sum = 0.0;
            for (var i = -10; i <= m; i++) {
                for (var j = 1; j <= n; j++) {
                    sum += (int)(Math.Pow(i + j, 3) / Math.Pow(j, 2));
                }
            }
            
            Console.WriteLine(sum);
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