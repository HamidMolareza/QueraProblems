using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<long>(1).Single()
                .CanSuccess()
                .PrintLine(canSuccess => canSuccess ? "Yes" : "No");
        }

        private static bool CanSuccess(this long n) {
            if (n == 1) return true;

            var sequence = new List<long>(); //For detect loop/cycle
            while (true) {
                if (n % 2 == 0)
                    n /= 2;
                else
                    n = 3 * n + 3;

                if (n == 1)
                    return true;
                if (sequence.Exists(number => number == n))
                    return false; //A loop is detected so it never can.

                sequence.Add(n);
            }
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static void PrintLine<TSource, TResult>(this TSource source, Func<TSource, TResult> func)
            => Console.WriteLine(func(source));
    }
}