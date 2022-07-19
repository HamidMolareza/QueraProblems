using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(2);

            ShowFibNth(inputs[0], inputs[1]);
        }

        private static void ShowFibNth(long n1, long n2) {
            if (n1 == 0)
                return;
            Console.WriteLine(n1);
            ShowFibNth(n2 - n1, n1);
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