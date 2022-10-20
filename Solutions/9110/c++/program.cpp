// Copy from Quera

#include <iostream>

using namespace std;

const int maxn = 100000;
int output[maxn];

int main()
{
    int n = 1;
    output[0] = 1;

    while (n < maxn)
    {
        for (int i = 0; i < n; i++)
            if (i + n < maxn)
                output[i + n] = 1 - output[i];

        n *= 2;
    }

    int l, r;
    cin >> l >> r;

    for (int i = l - 1; i < r; i++)
        cout << output[i];
    cout << '\n';
    return 0;
}
