using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfInputs = GetInputs<int>(1).Single();
            var inputs = new List<List<int>>(numOfInputs);
            for (var i = 0; i < numOfInputs; i++) 
                inputs.Add(GetInputs<int>(' '));

            foreach (var input in inputs) {
                var sumOfPerspolisGoals = input[0] + input[2];
                var sumOfEsteghlalGoals = input[1] + input[3];

                if (sumOfPerspolisGoals > sumOfEsteghlalGoals) {
                    Console.WriteLine("perspolis");
                }
                else if (sumOfPerspolisGoals < sumOfEsteghlalGoals) {
                    Console.WriteLine("esteghlal");
                }
                else {
                    if (input[1] < input[2]) {
                        Console.WriteLine("perspolis");
                    }
                    else if (input[1] > input[2]) {
                        Console.WriteLine("esteghlal");
                    }
                    else {
                        Console.WriteLine("penalty");
                    }
                }
            }
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
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