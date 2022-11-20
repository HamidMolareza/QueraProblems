using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs();

            inputs.PersonsData
                .Select(personAnswers => CalculateScore(personAnswers, inputs.Keys))
                .Print();
        }

        private static Input GetInputs() {
            var numOfQuestions = GetInputs<int>(1).Single();
            var keys = GetInputs<string>(1).Single();
            var numOfPersons = GetInputs<int>(1).Single();

            var personsData = new List<List<string>>(numOfPersons);
            for (var i = 0; i < numOfPersons; i++)
                personsData.Add(GetInputs<string>(numOfQuestions));

            return new Input {
                Keys = keys,
                PersonsData = personsData
            };
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static int CalculateScore(this IReadOnlyList<string> personAnswers, string keys) {
            var correctAnswers = 0;
            var wrongAnswers = 0;

            for (var i = 0; i < keys.Length; i++) {
                var markedCount = personAnswers[i].Count(a => a == '#');
                if (markedCount < 1)
                    continue;

                if (markedCount > 1 || personAnswers[i][keys[i] - 'A'] != '#')
                    wrongAnswers++;
                else
                    correctAnswers++;
            }

            return (3 * correctAnswers) - wrongAnswers;
        }

        private static void Print(this IEnumerable<int> scores) {
            foreach (var score in scores)
                Console.WriteLine(score);
        }
    }

    public class Input {
        public string Keys { get; set; }
        public List<List<string>> PersonsData { get; set; }
    }
}