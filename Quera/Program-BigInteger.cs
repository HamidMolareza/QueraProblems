using System;
using System.Collections.Generic;
using System.Numerics;

namespace Quera {
    public static class ProgramBigInteger {
        // public static void Main() {
        //     var inputs = GetInputs<string>(3);
        //
        //     var result = Operation(BigInteger.Parse(inputs[0]), BigInteger.Parse(inputs[2]), inputs[1]);
        //
        //     Console.WriteLine(result);
        // }

        private static BigInteger Operation(BigInteger num1, BigInteger num2, string op) {
            switch (op) {
                case "+":
                    return BigInteger.Add(num1, num2);
                case "*":
                    return num1 * num2;
            }

            throw new ArgumentOutOfRangeException(nameof(op));
        }

        // private static List<T> GetInputs<T>(int count) {
        //     var result = new List<T>(count);
        //     for (var i = 0; i < count; i++) {
        //         result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
        //     }
        //
        //     return result;
        // }
    }
}