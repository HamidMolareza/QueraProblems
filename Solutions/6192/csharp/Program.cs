using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<double>(' '); //a b c d e f
            var firstCube =
                inputs.Take(2) //c is not important because it is the height of the box and we cannot rotate the box.
                    .OrderBy(side => side).ToList();
            var secondCube = inputs.Skip(3).OrderBy(side => side).ToList();

            secondCube.RemoveFirst(side => side <= firstCube[0])
                .RemoveFirst(side => side <= firstCube[1])
                .Count
                .PrintLine(numOfRemainSides => numOfRemainSides == 1
                    ? "zende mimuni"
                    : "dari mimiri");
        }

        private static List<T> RemoveFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            var result = source.ToList(); //create a copy
            var index = result.FindIndex(side => predicate(side));
            if (index >= 0)
                result.RemoveAt(index);
            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();

        private static void PrintLine<TSource, TResult>(
            this TSource source,
            Func<TSource, TResult> func
        ) => Console.WriteLine(func(source));
    }
}