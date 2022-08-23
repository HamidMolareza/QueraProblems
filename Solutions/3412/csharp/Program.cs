using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var persons = GetInput();

            var personsEnterFromSameDoor = persons
                .Where(person => person.Direction != "U") //Up person is not important
                .GroupBy(person => person.Direction)
                .Single(personsGroup => personsGroup.Count() >= 2)
                .ToList();

            switch (personsEnterFromSameDoor.Count) {
                case 2:
                    Console.WriteLine(personsEnterFromSameDoor.First().Name);
                    break;
                case 3:
                    Console.WriteLine(personsEnterFromSameDoor[1].Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(personsEnterFromSameDoor.Count));
            }
        }

        private static IEnumerable<Person> GetInput() {
            var persons = new List<Person>(4);
            for (var i = 0; i < 4; i++) {
                var line = GetInputs<string>(' ');
                persons.Add(new Person(line[0], line[1]));
            }

            return persons;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Person {
        public Person(string name, string direction) {
            Name = name;
            Direction = direction;
        }

        public string Name { get; set; }
        public string Direction { get; set; }
    }
}