using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            // ax + b = 0
            var inputs = GetInputs<int>(' ');
            var a = inputs[0];
            var b = inputs[1];

            if (b == 0) {
                Console.WriteLine(a == 0 ? "infinite" : "unique");
            }
            else {
                Console.WriteLine(a == 0 ? "invalid" : "unique");
            }
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}