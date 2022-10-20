// Copy from Quera

#include <iostream>

using namespace std;

int main()
{
    long long int k, a, b, m, ans = 0;
    cin >> k >> a >> b;
    if (a > b)
        swap(a, b);
    m = b - a;
    if (abs(a % k) < k - abs(a % k))
    {
        ans += abs(a % k);
        if (a > 0)
            a -= abs(a % k);
        else
            a += abs(a % k);
    }
    else
    {
        ans += k - abs(a % k);
        if (a > 0)
            a += k - abs(a % k);
        else
            a -= k - abs(a % k);
    }
    if (abs(b % k) <= k - abs(b % k))
    {
        ans += abs(b % k);
        if (b > 0)
            b -= abs(b % k);
        else
            b += abs(b % k);
    }
    else
    {
        ans += k - abs(b % k);
        if (b > 0)
            b += k - abs(b % k);
        else
            b -= k - abs(b % k);
    }
    ans += (b - a) / k;
    ans = min(ans, m);
    cout << ans << endl;
    return 0;
}
