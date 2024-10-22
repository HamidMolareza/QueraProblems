using System;

namespace Quera {
    public static class Program {
        public static void Main() =>
            Console.ReadLine()
                .IsDangerous()
                .PrintLine(isDangerous => isDangerous ? "nakhor lite" : "rahat baash");

        private static bool IsDangerous(this string healthLabel) =>
            CountLabel(healthLabel)
                .IsDangerous();

        private static LabelCount CountLabel(this string healthLabel) {
            var labelCount = new LabelCount();
            foreach (var item in healthLabel) {
                switch (item) {
                    case 'R':
                        labelCount.NumOfReds += 1;
                        break;
                    case 'G':
                        labelCount.NumOfGreens += 1;
                        break;
                    case 'Y':
                        labelCount.NumOfYellows += 1;
                        break;
                }
            }

            return labelCount;
        }

        private static bool IsDangerous(this LabelCount labelCount) {
            if (labelCount.NumOfReds >= 3 || labelCount.NumOfGreens == 0)
                return true;
            return labelCount.NumOfReds >= 2 && labelCount.NumOfYellows >= 2;
        }


        private static void PrintLine<T>(this T @this, Func<T, string> func) {
            Console.WriteLine(func(@this));
        }
    }

    public class LabelCount {
        public int NumOfReds { get; set; }
        public int NumOfGreens { get; set; }
        public int NumOfYellows { get; set; }
    }
}