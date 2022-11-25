// 2018-09-05

#include <iostream>
#include <string>

using namespace std;

//Define:
#define MAXIMUM_LETTER 20

//Prototype:
int maximimLetter(string[], int);
int maximimLetter(string);

int main()
{
	int numOfInputs;
	cin >> numOfInputs;

	auto inputArray = new string[numOfInputs];
	for (size_t i = 0; i < numOfInputs; i++)
	{
		do
		{
			getline(cin, inputArray[i]);

		} while (inputArray[i] == "");
	}

	cout << maximimLetter(inputArray, numOfInputs) << endl;

	delete[] inputArray;
	return 0;
}

int maximimLetter(string inputArray[], int size)
{
	if (size < 1 || inputArray == nullptr)
		throw new invalid_argument("Argument is not valid.");

	auto maximum = 0;
	for (size_t i = 0; i < size; i++)
	{
		auto result = maximimLetter(inputArray[i]);
		if (maximum < result)
			maximum = result;
	}

	return maximum;
}

int maximimLetter(string inputStr)
{
	string oldChars;
	oldChars.reserve(MAXIMUM_LETTER);

	auto counter = 0;
	for (size_t i = 0; i < inputStr.length(); i++)
	{
		if (oldChars.find(inputStr[i]) == string::npos)
		{
			oldChars.push_back(inputStr[i]);
			counter++;
		}
	}

	return counter;
}