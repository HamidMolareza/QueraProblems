// 2018-09-13

#include <iostream>

using namespace std;

//Define:

//Prototypes:
void printSquare(int, int);


int main()
{
	int biggerSquare, smallerSquare;
	cin >> biggerSquare >> smallerSquare;

	printSquare(biggerSquare, smallerSquare);

	return 0;
}


void printSquare(int biggerSquare, int smallerSquare)
{
	if (biggerSquare < 1 || smallerSquare < 1)
		throw invalid_argument("Argument is not valid.");

	if (smallerSquare >= biggerSquare)
	{
		cout << "Wrong order!" << endl;
		return;
	}

	if ((biggerSquare - smallerSquare) % 2 != 0)
	{
		cout << "Wrong difference!" << endl;
		return;
	}

	auto diff = (biggerSquare - smallerSquare) / 2;

	for (int r = 0; r < biggerSquare; r++)
	{
		for (int c = 0; c < biggerSquare; c++)
		{
			if (r >= diff && r < biggerSquare - diff && c >= diff && c < biggerSquare - diff)
			{
				cout << "  ";
			}
			else
			{
				cout << "* ";
			}
		}
		cout << endl;
	}
}