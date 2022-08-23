using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var teacherPeriodsLcm = GetTeacherPeriods()
                .Lcm();

            var dayOfMonth = GetDayOfMonth(teacherPeriodsLcm + 1);

            Console.WriteLine(dayOfMonth);
        }

        /// <summary>
        /// Least Common Multiple
        /// </summary>
        private static long Lcm(this List<int> teacherPeriods) {
            if (teacherPeriods == null)
                throw new ArgumentNullException(nameof(teacherPeriods));
            if (teacherPeriods.Count <= 1)
                return teacherPeriods.FirstOrDefault();

            var lcm = Lcm(teacherPeriods[0], teacherPeriods[1]);
            foreach (var teacherPeriod in teacherPeriods.Skip(2)) {
                lcm = Lcm(lcm, teacherPeriod);
            }


            return lcm;
        }

        private static long Lcm(long a, long b) => (a * b) / Gcd(a, b);

        /// <summary>
        /// Greatest Common Divisor
        /// </summary>
        private static long Gcd(long a, long b) {
            while (b != 0) {
                var remain = a % b;
                a = b;
                b = remain;
            }

            return a;
        }

        private static int GetDayOfMonth(long number) {
            var remain = (int) (number % 30);
            return remain == 0 ? 30 : remain;
        }

        private static List<int> GetTeacherPeriods() {
            var numOfTeachers = GetInputs<int>(1).Single();
            var teacherPeriods = new List<int>(numOfTeachers);

            for (var i = 0; i < numOfTeachers; i++) {
                var input = GetInputs<int>(1).Single();
                teacherPeriods.Add(input);
            }

            return teacherPeriods;
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