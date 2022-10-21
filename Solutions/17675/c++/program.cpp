// Copy from Quera

// In the name of God

#include <iostream>
using namespace std;

int fib(int n)
{
    if (n <= 1)
        return 1;
    return fib(n - 1) + fib(n - 2);
}

int main()
{
    int n;
    cin >> n;
    for (int i = 1; i <= n; i++)
    {
        int j = 1;
        while (fib(j) < i)
            j++;
        if (fib(j) == i)
            cout << '+';
        else
            cout << '-';
    }
    cout << '\n';
    return 0;
}
