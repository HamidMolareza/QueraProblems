#include <bits/stdc++.h>

using namespace std;

int main ()
{
    int initial_number, hunts, years;
    cin >> initial_number >> hunts >> years;

    int result = initial_number;

    for (int i = 0; i < years; i++)
    {
        result *= 2;
        result -= hunts;
    }

    cout << result << endl;

    return 0;
}