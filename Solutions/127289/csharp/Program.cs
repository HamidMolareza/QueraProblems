using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const string Text = "codecup6";

        public static void Main() {
            var n = GetInputs<int>(1).Single(); //start from 1

            var remain = n % Text.Length;
            Console.WriteLine(remain == 0 ? Text.Last() : Text[remain - 1]);
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