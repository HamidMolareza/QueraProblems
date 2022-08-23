using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(' ');

            Mirror(inputs[0], inputs[1])
                .Print();
        }

        private static Clock Mirror(int hour, int minute) =>
            new Clock((12 - hour) % 12, (60 - minute) % 60);

        private static void Print(this Clock clock) {
            Console.WriteLine($"{clock.Hour:D2}:{clock.Minute:D2}");
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Clock {
        public Clock(int hour, int minute) {
            Hour = hour;
            Minute = minute;
        }

        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}