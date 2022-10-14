using System.Linq;

namespace Quera {
    public static class Solution2 {
        public static string Solve(string number) {
            for (var i = number.Length - 2; i >= 0; i--) {
                var remainDigits = number.Remove(0, number.Length - i)
                    .Append(number[i])
                    .OrderBy(digit => digit).ToList();
                var minimumDigit = remainDigits.SingleOrDefault(digit => digit > number[i]);
                if (minimumDigit == 0)
                    continue;

                remainDigits.Remove(minimumDigit);
                var temp = number[..i] + minimumDigit + string.Join("", remainDigits);
                return temp;
            }

            return "0";
        }
    }
}