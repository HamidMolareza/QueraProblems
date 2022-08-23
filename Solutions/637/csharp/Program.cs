using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfNumbers = GetInputs<int>(1).Single();
            var numbers = GetInputs<string>(numOfNumbers);

            var result = Sum(numbers);

            Console.WriteLine(result);
        }

        private static string RemoveBeginZeroes(this string number) {
            if (string.IsNullOrWhiteSpace(number))
                return "0";

            var result = number.TrimStart('0');
            return result.Any() ? result : "0";
        }

        private static string Sum(IEnumerable<string> numbers) {
            var sum = "0";
            sum = numbers.Aggregate(sum, Sum);

            return sum;
        }

        private static string Sum(string num1, string num2) {
            var biggerLength = num1.Length >= num2.Length ? num1 : num2;
            var smallerLength = num1.Length >= num2.Length ? num2 : num1;

            var result = new List<int>(Enumerable.Repeat(0, biggerLength.Length + 1));
            var remain = 0;
            for (var i = 0; i < biggerLength.Length; i++) {
                var biggerIndex = biggerLength.Length - i - 1;
                var smallerIndex = smallerLength.Length - i - 1;
                
                var digit1 = biggerLength[biggerIndex] - '0';
                var digit2 = smallerIndex >= 0 ? smallerLength[smallerIndex] - '0' : 0;

                var digitsSum = digit1 + digit2 + remain;
                remain = digitsSum >= 10 ? 1 : 0;
                digitsSum %= 10;

                result[biggerIndex + 1] = digitsSum;
            }

            if (remain > 0)
                result[0] = 1;

            return string.Join(string.Empty, result).RemoveBeginZeroes();
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