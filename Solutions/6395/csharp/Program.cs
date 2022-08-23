using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var _ = Console.ReadLine(); //ignore
            var childrenNeeds = GetInputs<long>(' ');

            var needCups = 0L;
            var storage = 0L;
            foreach (var childrenNeed in childrenNeeds) {
                if (childrenNeed > 0) {
                    if (storage >= childrenNeed) {
                        storage -= childrenNeed;
                    }
                    else {
                        needCups += childrenNeed - storage;
                        storage = 0;
                    }
                }
                else {
                    storage += Math.Abs(childrenNeed);
                }
            }

            Console.WriteLine(needCups);
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}