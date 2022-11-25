// 2018-07-28

#include <iostream>
#include <vector>

using namespace std;

//Prototypes:
void drawKhayamTriangle(int);
void firstInitialize(vector<vector<int>>&);
void printKhayamTriangle(vector<vector<int>>const&);

int main()
{
	int inputNum;
	cin >> inputNum;

	drawKhayamTriangle(inputNum);

	return 0;
}


void drawKhayamTriangle(int n)
{
	if (n < 1) return;

	auto myVector = new vector<vector<int>>(n);
	firstInitialize(*myVector);
	for (size_t row = 1; row <= n-1; row++)
	{
		myVector->at(row).push_back(0);
		for (size_t column = 0; column < row+1; column++)
		{
			auto sum = myVector->at(row - 1).at(column) + myVector->at(row - 1).at(column + 1);
			myVector->at(row).push_back(sum);
		}
		myVector->at(row).push_back(0);
	}

	printKhayamTriangle(*myVector);
}

void firstInitialize(vector<vector<int>> &myVector)
{
	myVector.at(0).push_back(0);
	myVector.at(0).push_back(1);
	myVector.at(0).push_back(0);
}

void printKhayamTriangle(vector<vector<int>>const &myVector)
{
	for (size_t row = 0; row < myVector.size(); row++)
	{
		for (size_t column = 0; column < myVector.at(row).size(); column++)
		{
			if (myVector.at(row).at(column) != 0)
				cout << myVector.at(row).at(column) << " ";
		}
		cout << endl;
	}
}