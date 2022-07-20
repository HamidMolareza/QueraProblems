using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private static readonly Person Keyvoon = new Person("keyvoon", "331122");
        private static readonly Person Nezam = new Person("nezam", "123");
        private static readonly Person Shirfarhad = new Person("shir farhad", "2123");

        public static void Main() {
            //Input
            var _ = Console.ReadLine(); //Ignore n
            var keys = GetInputs<string>(1).Single();

            //Process
            var persons = Evaluate(keys, Keyvoon, Nezam, Shirfarhad)
                .OrderByDescending(result => result.Score)
                .ThenBy(result => result.Name)
                .ToList();

            //Output
            var maximumScore = persons.First().Score;
            Console.WriteLine(maximumScore);

            foreach (var person in persons.Where(person => person.Score == maximumScore)) {
                Console.WriteLine(person.Name);
            }
        }

        private static IEnumerable<Person> Evaluate(string keys, params Person[] persons) {
            var result = persons.ToList(); //Get copy of input

            for (var i = 0; i < keys.Length; i++) {
                foreach (var person in result) {
                    var personIndexAnswer = i % person.Pattern.Length;
                    var personAnswer = person.Pattern[personIndexAnswer];
                    if (personAnswer == keys[i]) {
                        person.Score++;
                    }
                }
            }

            return result;
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }

    public class Person {
        public Person(string name, string pattern) {
            Name = name;
            Pattern = pattern;
        }

        public string Name { get; set; }
        public string Pattern { get; set; }
        public int Score { get; set; } = 0;
    }
}