// Copy from Quera

#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;
    int ans = 1;
    while (ans <= n)
        ans *= 2;
    cout << ans;
    return 0;
}