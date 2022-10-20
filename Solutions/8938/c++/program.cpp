// Copy from Quera

#include <iostream>

using namespace std;

int main()
{
    int n, m;
    cin >> n >> m;

    int a[n][n];
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            cin >> a[i][j];

    int ans = 0;
    for (int i = 0; i < m; i++)
    {
        int x, y;
        cin >> x >> y;
        ans += a[x - 1][y - 1];
    }

    cout << ans << '\n';
    return 0;
}
