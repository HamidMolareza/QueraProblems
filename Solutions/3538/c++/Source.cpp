// 2018-06-24

#include <iostream>
#include <string>

using namespace std;

#define NUMBER_OF_PERSONS 3
#define NUMBER_OF_WEEKS 7
#define GOOD_DAY 1
#define BAD_DAY 0

//Prototypes:
string* splits(string, char);
size_t countChar(string, char);
int numOfDay(string);

int main()
{
	int daysValue[NUMBER_OF_WEEKS] = { GOOD_DAY,GOOD_DAY,GOOD_DAY,GOOD_DAY,GOOD_DAY,GOOD_DAY,GOOD_DAY };
	for (size_t i = 0; i < NUMBER_OF_PERSONS; i++)
	{
		int numOfInputs;
		cin >> numOfInputs;

		string inputDays;
		do
		{
			getline(cin, inputDays);
		} while (inputDays == "");

		auto splitDays = splits(inputDays, ' ');

		for (size_t j = 0; j < numOfInputs; j++)
		{
			auto numerOfDay = numOfDay(splitDays[j]);
			daysValue[numerOfDay] = BAD_DAY;
		}
		delete[]splitDays;
	}

	int sum = 0;
	for (size_t i = 0; i < NUMBER_OF_WEEKS; i++)
		sum += daysValue[i];
	cout << sum;

	return 0;
}

string* splits(string inputStr, char baseChar)
{
	auto numOfChars = countChar(inputStr, baseChar);
	string* result = new string[numOfChars + 1];

	size_t counter = 0;
	size_t index = 0;
	for (size_t i = 0; i < inputStr.length(); i++)
	{
		if (inputStr.at(i) == baseChar)
		{
			result[counter] = inputStr.substr(index, i - index);
			index = i + 1;
			counter++;
		}
	}

	result[counter] = inputStr.substr(index, inputStr.length() - index);

	return result;
}

size_t countChar(string inputStr, char baseChar)
{
	size_t counter = 0;
	for (size_t i = 0; i < inputStr.length(); i++)
		if (inputStr.at(i) == baseChar)
			counter++;
	return counter;
}

int numOfDay(string dayStr)
{
	if (dayStr == "shanbe")
		return 0;
	else if (dayStr == "1shanbe")
		return 1;
	else if (dayStr == "2shanbe")
		return 2;
	else if (dayStr == "3shanbe")
		return 3;
	else if (dayStr == "4shanbe")
		return 4;
	else if (dayStr == "5shanbe")
		return 5;
	else if (dayStr == "jome")
		return 6;
	else
		return -1;
}