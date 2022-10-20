// Copy from Quera

#include <iostream>
using namespace std;

int main()
{
    long long fact = 1;
    int n;
    cin >> n;
    for (int i = 0; i < n; i++)
        fact *= i + 1;
    cout << fact << "\n";
    return 0;
}