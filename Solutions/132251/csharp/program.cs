using System;
using System.Collections.Generic;

class Program {
    static List<string> GetInputs(int numOfLines) {
        var inputs = new List<string>();
        for (int i = 0; i < numOfLines; i++) {
            inputs.Add(Console.ReadLine());
        }
        return inputs;
    }

    static void Main(string[] args) {
        // Get inputs
        const int NUM_OF_INPUTS = 5;
        var inputs = GetInputs(NUM_OF_INPUTS);

        // Find indexes
        var indexes = new List<int>();
        for (int i = 0; i < NUM_OF_INPUTS; i++) {
            if (inputs[i].Contains("FBI")) {
                indexes.Add(i + 1);
            }
        }

        // Print
        if (indexes.Count > 0) {
            Console.WriteLine(string.Join(" ", indexes));
        } else {
            Console.WriteLine("HE GOT AWAY!");
        }
    }
}
