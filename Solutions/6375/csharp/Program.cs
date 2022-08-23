using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<double>(' ')
                .GetNumOfMinimumSteps()
                .PrintLine();
        }

        private static int GetNumOfMinimumSteps(this List<double> pots) {
            var waterInEachPot = pots.Average();
            if (waterInEachPot == 0)
                return 0;

            var extraWaters = pots.Where(pot => pot > waterInEachPot)
                .Select(pot => pot - waterInEachPot)
                .Sum();

            return (int) Math.Ceiling(extraWaters / waterInEachPot);
        }

        private static void PrintLine<T>(this T source) => Console.WriteLine(source);

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}