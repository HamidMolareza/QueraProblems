// 2018-07-31

#include <iostream>
#include <string>

using namespace std;

//Defines:

//Prototypes:
void printChar(char, int);
void printStr(string, int);

int main()
{
	string inputStr;
	do
	{
		getline(cin, inputStr);
	} while (inputStr == "");

	cout << inputStr << endl;
	for (size_t i = 0; i < inputStr.length() - 1; i++)
	{
		printChar(inputStr.at(i + 1), i + 1);
		printStr(inputStr, i + 1);
		cout << endl;
	}

	return 0;
}

void printChar(char c, int count)
{
	if (count < 1) return;

	for (size_t i = 0; i < count; i++)
		cout << c;
}

void printStr(string str, int startIndex)
{
	if (startIndex < 0) return;

	for (size_t i = startIndex; i < str.length(); i++)
		cout << str.at(i);
}
