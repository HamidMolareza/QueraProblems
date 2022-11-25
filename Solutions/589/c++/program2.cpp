// 2018-07-28

#include <iostream>

using namespace std;

//Prototypes:

int main()
{
	int number;
	cin >> number;

	double sum = 1;
	for (size_t i = 2; i <= number; i++)
		sum *= i;
	cout << sum;

	return 0;
}