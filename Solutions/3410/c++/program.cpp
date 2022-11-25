// 2018-08-11

#include <iostream>
#include <vector>

using namespace std;

//Prototypes:
void drawKhayamTriangle(int);
void firstInitialize(vector<vector<long long>>&);
void printKhayamTriangle(vector<vector<long long>>const&);

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

	auto myVector = new vector<vector<long long>>(n);
	firstInitialize(*myVector);
	for (int row = 1; row <= n - 1; row++)
	{
		myVector->at(row).push_back(0);
		for (int column = 0; column < row + 1; column++)
		{
			auto sum = myVector->at(row - 1).at(column) + myVector->at(row - 1).at(column + 1);
			myVector->at(row).push_back(sum);
		}
		myVector->at(row).push_back(0);
	}

	printKhayamTriangle(*myVector);
}

void firstInitialize(vector<vector<long long>> &myVector)
{
	myVector.at(0).push_back(0);
	myVector.at(0).push_back(1);
	myVector.at(0).push_back(0);
}

void printKhayamTriangle(vector<vector<long long>>const &myVector)
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