using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var result = GetInputs<int>(1).Single()
                .GetMaximumSquares();

            Console.WriteLine(result);
        }

        private static int GetMaximumSquares(this int n) =>
            2 + SumOfSequence(n - 1); // 2 + 2,2,3,3,4,4,5,5,...

        private static int SumOfSequence(int n) {
            if (n < 1) return 0;
            var sequenceValue = GetSequenceValue(n); //Sequence: 2,2,3,3,4,4,5,5,...

            /*
             * If n = 4
             * 2 + 2 + 3 + 3 = 2 * (2 + 3) = 2 * (1 + 2 + 3 - 1)
             * 1 + 2 + 3 + ... = n(n+1)/2
             */
            var result = 2 * (sequenceValue * (sequenceValue + 1) / 2 - 1);
            
            /*
             * If n = 2 --> 2 + 2 = 2(2)
             * But if n is odd like n = 3 ---> 2 + 2 + 3 = (2 * 5) - 3
             */
            if (n.IsOdd())
                result -= sequenceValue;

            return result;
        }

        private static int GetSequenceValue(int n) {
            //Sequence: 2,2,3,3,4,4,5,5,...
            if (n < 1) return 0;
            if (!n.IsOdd())
                n -= 1;
            return (int) (Math.Floor(n / 2.0) + 2);
        }

        private static bool IsOdd(this int number) =>
            number % 2 != 0;

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}