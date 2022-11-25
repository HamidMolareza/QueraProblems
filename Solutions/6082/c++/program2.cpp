// 2018-08-23

#include <iostream>

using namespace std;

int main()
{
	int row, column;
	int counter = 0;

	cin >> row >> column;

	for (int r = 0; r < row; r++)
	{
		for (int c = 0; c < column; c++)
		{
			char tempChar;
			cin >> tempChar;
			if (tempChar == '*')
				counter++;
		}
	}

	cout << counter << endl;
	return 0;
}