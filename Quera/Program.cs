using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const int NumOfInputsPerCities = 2;

        public static void Main() {
            var cities = GetInputs(3);
            var result = cities.Select(city => city.Min()).Sum();
            Console.WriteLine(result);
        }

        private static List<List<int>> GetInputs(int numOfCities) {
            var result = new List<List<int>>(numOfCities);
            for (var i = 0; i < numOfCities; i++) {
                result.Add(GetInputs<int>(NumOfInputsPerCities));
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
}