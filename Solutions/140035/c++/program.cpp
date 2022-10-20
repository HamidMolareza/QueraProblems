// Copy from Quera

#include <iostream>
#include <iomanip>
using namespace std;
int main()
{
    int A, B, C;
    cin >> A >> B >> C;

    if (A % 2 == 0 || (B % 2 == 0 && C % 2 == 0))
    {
        cout << "good";
    }
    else
    {
        cout << "bad";
    }
    return 0;
}