using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var capacity = GetInputs<int>(' ').Skip(1).Single();
            var numOfGroupMembers = GetInputs<int>(' ');

            var minimumShots = 0;
            var sum = 0;
            foreach (var numOfGroupMember in numOfGroupMembers) {
                sum += numOfGroupMember;
                if (sum <= capacity)
                    continue;
                sum = numOfGroupMember;
                minimumShots++;
            }

            if (sum > 0)
                minimumShots++;

            Console.WriteLine(minimumShots);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}