// Copy from Quera

#include <iostream>
using namespace std;

int main()
{
    char c1, c2, c3, c4, c5;
    cin >> c1 >> c2 >> c3 >> c4 >> c5;
    int R = 0, Y = 0, G = 0;
    // This piece of code repeats 5 times!
    // 1
    if (c1 == 'R')
        R = R + 1;
    if (c1 == 'Y')
        Y = Y + 1;
    if (c1 == 'G')
        G = G + 1;

    // 2
    if (c2 == 'R')
        R = R + 1;
    if (c2 == 'Y')
        Y = Y + 1;
    if (c2 == 'G')
        G = G + 1;

    // 3
    if (c3 == 'R')
        R = R + 1;
    if (c3 == 'Y')
        Y = Y + 1;
    if (c3 == 'G')
        G = G + 1;

    // 4
    if (c4 == 'R')
        R = R + 1;
    if (c4 == 'Y')
        Y = Y + 1;
    if (c4 == 'G')
        G = G + 1;

    // 5
    if (c5 == 'R')
        R = R + 1;
    if (c5 == 'Y')
        Y = Y + 1;
    if (c5 == 'G')
        G = G + 1;

    if (R >= 3 || (Y >= 2 && R >= 2) || G == 0)
        cout << "nakhor lite\n";
    else
        cout << "rahat baash\n";
    return 0;
}
