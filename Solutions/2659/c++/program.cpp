// Copy from Quera

#include <iostream>

using namespace std;

int main()
{
    int n;
    cin >> n;

    char s[n];
    for (int i = 0; i < n; i++)
        cin >> s[i];

    char t[n];
    for (int i = 0; i < n; i++)
        cin >> t[i];

    int ans = 0;
    for (int i = 0; i < n; i++)
        if (s[i] != t[i])
            ans++;

    cout << ans << '\n';
    return 0;
}
