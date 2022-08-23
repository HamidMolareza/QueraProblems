using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        public static void Main() {
            var inputs = GetInputs<int>(' ');

            var binaryStr = CreateBinaryString(inputs[1]);
            var result = binaryStr.Substring(inputs[0] - 1, inputs[1] - inputs[0] + 1);

            Console.WriteLine(result);
        }

        private static string CreateBinaryString(int maximumLength) {
            var result = "1";
            
            while (result.Length < maximumLength) 
                result += result.Reverse();
            
            return result;
        }

        private static string Reverse(this string binaryString) =>
            string.Join(string.Empty, binaryString.Select(c => c == '0' ? '1' : '0'));

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}