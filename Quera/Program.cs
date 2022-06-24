using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(' ');
            var numOfPersons = inputs[0];
            var k = inputs[1];

            var previousPerson = 1;
            var i = 1;
            for (;; i++) {
                var currentPerson = previousPerson + k;
                if (currentPerson > numOfPersons)
                    currentPerson %= numOfPersons;
                
                if (currentPerson == 1)
                    break;
                previousPerson = currentPerson;
            }

            Console.WriteLine(i);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}