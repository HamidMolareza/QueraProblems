// 2018-07-28

#include <iostream>
#include <vector>

using namespace std;

//Prototypes:
bool isPrime(int);
void printVector(vector<int> inputVec);

int main()
{
	int beginNum, endNum;
	cin >> beginNum >> endNum;

	vector<int> result;
	for (int i = beginNum + 1; i < endNum; i++)
		if (isPrime(i))
			result.push_back(i);

	printVector(result);

	return 0;
}

bool isPrime(int number)
{
	if (number < 1) throw new invalid_argument("Input is not valid.");
	if (number == 1) return false;
	if (number < 3) return true; //2 3 is prime.

	for (int i = 2; i < number / 2 + 1; i++)
		if (number%i == 0) return false;
	return true;
}

void printVector(vector<int> inputVec)
{
	for (size_t i = 0; i < inputVec.size(); i++)
	{
		cout << inputVec.at(i);
		if (i != inputVec.size() - 1)
			cout << ",";
	}
}