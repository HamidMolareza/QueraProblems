using System.Linq;

namespace Quera {
    public static class Solution2 {
        public static string Solve(string number) {
            for (var i = number.Length - 2; i >= 0; i--) {
                var remainDigits = number.Remove(0, i)
                    .OrderBy(digit => digit).ToList();
                var minimumDigit = remainDigits.FirstOrDefault(digit => digit > number[i]);
                if (minimumDigit == 0)
                    continue;

                remainDigits.Remove(minimumDigit);
                return number.Substring(0, i) + minimumDigit + string.Join("", remainDigits);
            }

            return "0";
        }
    }
}