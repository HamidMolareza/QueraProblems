// 2018-08-22

#include <iostream>
#include <vector>

using namespace std;

//Defines:
vector<int> convertFrom10(int, int);
vector<int> reverse(vector<int>);
bool isSumOk(vector<int>);

//Prototypes:


int main()
{
	int number, goal;
	cin >> number >> goal;

	auto converted = convertFrom10(number, goal);
	cout << (isSumOk(converted) ? "Yes" : "No") << endl;

	return 0;
}

vector<int> convertFrom10(int inputNum, int goal)
{
	//if (inputNum < 0) throw new invalid_argument("Negative is not implement.");
	//if (goal < 1 || goal > 10) throw new invalid_argument("Input is not valid.");

	vector<int> temp;
	if (goal == 1)
	{
		temp.push_back(0);
		return temp;
	}

	temp.reserve(sizeof(inputNum) * 8);

	while (inputNum > 0)
	{
		temp.push_back(inputNum%goal);
		inputNum /= goal;
	}

	return reverse(temp);
}

vector<int> reverse(vector<int> inputVec)
{
	vector<int> result;
	result.reserve(inputVec.size());
	for (int i = inputVec.size() - 1; i >= 0; i--)
		result.push_back(inputVec.at(i));
	return result;
}

bool isSumOk(vector<int> inputVec)
{
	if (inputVec.size() < 2) return true;

	long sum1 = 0;
	long sum2 = 0;
	for (int i = 0; i < inputVec.size(); i++)
	{
		if (i % 2 == 0)
			sum1 += inputVec.at(i);
		else
			sum2 += inputVec.at(i);
	}

	return sum1 == sum2;
}
