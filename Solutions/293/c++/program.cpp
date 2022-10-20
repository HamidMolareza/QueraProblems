// Copy from Quera

#include <iostream>
using namespace std;

int is_prime(int num)
{
    if (num == 1)
        return 0;
    for (int i = 2; i < num; i++)
    {
        if (num % i == 0)
            return 0;
    }
    return 1;
}

int main()
{
    int a, b;
    cin >> a >> b;
    for (int i = a; i <= b; i++)
    {
        if (is_prime(i) == 1)
            cout << i << endl;
    }
    return 0;
}