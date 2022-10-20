// Copy from Quera

#include <iostream>
using namespace std;

int main()
{
    int n, x, y;
    cin >> n >> x >> y;
    for (int i = 0; i * x <= n; i++)
        if ((n - i * x) % y == 0)
        {
            cout << i << ' ' << (n - i * x) / y << '\n';
            return 0;
        }
    cout << "-1\n";
    return 0;
}
