using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Quera {
    public static class Program {
        public static void Main() {
            var input = GetInputs<string>(1).Single()
                .TrimStart('=');

            var rgx = new Regex("[=]+");
            var matches = rgx.Matches(input);

            var str = new StringBuilder(input);
            var movedChars = 0;
            foreach (Match match in matches) {
                //Normally, we have to remove from match.Index to match.Length.
                //But with each removing of characters, the new match.Index must be moved as much as the changes.
                //That's why we delete from match.Index - movedChars to match.Length.

                //In this problem, one character must be removed for each =.
                //Therefore, two (match.Length * 2) characters are removed for each =.
                var startIndex = match.Index - movedChars - match.Length;
                var length = match.Length * 2;

                if (startIndex < 0) {
                    //If startIndex is negative, it means that the number of = is more than the number of characters.
                    length += startIndex;
                    startIndex = 0;
                }

                str.Remove(startIndex, length);
                movedChars += length;
            }

            Console.WriteLine(str);
        }

        private static List<T> GetInputs<T>(int count) {
            var result = new List<T>(count);
            for (var i = 0; i < count; i++) {
                result.Add((T) Convert.ChangeType(Console.ReadLine(), typeof(T)));
            }

            return result;
        }
    }
}