// 2018-07-31

#include <iostream>

using namespace std;

//Defines:

//Prototypes:
bool isPrime(long long);
long sumOfNumber(long);

int main()
{
	long inputNum;
	cin >> inputNum;

	auto sum = sumOfNumber(inputNum);
	int counter = 1;
	for (long long i = inputNum + 1;; i++)
	{
		if (!isPrime(i)) continue;

		if (counter < sum)
		{
			counter++;
		}
		else
		{
			cout << i << endl;
			break;
		}
	}

	return 0;
}

bool isPrime(long long number)
{
	if (number <= 1) return false;
	if (number <= 3) return true;

	for (size_t i = 2; i < number/2+1; i++)
		if (number%i == 0)
			return false;
	return true;
}

long sumOfNumber(long number)
{
	long sum = 0;
	while (number>0)
	{
		sum += number % 10;
		number /= 10;
	}
	return sum;
}