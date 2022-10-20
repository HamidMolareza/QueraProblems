// Copy from Quera

#include <iostream>
using namespace std;

int main()
{
    int a, b, x;
    cin >> a >> b >> x;
    int ans = 0;
    for (int i = 1; i <= a; i++)
        for (int j = 1; j <= b; j++)
            if (a % i == 0 && b % j == 0 && i + j <= x)
                ans++;
    cout << ans << '\n';
    return 0;
}
