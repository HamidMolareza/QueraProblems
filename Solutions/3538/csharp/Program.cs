using System;
using System.Collections.Generic;

namespace Quera {
    public static class Program {
        private const int NumOfUsers = 3;
        private const int NumOfWeekDays = 7;
        
        public static void Main() {
            var daysCount = GetDaysCount();
            Console.WriteLine(NumOfWeekDays - daysCount);
        }

        private static int GetDaysCount() {
            var uniqueDays = new HashSet<string>();
            for (var i = 0; i < NumOfUsers; i++) {
                Console.ReadLine(); //Skip
                var days = Console.ReadLine()
                    .Split(' ');
                uniqueDays.AddRange(days);
            }

            return uniqueDays.Count;
        }

        private static void AddRange<T>(this ISet<T> hashList, IEnumerable<T> list) {
            foreach (var item in list) {
                hashList.Add(item);
            }
        }
    }
}