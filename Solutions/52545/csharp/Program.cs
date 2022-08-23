using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const int NumOfPersons = 4;

        public static void Main() {
            //Get inputs
            var persons = new List<List<int>>(NumOfPersons);
            for (var i = 0; i < NumOfPersons; i++)
                persons.Add(GetInputs<int>(' '));

            var mostSkilledPerson = persons.Select((person, index) =>
                    new KeyValuePair<int, int>(index, person.Max()))
                .OrderByDescending(person=> person.Value)
                .First();

            Console.WriteLine(mostSkilledPerson.Key + 1);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}