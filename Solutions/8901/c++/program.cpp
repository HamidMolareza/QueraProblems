// Copy from Quera

#include <iostream>
using namespace std;

int main()
{
    int n;
    char x, a, b, ans;
    cin >> n >> x;
    ans = x;
    for (int i = 0; i < n; i++)
    {
        cin >> a >> b;
        if ((ans == a) || (ans == b))
        {
            if (ans != a)
                ans = a;
            else
                ans = b;
        }
    }
    cout << ans;
    return 0;
}
