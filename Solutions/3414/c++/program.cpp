// Copy from Quera

// In the name of God

#include <iostream>
using namespace std;

int main()
{
    int x1, y1, x2, y2;
    cin >> x1 >> y1 >> x2 >> y2;

    if (x1 == x2)
        cout << "Vertical\n";
    else if (y1 == y2)
        cout << "Horizontal\n";
    else
        cout << "Try again\n";
    return 0;
}
