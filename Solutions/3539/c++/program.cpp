// 2016-12-29

#include <iostream>

using namespace std;

//Prototypes:
int convertToDigit(long long number);
long long sumDigits(long long number);

int main()
{
	long long number;
	cin >> number;

	cout << convertToDigit(number); //Convert 29 to 2

	return 0;
}


int convertToDigit(long long number)
{
	while (number > 9)
		number = sumDigits(number);
	return number;
}


long long sumDigits(long long number)
{
	long long sum = 0;
	while (number > 0)
	{
		sum += number % 10;
		number /= 10;
	}
	return sum;
}