using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            //Get inputs
            var numOfInputs = GetInputs<int>(1).Single();
            var numbers = GetInputs<double>(numOfInputs);

            //Process
            var max = numbers.Max().ConvertToStandardFormat();
            var min = numbers.Min().ConvertToStandardFormat();
            var average = numbers.Average().ConvertToStandardFormat();

            //Print
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Avg: {average}");
        }

        private static string ConvertToStandardFormat(this double number) =>
            number.TruncateDigits(3).ToString("N3");
        
        private static double TruncateDigits(this double value, int places)
        {
            var integral = Math.Truncate(value);
            var fraction = value - integral;

            var multiplier = Math.Pow(10, places);
            var truncatedFraction = Math.Truncate(fraction * multiplier) / multiplier;

            return integral + truncatedFraction;
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