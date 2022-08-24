using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfPersons = GetInputs<int>(1).Single();
            var persons = GetInputs<string>(numOfPersons);

            for (var i = 0; i < persons.Count - 1; i++) {
                Console.WriteLine(
                    $"{persons[i]} to {persons[i + 1]}: ke ba in dar agar dar bande dar manand, dar manand.");

                for (var j = i + 1; j > 0; j--)
                    Console.WriteLine($"{persons[j]} to {persons[j - 1]}: dar manand?");

                for (var j = 0; j < i + 1; j++)
                    Console.WriteLine($"{persons[j]} to {persons[j + 1]}: dar manand.");
            }
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