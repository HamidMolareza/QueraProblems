using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
        }

        private static List<int> GetNumbers(int count) {
            var result = new List<int>(count);
            for (var i = 0; i < count; i++) {
                result.Add(Convert.ToInt32(Console.ReadLine()));
            }

            return result;
        }
    }
}