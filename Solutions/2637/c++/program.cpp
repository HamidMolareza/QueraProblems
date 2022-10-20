// Copy from Quera

// In the name of God

#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;
    // ceil(a/b) is equal to floor((a+b-1)/b)
    cout << (n / 2 + 1) * ((n + 1) / 2 + 1) << '\n';
    return 0;
}
