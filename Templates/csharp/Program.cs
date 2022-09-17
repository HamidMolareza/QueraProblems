using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            //TODO: Use Solution class
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                var item = Convert.ChangeType(Console.ReadLine(), typeof(T)) ?? throw new ArgumentException(
                    $"Can not convert the input of the {i + 1}th line to the requested type ({typeof(T)}).");
                result.Add((T) item);
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList()
            ?? new List<T>();

        private static void PrintLine<T>(this T source) => Console.WriteLine(source);

        private static void PrintLine<TSource, TResult>(
            this TSource source,
            Func<TSource, TResult> func
        ) => Console.WriteLine(func(source));
    }
    
    public static class Solution {
        //TODO: Complete this section
    }
}