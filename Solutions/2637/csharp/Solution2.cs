namespace Quera; 

public static class Solution2 {
    public static int GetMaximumSquares(int n) =>
        (n / 2 + 1) * ((n + 1) / 2 + 1);
}