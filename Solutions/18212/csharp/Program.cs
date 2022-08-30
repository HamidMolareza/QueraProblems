using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs<string>(1).Single()
                .CalculateAmountOfElectricity()
                .PrintLine();
        }

        private static long CalculateAmountOfElectricity(this string numberInScientificNotation) =>
            numberInScientificNotation
                .ParseScientificNotation()
                .CalculateAmountOfElectricity();

        private static ParsedScientificNotation ParseScientificNotation(this string numberInScientificNotation) {
            var splitsBaseE = numberInScientificNotation.Split('e'); //2.3e10
            if (splitsBaseE.Length == 1) {
                //It has only integer part like 9
                return new ParsedScientificNotation {
                    IntegerPart = numberInScientificNotation,
                    NumOfZeroes = 0
                };
            }

            var splitsBaseDot = splitsBaseE[0].Split('.'); //2.3
            if (splitsBaseDot.Length == 1) {
                //It has only integer part like 9
                return new ParsedScientificNotation {
                    IntegerPart = splitsBaseDot[0],
                    NumOfZeroes = Convert.ToInt32(splitsBaseE[1])
                };
            }

            //Convert 2.3e10 to (23, 9)
            return new ParsedScientificNotation {
                NumOfZeroes = Convert.ToInt32(splitsBaseE[1]) - splitsBaseDot[1].Length,
                IntegerPart = string.Join(string.Empty, splitsBaseDot)
            };
        }

        private static long CalculateAmountOfElectricity(this ParsedScientificNotation parsedScientificNotation) {
            if (Convert.ToInt32(parsedScientificNotation.IntegerPart) == 0)
                return GetRequiredElectricity('0'); // 0 * (10 ^ anything) = 0

            return parsedScientificNotation.NumOfZeroes * GetRequiredElectricity('0')
                   + parsedScientificNotation.IntegerPart.Sum(digit => GetRequiredElectricity(digit));
        }

        private static int GetRequiredElectricity(int digit) {
            switch (digit) {
                case '0':
                    return 6;
                case '1':
                    return 2;
                case '2':
                case '3':
                case '5':
                    return 5;
                case '4':
                    return 4;
                case '6':
                case '9':
                    return 6;
                case '7':
                    return 3;
                case '8':
                    return 7;
            }

            throw new ArgumentOutOfRangeException(nameof(digit));
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static void PrintLine<T>(this T source) => Console.WriteLine(source);
    }

    public class ParsedScientificNotation {
        public string IntegerPart { get; set; }
        public int NumOfZeroes { get; set; }
    }
}