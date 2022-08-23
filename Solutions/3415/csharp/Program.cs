using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var numOfGoodMobiles = GetInput()
                .RemoveBadMobiles()
                .Count;

            Console.WriteLine(numOfGoodMobiles);
        }

        private static List<Mobile> GetInput() {
            var numOfMobiles = GetInputs<int>(1).Single();

            var result = new List<Mobile>(numOfMobiles);
            for (var i = 0; i < numOfMobiles; i++) {
                var line = GetInputs<double>(' ');
                result.Add(new Mobile {
                    Price = line[0],
                    Quality = line[1]
                });
            }

            return result;
        }

        private static List<Mobile> RemoveBadMobiles(this IReadOnlyList<Mobile> mobiles) {
            var result = new List<Mobile>(mobiles.Count);

            for (var i = 0; i < mobiles.Count; i++) {
                var isGoodMobile = mobiles.Where((_, j) => j != i)
                    .All(otherMobile =>
                        mobiles[i].Price < otherMobile.Price || mobiles[i].Quality > otherMobile.Quality);

                if (isGoodMobile)
                    result.Add(mobiles[i]);
            }

            return result;
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Mobile {
        public double Price { get; set; }
        public double Quality { get; set; }
    }
}