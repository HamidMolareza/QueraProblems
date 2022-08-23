using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const int NumOfPersons = 3;

        public static void Main() {
            var prices = GetInputs<int>(' '); //First line

            //The number of people attending is important for us.
            var numOfPeoplePresent = GetNumOfPeoplePresent(NumOfPersons);

            var result = CalculatePrice(numOfPeoplePresent, prices);

            Console.WriteLine(result);
        }

        private static IEnumerable<int> GetNumOfPeoplePresent(int numOfPersons) =>
            GetPresenceTimes(numOfPersons) //For example: 1, 2, 3, 2, 1, 2
                .GroupBy(time => time) // (1,2), (2,3), (3,1)
                .Select(presenceTimes => presenceTimes.Count()); // 2, 3, 1

        private static int CalculatePrice(this IEnumerable<int> numOfPeoplePresent, IReadOnlyList<int> prices) =>
            numOfPeoplePresent.Sum(personPresence => personPresence * prices[personPresence - 1]);

        private static IEnumerable<int> GetPresenceTimes(int numOfPersons) {
            var result = new List<int>();

            for (var i = 0; i < numOfPersons; i++) {
                var range = GetInputs<int>(' '); //For example: 2 6
                var simplification = Simplification(range[0], range[1]); // 2, 3, 4, 5
                result.AddRange(simplification);
            }

            return result;
        }

        private static IEnumerable<int> Simplification(int begin, int end) {
            var result = new List<int>();

            for (var i = begin; i < end; i++) {
                result.Add(i);
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}