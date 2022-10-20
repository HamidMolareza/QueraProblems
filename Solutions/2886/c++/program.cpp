// Copy from Quera

#include <iostream>
#include <stdio.h>

using namespace std;

int main()
{
    int a, b;
    cin >> a >> b;
    printf("%02d:%02d\n", (12 - a) % 12, (60 - b) % 60);
    return 0;
}
