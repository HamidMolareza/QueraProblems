// 2018-08-11

#include <iostream>
#include <string>

using namespace std;

//Defines:
string* getStandard(string[], int);
string getStandard(string);
void printStr(string[], int);

//Prototypes:

int main()
{
	int n;
	cin >> n;

	auto inputsStr = new string[n];
	for (int i = 0; i < n; i++)
	{
		do
		{
			getline(cin, inputsStr[i]);
		} while (inputsStr[i] == "");
	}

	inputsStr = getStandard(inputsStr, n);

	printStr(inputsStr, n);

	delete[] inputsStr;
	return 0;
}

string* getStandard(string inputs[], int size)
{
	if (size < 1) return inputs;
	if (inputs == NULL) throw new invalid_argument("Argument is null.");

	for (int i = 0; i < size; i++)
		inputs[i] = getStandard(inputs[i]);
	return inputs;
}

string getStandard(string inputStr)
{
	if (inputStr.length() < 1) return inputStr;

	inputStr[0] = toupper(inputStr[0]);
	for (int i = 1; i < inputStr.length(); i++)
	{
		inputStr[i] = inputStr[i - 1] == ' ' ?
			toupper(inputStr[i]) : tolower(inputStr[i]);
	}
	return inputStr;
}

void printStr(string inputs[], int size)
{
	if (size < 1 || inputs == NULL) return;

	for (int i = 0; i < size; i++)
		cout << inputs[i] << endl;
}
