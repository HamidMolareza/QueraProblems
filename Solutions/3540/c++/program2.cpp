// 2018-07-28

#include <iostream>

using namespace std;

#define X_COUNT 2

int* solve(int*, int, int);

int main()
{
	int answer;
	cin >> answer;

	int coefficient[X_COUNT];
	for (int i = 0; i < X_COUNT; i++)
		cin >> coefficient[i];

	auto result = solve(coefficient, X_COUNT, answer);
	if (result == NULL)
	{
		cout << "-1";
	}
	else
	{
		for (int i = 0; i < X_COUNT; i++)
		{
			cout << result[i];
			if (i != X_COUNT - 1) cout << " ";
		}
	}

	return 0;
}


int* solve(int* coefficient, int numOfCoefficient, int answer)
{
	if (numOfCoefficient < 1)return NULL;
	if (numOfCoefficient == 1)
	{
		if (answer%coefficient[0] != 0) return NULL;
		return new int[1]{ answer / coefficient[0] };
	}

	for (int i = 0; i < (int)(answer / coefficient[numOfCoefficient - 1]) + 1; i++)
	{
		auto minorAnswer = answer - (coefficient[numOfCoefficient - 1] * i);
		auto minorResult = solve(coefficient, numOfCoefficient - 1, minorAnswer);
		if (minorResult == NULL) continue;

		int* result = new int[numOfCoefficient];
		for (int i = 0; i <= numOfCoefficient - 2; i++)
			result[i] = minorResult[i];
		result[numOfCoefficient - 1] = i;
		return result;
	}

	return NULL;
}