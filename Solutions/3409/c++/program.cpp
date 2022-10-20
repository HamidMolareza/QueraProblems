// Copy from Quera

#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;
    for (int i = 1; i <= n; i++)
    {
        for (int j = 1; j <= n; j++)
            cout << i * j << ' ';
        cout << '\n';
    }
    return 0;
}
