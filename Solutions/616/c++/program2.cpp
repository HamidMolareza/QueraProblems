// 2018-07-28

#include <iostream>

using namespace std;

//Prototypes:

int main()
{
	long long input;
	cin >> input;

	long long powerBase2 = 1;
	while (powerBase2 < input)
		powerBase2 *= 2;

	cout << powerBase2;

	return 0;
}