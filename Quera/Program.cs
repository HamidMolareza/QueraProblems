using System;
using System.Collections.Generic;

namespace Quera {
    public static class Program {
        public static void Main() {
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
        
        private static void AddRange<T>(this ISet<T> hashList, IEnumerable<T> list) {
            foreach (var item in list) {
                hashList.Add(item);
            }
        }
    }
}