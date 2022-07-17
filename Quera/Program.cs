using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public class Worker {
        public List<int> WorkDays { get; } = new List<int>();
    }

    public static class Program {
        public static void Main() {
            var numOfCommonDays = GetWorkers()
                .Select(worker => worker.WorkDays)
                .GetCommonList()
                .Count();

            Console.WriteLine(numOfCommonDays);
        }

        private static IEnumerable<Worker> GetWorkers() {
            var numOfWorkerInputs = GetInputs<int>(' ');

            var workers = numOfWorkerInputs.Select(GetWorker);

            return workers;
        }

        private static Worker GetWorker(int numOfWorkerInput) {
            var worker = new Worker();
            for (var i = 0; i < numOfWorkerInput; i++) {
                var dayRange = GetInputs<int>(' ');

                var workDays = GetWorkDays(dayRange[0], dayRange[1]);
                worker.WorkDays.AddRange(workDays);
            }

            return worker;
        }

        private static IEnumerable<int> GetWorkDays(int begin, int end) {
            var count = end - begin + 1;
            return Enumerable.Range(begin, count);
        }

        private static IEnumerable<T> GetCommonList<T>(this IEnumerable<IEnumerable<T>> input) {
            var list = input.ToList();
            if (list.Count < 2) return list.FirstOrDefault();

            var result = list[0].Intersect(list[1]);
            for (var i = 2; i < list.Count; i++) {
                result = result.Intersect(list[i]);
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}