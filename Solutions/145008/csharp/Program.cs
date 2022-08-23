using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfDishes = GetInputs<int>(1).Single();
            var str = GetInputs<string>(1).Single();

            var areComplete = AreComplete(numOfDishes, str);

            Console.WriteLine(areComplete ? "YES" : "NO");
        }

        private static bool AreComplete(int numOfDishes, string str) {
            str = str.ToLower();
            const int completeStateCode = 's' + 'f';

            for (var i = 0; i < numOfDishes; i++) {
                var state = str[i] + str[i + numOfDishes];
                if (state != completeStateCode)
                    return false;
            }

            return true;
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