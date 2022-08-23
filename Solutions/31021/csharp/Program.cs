using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = Console.ReadLine(); //Ignore
            var persons = GetInputs<string>(' ');

            //Salam
            for (var i = 1; i < persons.Count; i++) {
                for (var j = i - 1; j >= 0; j--) {
                    Console.WriteLine($"{persons[i]}: salam {persons[j]}!");
                }
            }

            //Khodafez
            for (var i = 0; i < persons.Count; i++) {
                Console.WriteLine($"{persons[i]}: khodafez bacheha!");

                for (var j = i + 1; j < persons.Count; j++) {
                    Console.WriteLine($"{persons[j]}: khodafez {persons[i]}!");
                }
            }
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}