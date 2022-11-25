// 2018-10-13

#include <iostream>
#include <string>

using namespace std;

//Define:
#define MAXIMUM_LENGTH 7

//Prototypes:
string solve(int);
bool solve(string, string&, int);
bool isMoreThan(string, string);
int findMinimumBiggerDigitIndex(string, int);
string num2Str(int);
string reverse(string);
void swap(string&, int, int);
void sort(string&, int);

//30999992
int main()
{
	int input;
	cin >> input;

	cout << solve(input) << endl;

	return 0;
}


string solve(int inputNum)
{
	if (inputNum < 1)
		throw invalid_argument("Argument is not valid.");

	auto inputStr = num2Str(inputNum);
	auto result = inputStr;

	auto isSolved = solve(inputStr, result, 0);
	if (isSolved)
		return result;
	return "0";
}

bool solve(string mainNum, string &resultNum, int index)
{
	if (index == mainNum.length() - 1)
		return isMoreThan(resultNum, mainNum);

	if (solve(mainNum, resultNum, index + 1))
		return true;

	auto biggerDigitIndex = findMinimumBiggerDigitIndex(resultNum, index);
	if (biggerDigitIndex > index)
	{
		swap(resultNum, index, biggerDigitIndex);
		sort(resultNum, index + 1);
		return true;
	}

	return false;
}

bool isMoreThan(string num1, string num2)
{
	if (num1.length() > num2.length())
		return true;
	if (num1.length() < num2.length())
		return false;

	for (size_t i = 0; i < num1.length(); i++)
	{
		if (num1[i] > num2[i])
			return true;
		if (num1[i] < num2[i])
			return false;
	}
	return false;
}

int findMinimumBiggerDigitIndex(string input, int targetIndex)
{
	int minimumBigger = '9' + 1;
	int index = -1;
	for (size_t i = targetIndex + 1; i < input.length(); i++)
	{
		if (input[i] > input[targetIndex] && input[i] < minimumBigger)
		{
			minimumBigger = input[i];
			index = i;
		}
	}
	return index;
}

string num2Str(int inputNum)
{
	string temp;
	temp.reserve(MAXIMUM_LENGTH);

	while (inputNum > 0)
	{
		temp.push_back(inputNum % 10 + '0');
		inputNum /= 10;
	}

	return reverse(temp);
}

string reverse(string inputStr)
{
	string result;
	result.reserve(inputStr.length());
	for (int i = inputStr.length() - 1; i >= 0; i--)
		result.push_back(inputStr.at(i));
	return result;
}

void swap(string &input, int index1, int index2)
{
	auto temp = input[index1];
	input[index1] = input[index2];
	input[index2] = temp;
}

void sort(string &input, int startIndex)
{
	for (size_t i = startIndex; i < input.length() - 1; i++)
	{
		for (size_t j = i + 1; j < input.length(); j++)
		{
			if (input[i] > input[j])
				swap(input, i, j);
		}
	}
}