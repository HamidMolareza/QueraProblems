// 2017-01-01

#include <iostream>

using namespace std;

int main()
{
	int userNum;
	cin >> userNum;

	int numOfStars = 1; //For print.

	//Print first star:
	for (int i = 0; i < userNum; i++)
	{
		//Print space:
		for (int space = 0; space < userNum - i; space++)
			cout << " ";

		//Print star:
		for (size_t star = 0; star < numOfStars; star++)
			cout << "*";

		//Go to next line:
		cout << endl;
		numOfStars += 2;
	}

	//Print second star:
	for (int i = 0; i <= userNum; i++)
	{


		//Print space:
		for (int space = 0; space < i; space++)
			cout << " ";

		//Print star:
		for (size_t star = 0; star < numOfStars; star++)
			cout << "*";

		//Go to next line:
		cout << endl;
		numOfStars -= 2;
	}

	return 0;
}