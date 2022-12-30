using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            GetInputs()
                .CalculateMinimumTime()
                .PrintLine();
        }

        private static int CalculateMinimumTime(this Inputs inputs) {
            var onlyTrain1 = Math.Abs(inputs.Position1 - inputs.Position2);

            var position1NearestStation = FindNearestStation(inputs.K, inputs.Position1);
            var train1AndTrain2 = Math.Abs(position1NearestStation - inputs.Position1);

            var position2NearestStation = FindNearestStation(inputs.K, inputs.Position2);
            train1AndTrain2 += Math.Abs(position2NearestStation - inputs.Position2);

            var remainDistance = Math.Abs(position2NearestStation - position1NearestStation) / inputs.K;
            train1AndTrain2 += remainDistance;

            return Math.Min(train1AndTrain2, onlyTrain1);
        }

        private static int FindNearestStation(int k, int position) {
            var remain = Math.Abs(position) % k;
            if (remain == 0)
                return position;
            if (Math.Abs(position) % k > k / 2)
                return (int) (k * Math.Round(position * 1.0 / k));
            return k * (position / k);
        }

        private static void PrintLine<T>(this T data) {
            Console.WriteLine(data);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList()
            ?? new List<T>();

        private static Inputs GetInputs() {
            var inputs = GetInputs<int>(' ');
            return new Inputs {
                K = inputs[0],
                Position1 = inputs[1],
                Position2 = inputs[2]
            };
        }
    }

    public class Inputs {
        public int K { get; set; }
        public int Position1 { get; set; }
        public int Position2 { get; set; }
    }
}