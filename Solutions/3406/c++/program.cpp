// Copy from Quera

// In the name of God

#include <iostream>

using namespace std;

int main()
{
    int a, b;
    cin >> a >> b;
    if (a == b)
    {
        cout << a << " = " << b << '\n';
    }
    else
    {
        int reverse_a = (a % 10) * 100 + (a / 10 % 10) * 10 + (a / 100);
        int reverse_b = (b % 10) * 100 + (b / 10 % 10) * 10 + (b / 100);
        if (reverse_a < reverse_b)
            cout << a << " < " << b << '\n';
        else
            cout << b << " < " << a << '\n';
    }
    return 0;
}
