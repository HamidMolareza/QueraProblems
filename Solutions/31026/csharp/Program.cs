using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInput();

            var commonNode = FindCommonNode(input);
            var minimumSteps = CalculateMinimumSteps(input, commonNode);

            Console.WriteLine(minimumSteps);
        }

        private static string FindCommonNode(Input input) {
            var i = 0;
            for (; i < input.SmallerNode.Length; i++) {
                if (input.SmallerNode[i] != input.BiggerNode[i])
                    break;
            }

            return input.SmallerNode.Substring(0, i);
        }

        private static int CalculateMinimumSteps(Input input, string commonNode) =>
            (input.SmallerNode.Length - commonNode.Length) + (input.BiggerNode.Length - commonNode.Length);

        private static Input GetInput() {
            Console.ReadLine(); //ignore
            var node1 = GetInputs<string>(1).Single();
            Console.ReadLine(); //ignore
            var node2 = GetInputs<string>(1).Single();

            return new Input {
                SmallerNode = node1.Length <= node2.Length ? node1 : node2,
                BiggerNode = node1.Length <= node2.Length ? node2 : node1
            };
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }

    public class Input {
        public string SmallerNode { get; set; }
        public string BiggerNode { get; set; }
    }
}