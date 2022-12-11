using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var firstLine = Console.ReadLine().Split(' ');
            var numOfCodes = Convert.ToInt32(firstLine[0]);
            var validChars = firstLine[1].Distinct().ToList();

            for (var i = 0; i < numOfCodes; i++) {
                var codeChars = Console.ReadLine().Distinct().ToList();
                var isValid = IsEqual(codeChars, validChars);
                Console.WriteLine(isValid ? "Yes" : "No");
            }
        }

        private static bool IsEqual<T>(IReadOnlyCollection<T> a, IReadOnlyCollection<T> b) =>
            !a.Except(b).Any() && !b.Except(a).Any();
    }
}