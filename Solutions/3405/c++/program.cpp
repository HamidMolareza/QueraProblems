// Copy from Quera

#include <iostream>
using namespace std;

int main()
{
    int num;
    cin >> num;
    int arr[1000];
    int i = 0;
    while (num != 0)
    {
        arr[i] = num;
        cin >> num;
        i++;
    }
    for (int j = i - 1; j >= 0; j--)
    {
        cout << arr[j] << endl;
    }
    return 0;
}