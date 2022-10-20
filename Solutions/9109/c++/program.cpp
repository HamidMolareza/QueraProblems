// Copy from Quera

#include <iostream>

using namespace std;

const int maxn = 100;
int counter[maxn];

int main()
{
    int n;
    cin >> n;
    for (int i = 0; i < n; i++)
    {
        int x;
        cin >> x;
        counter[x - 1]++;
    }

    int mn = -1;
    for (int i = 0; i < maxn; i++)
        if (counter[i] > 0 && (mn == -1 || counter[mn] > counter[i]))
            mn = i;

    cout << mn + 1 << '\n';
}
