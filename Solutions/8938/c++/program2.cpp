// 2018-09-07

#include <iostream>

using namespace std;

//Define:

//Prototype:

int main()
{
	int sizeOfMatrix;
	cin >> sizeOfMatrix;

	int numOfTravel;
	cin >> numOfTravel;

	int **squareMatrix;
	squareMatrix = new int*[sizeOfMatrix];
	for (size_t i = 0; i < sizeOfMatrix; i++)
		squareMatrix[i] = new int[sizeOfMatrix];

	for (size_t row = 0; row < sizeOfMatrix; row++)
	{
		for (size_t column = 0; column < sizeOfMatrix; column++)
			cin >> squareMatrix[row][column];
	}

	int sum = 0;
	for (size_t i = 0; i < numOfTravel; i++)
	{
		int row, column;
		cin >> row >> column;
		sum += squareMatrix[row - 1][column - 1];
	}

	cout << sum << endl;

	return 0;
}