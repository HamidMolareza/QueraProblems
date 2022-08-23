using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var date1 = GetDate().ConvertToDays();
            var date2 = GetDate().ConvertToDays();

            Console.WriteLine(date2 - date1);
        }

        private static int ConvertToDays(this Date date) {
            if (date.Month <= 6)
                return (date.Month - 1) * 31 + date.Day;

            return (6 * 31) + (date.Month - 6 - 1) * 30 + date.Day;
        }

        private static Date GetDate() {
            var inputs = GetInputs<int>(' ');
            return new Date(inputs[0], inputs[1]);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Date {
        public Date(int month, int day) {
            Month = month;
            Day = day;
        }

        public int Month { get; set; }
        public int Day { get; set; }
    }
}