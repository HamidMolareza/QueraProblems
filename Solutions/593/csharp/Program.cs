using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private static int _checkedPrimeNumbers = 22;
        private static readonly List<int> PrimeNumbers = new List<int>() {2, 3, 5, 7, 11, 13, 17, 19};

        public static void Main() {
            var n = GetInputs<string>(1).Single();
            var b = n.Sum(digit => digit - '0'); //sum of digits.

            var startIndex = Convert.ToInt32(n) + 1;
            var result = GetPrimeNumber(startIndex, b - 1);
            
            Console.WriteLine(result);
        }

        private static int GetPrimeNumber(int startIndex, int skip = 0) {
            var counter = 0;
            for (var i = startIndex;; i++) {
                if (!IsPrime(i))
                    continue;

                if (counter < skip)
                    counter++;
                else
                    return i;
            }
        }

        private static bool IsPrime(int number) {
            if (number <= _checkedPrimeNumbers) {
                //If number is prime, it must exist in list.
                return PrimeNumbers.Exists(primeNumber =>
                    primeNumber == number);
            } 

            var maximumCheck = (int)Math.Sqrt(number) + 1;
            if (maximumCheck <= _checkedPrimeNumbers) {
                //We do not know if (number) is prime or not,
                //but we have prime numbers up to (maximumCheck).
                //So it is enough to calculate the divisibility of (number) to the existing prime numbers.

                return PrimeNumbers.TakeWhile(primeNumber => primeNumber <= maximumCheck)
                    .All(primeNumber => number % primeNumber != 0);
            }

            //We do not have prime numbers up to (maximumCheck),
            //so first we examine whether (number) is divisible by the existing prime numbers or not.
            //If it was divisible, it is not the prime. But if not,
            //we have to calculate the prime numbers to (maximumCheck) and check again.
            //This calculation is useful for future optimizations.
            
            if (PrimeNumbers.Any(primeNumber => number % primeNumber == 0))
                return false;

            CalculatePrimeNumbers(_checkedPrimeNumbers + 1, maximumCheck);

            //We now have enough information to evaluate. This method is called only once.
            return IsPrime(number);
        }

        private static void CalculatePrimeNumbers(int begin, int end) {
            for (var i = begin; i <= end; i++) {
                if (IsPrime(i)) 
                    PrimeNumbers.Add(i);
                _checkedPrimeNumbers = i;
            }
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