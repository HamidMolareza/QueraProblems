using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const int NumbOfEatenPerCycle = 2;
        
        public static void Main() {
            var chocolates = GetInputs<int>(' ');
            
            //Only the first and third chocolates are eaten.
            var first = chocolates[0];
            var third = chocolates[2];

            var cycle = FindMaximumCycle(first, third);
            
            //When n cycles are completed, each person eats up to n chocolate.
            var result = new List<int> {cycle, cycle, cycle, cycle};

            //Number of chocolates eaten in cycles.
            var eatenChocolates = NumbOfEatenPerCycle * cycle;
            
            //Remain chocolates
            first = chocolates[0] - eatenChocolates;
            third = chocolates[2] - eatenChocolates;

            //Continue the cycle until one of the chocolates is finished.
            for (var i = 0; first > 0 && third > 0; i++) {
                result[i]++;
                if (i % 2 == 0) {
                    first--;
                }
                else {
                    third--;
                }
            }

            //Print result
            Console.WriteLine(string.Join(" ", result));
        }

        //2 chocolates are eaten in each cycle. The first time is an exception.
        private static int FindMaximumCycle(int first, int third) =>
            new List<int> {(first - 1) / NumbOfEatenPerCycle, third / NumbOfEatenPerCycle}.Min();

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}