using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs();

            var sumOfCosts = inputs.Trips.Sum(trip => inputs.Costs[trip.X - 1][trip.Y - 1]);

            Console.WriteLine(sumOfCosts);
        }

        private static Input GetInputs() {
            var line =  GetInputs<int>(' ');
            var numOfRegions = line[0];
            var numOfTrips = line[1];

            var result = new Input();
            for (var i = 0; i < numOfRegions; i++) 
                result.Costs.Add(GetInputs<int>(' '));
            for (var i = 0; i < numOfTrips; i++) {
                var trip = GetInputs<int>(' ');
                result.Trips.Add(new Trip(trip[0], trip[1]));
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class Input {
        public List<List<int>> Costs { get; set; } = new List<List<int>>();
        public List<Trip> Trips { get; set; } = new List<Trip>();
    }

    public class Trip {
        public Trip(int x, int y) {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}