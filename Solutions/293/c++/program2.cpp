// 2018-07-28

#include <iostream>

using namespace std;

//Prototypes:
bool isPrime(long long);

int main()
{
	long long beginNum, endNum;
	cin >> beginNum >> endNum;

	for (long long i = beginNum; i <= endNum; i++)
	{
		if (isPrime(i))
			cout << i << endl;
	}

	return 0;
}

bool isPrime(long long number)
{
	if (number < 0) throw new invalid_argument("Input is not valid.");
	if (number == 0) return false;
	if (number == 1) return false;
	if (number < 3) return true; //1 2 3 is prime.

	for (long long i = 2; i < number/2+1; i++)
	{
		if (number%i == 0) return false;
	}
	return true;
}