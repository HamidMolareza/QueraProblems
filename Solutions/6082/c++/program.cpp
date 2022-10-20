// Copy from Quera

#include <iostream>

using namespace std;

int main()
{
    int n, m;
    cin >> n >> m;

    char a[n][m];
    for (int i = 0; i < n; i++)
        cin >> a[i];

    int ans = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            if (a[i][j] == '*')
                ans++;

    cout << ans << '\n';
    return 0;
}
