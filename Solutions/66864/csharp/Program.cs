using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quera {
    public static class Program {
        public static void Main() {
            var k = GetInputs<int>(1).Single();

            var numbers = new StringBuilder();
            for (var i = 1;; i++) {
                numbers.Append(i);

                if (numbers.Length >= k) {
                    Console.WriteLine(numbers[k - 1]);
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