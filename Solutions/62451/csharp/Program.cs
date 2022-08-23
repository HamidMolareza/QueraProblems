using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var ali = GetInputs<int>(2).MapToPerson();
            var torob = GetInputs<int>(2).MapToPerson();

            // ali.V * t + ali.Position = torob.V * t + torob.Position
            // ali.Position - torob.Position = (torob.V - ali.V) * t
            // t = (ali.Position - torob.Position) / (torob.V - ali.V)

            if (torob.V - ali.V == 0) {
                Console.WriteLine("WAIT WAIT");
                return;
            }

            var t = (ali.Position - torob.Position) / (torob.V - ali.V);
            Console.WriteLine(t >= 0 ? "SEE YOU" : "BORO BORO");
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }

        private static Person MapToPerson(this IReadOnlyList<int> inputs) =>
            new Person(inputs[0], inputs[1]);
    }

    public class Person {
        public Person(int position, int v) {
            Position = position;
            V = v;
        }

        public int Position { get; set; }
        public int V { get; set; }
    }
}