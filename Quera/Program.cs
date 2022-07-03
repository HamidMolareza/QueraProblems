using System;
using System.Collections.Generic;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<string>(3);

            var result = Operation(inputs[0], inputs[2], inputs[1]);

            Console.WriteLine(result);
        }

        private static string Operation(string num1, string num2, string op) {
            switch (op) {
                case "+":
                    return Sum(num1, num2);
                case "*":
                    var numOfZeroes = num1.GetNumOfZeroes() + num2.GetNumOfZeroes();
                    return "1" + new string('0', numOfZeroes);
            }

            throw new ArgumentOutOfRangeException(nameof(op));
        }

        private static string Sum(string num1, string num2) {
            var biggerNumber = num1.Length >= num2.Length ? num1 : num2;
            var smallerNumber = num1.Length < num2.Length ? num1 : num2;

            //The numbers are Pow(10, x) like 1000,
            //then for 2 numbers we have only 2 non-zero value,
            //Therefore, it is enough to know to which index we should add
            //the non-zero number (from smaller number to bigger number).

            var index = biggerNumber.Length - (smallerNumber.Length - smallerNumber.IndexOf('1'));
            return biggerNumber.Add(index, 1);
        }

        private static string Add(this string number, int index, int value) =>
            number.Substring(0, index)
            + (ConvertToNumericalEquivalent(number[index]) + value)
            + number.Remove(0, index + 1);

        private static int GetNumOfZeroes(this string number) =>
            number.Length - 1; //number is Pow(10, x) like 10000

        private static int ConvertToNumericalEquivalent(char c) => c - '0';

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}