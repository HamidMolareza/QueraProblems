// 2018-07-02

#include <iostream>

using namespace std;

#define PERSONS_NUMBER 4

int numOfStage(int, int);

int main()
{
	int candy[PERSONS_NUMBER];
	for (size_t i = 0; i < PERSONS_NUMBER; i++)
		cin >> candy[i];

	auto numberOfStage = numOfStage(candy[0], candy[2]);
	int divisor = numberOfStage / PERSONS_NUMBER;
	int remaining = numberOfStage % PERSONS_NUMBER;

	int ate[PERSONS_NUMBER] = { divisor ,divisor ,divisor, divisor };
	for (size_t i = 0; i < remaining; i++)
		ate[i]++;

	for (size_t i = 0; i < PERSONS_NUMBER; i++)
	{
		cout << ate[i];
		if (i != PERSONS_NUMBER - 1) cout << " ";
	}

	return 0;
}


int numOfStage(int first, int third)
{
	if (first == 1) return 1;
	if (third == 1) return 2;
	if (first <= third) return (first - 1) * 2 + 1;
	return third * 2;
}