// Copy from Quera

#include <iostream>
using namespace std;

int x_digits[10], current_digits[10];

int main()
{
    int x;
    cin >> x;
    int counter = x;
    while (x)
    {
        x_digits[x % 10]++;
        x = x / 10;
    }
    while (counter <= 10 * 1000 * 1000)
    {
        counter++;
        for (int i = 0; i < 10; i++)
            current_digits[i] = 0;
        int current = counter;
        while (current)
        {
            current_digits[current % 10]++;
            current = current / 10;
        }
        bool flag = false;
        for (int i = 0; i < 10; i++)
            if (current_digits[i] != x_digits[i])
                flag = true;
        if (!flag)
            break;
    }
    if (counter > 10 * 1000 * 1000)
        counter = 0;
    cout << counter << '\n';
    return 0;
}
