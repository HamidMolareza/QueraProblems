using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInput()
                .IsValid()
                .PrintLine(isValid => isValid ? "YES" : "NO");
        }

        private static Input GetInput() {
            var line1 = GetInputs<int>(' ');
            return new Input {
                FirstHalf = 45 + line1[1],
                SecondHalf = 90 + line1[2],
                GoalTimes = GetInputs<int>(' ')
            };
        }

        private static bool IsValid(this Input input) {
            if (input.GoalTimes.Any(goalTime => goalTime < 0 || goalTime > input.SecondHalf))
                return false;

            var indexOfGoalInSecondHalf = input.GoalTimes.FindIndex(goalTime => goalTime > input.FirstHalf);
            return input.GoalTimes.Skip(indexOfGoalInSecondHalf)
                .All(goalTimeInSecondHalf => goalTimeInSecondHalf > input.FirstHalf);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();

        private static void PrintLine<TSource, TResult>(
            this TSource source,
            Func<TSource, TResult> func
        ) => Console.WriteLine(func(source));
    }

    public class Input {
        public int FirstHalf { get; set; }
        public int SecondHalf { get; set; }
        public List<int> GoalTimes { get; set; }
    }
}