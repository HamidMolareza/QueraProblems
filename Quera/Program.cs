using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
          var sizeOfTShirts =  GetInputs<int>(' ');
          var sizeOfTHumans =  GetInputs<int>(' ');

          Console.WriteLine(IsSuitable(sizeOfTShirts, sizeOfTHumans) ? "yes" : "no");
        }

        private static bool IsSuitable(IEnumerable<int> sizeOfTShirts, IReadOnlyList<int> sizeOfTHumans) =>
            !sizeOfTShirts.Where((tShirts, index) => tShirts < sizeOfTHumans[index]).Any();

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}