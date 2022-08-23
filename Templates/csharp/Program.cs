using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() { }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();

        private static void PrintLine<T>(this T source) => Console.WriteLine(source);

        private static void PrintLine<TSource, TResult>(
            this TSource source,
            Func<TSource, TResult> func
        ) => Console.WriteLine(func(source));
    }
}