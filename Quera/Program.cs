using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInput();

            var currentState = input.FirstState;
            foreach (var move in input.Moves) {
                if (move.From == currentState) {
                    currentState = move.To;
                }
                else if(move.To == currentState) {
                    currentState = move.From;
                }
            }

            Console.WriteLine(currentState);
        }

        private static Input GetInput() {
            var result = new Input();
            
            var line = GetInputs<string>(' ');
            result.FirstState = line[1];
            var numOfMoves = Convert.ToInt32(line[0]);
            
            for (var i = 0; i < numOfMoves; i++) {
               line = GetInputs<string>(' ');
               result.Moves.Add(new Movement(line[0], line[1]));
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
        public string FirstState { get; set; }
        public List<Movement> Moves { get; set; } = new();
    }

    public class Movement {
        public Movement(string @from, string to) {
            From = @from;
            To = to;
        }
        
        public string From { get; set; }
        public string To { get; set; }
    }
}