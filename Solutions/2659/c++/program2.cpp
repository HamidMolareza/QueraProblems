// 2018-08-26

#include <iostream>
#include <string>

using namespace std;

//Defines:

//Prototypes:
string getStr();
int getNumOfWrong(string, string);

int main()
{
	int inputLength;
	cin >> inputLength;

	auto baseStr = getStr();
	auto userStr = getStr();

	cout << getNumOfWrong(baseStr, userStr) << endl;

	return 0;
}

string getStr()
{
	string inputStr;
	do
	{
		getline(cin, inputStr);
	} while (inputStr == "");

	return inputStr;
}

int getNumOfWrong(string baseStr, string userStr)
{
	if (baseStr.length() != userStr.length())
		throw new invalid_argument("Length error!");

	int counter = 0;
	for (size_t i = 0; i < baseStr.length(); i++)
	{
		if (baseStr.at(i) != userStr.at(i))
			counter++;
	}
	return counter;
}
