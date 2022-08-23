using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            //Get inputs
            var line = GetInputs<int>(' '); //returns (numOfTrafficLights, totalDistance)
            var trafficLights = GetTrafficLights(line[0]);

            var requiredTime = CalculateRequiredTime(trafficLights, line[1]);

            Console.WriteLine(requiredTime);
        }

        private static int CalculateRequiredTime(IEnumerable<TrafficLight> trafficLights, int totalDistance) {
            var coveredDistance = 0;
            var time = 0;
            foreach (var trafficLight in trafficLights) {
                time += trafficLight.Distance - coveredDistance; //The time required to reach the traffic light
                time += TimeRequiredLightTurnsGreen(time, trafficLight);
                coveredDistance = trafficLight.Distance;
            }

            time += totalDistance - coveredDistance; //The time required to university 
            return time;
        }

        private static int TimeRequiredLightTurnsGreen(int currentTime, TrafficLight trafficLight) {
            var completeCycle = trafficLight.RedTime + trafficLight.GreenTime;
            var timeInCycle = currentTime % completeCycle;

            return timeInCycle >= trafficLight.RedTime
                ? 0
                : trafficLight.RedTime - timeInCycle;
        }

        private static List<TrafficLight> GetTrafficLights(int numOfTrafficLights) {
            var result = new List<TrafficLight>(numOfTrafficLights);

            for (var i = 0; i < numOfTrafficLights; i++) {
                var input = GetInputs<int>(' ');
                result.Add(new TrafficLight {
                    Distance = input[0],
                    RedTime = input[1],
                    GreenTime = input[2]
                });
            }

            return result;
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }

    public class TrafficLight {
        public int Distance { get; set; }
        public int RedTime { get; set; }
        public int GreenTime { get; set; }
    }
}