using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfNames = GetInputs<int>(1).Single();
            
            var maximumChar = GetInputs<string>(numOfNames)
                .Select(name => name.GroupBy(c => c).Count())
                .Max();
            
            Console.WriteLine(maximumChar);
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