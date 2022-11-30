// Copy from Quera

#include <iostream>
using namespace std;

const int MAXN = 100000;
int cnt[MAXN];
int ans = 0 ;

int main()
{
    int a,b,c;
    cin >> a >> b >> c;
    for (int i = 0 ; i < 3 ; i++)
    {
        int x,y;    cin >> x >> y;
        for (int j = x ; j < y ; j++)
        {
            cnt[j]++;
        }
    }
    for (int i = 0 ; i < MAXN ; i++)
    {
        if (cnt[i] == 1)
        {
            ans += a;
        }
        else if (cnt[i] == 2)
        {
            ans += b * 2;
        }
        else if (cnt[i] == 3)
        {
            ans += c * 3;
        }
    }
    cout << ans << endl;
}