// Copy from Quera

// In the name of God

#include <iostream>

using namespace std;

int gcd(long long a, long long b)
{
    int i;
    if (a < b)
        i = a;
    else
        i = b;
    // at least one of 'a' or 'b' is less than 20
    for (; i >= 0; i--)
        if (a % i == 0 && b % i == 0)
            return i;
}

long long lcm(long long a, long long b)
{
    return a * b / gcd(a, b);
}

int main()
{
    int n;
    cin >> n;
    long long answer = 1;
    for (int i = 1; i < n; i++)
        if (gcd(i, n) == 1)
            answer = lcm(answer, i);
    cout << answer;
    return 0;
}
