using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfKeys = GetInputs<int>(1).Single();

            var result = GetInputs<string>(numOfKeys)
                .GetNameFromKeys(numOfKeys);

            Console.WriteLine(result);
        }

        private static string GetNameFromKeys(this List<string> keys, int numOfKeys) {
            var result = new StringBuilder(numOfKeys);
            var isCapsLock = false;
            foreach (var key in keys) {
                if (key == "CAPS")
                    isCapsLock = !isCapsLock;
                else
                    result.Append(isCapsLock ? key.ToUpper() : key);
            }

            return result.ToString();
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