// 2018-08-25

#include <iostream>
#include <string>

using namespace std;

//Defines:

//Prototypes:
string add(string, string);
void print(string);
void swap(string&, string&);
string removeFirstZero(string);

int main()
{
	int numOfInputs;
	cin >> numOfInputs;

	string sumStr;
	for (int i = 0; i < numOfInputs; i++)
	{
		string inputStr = "";
		do
		{
			getline(cin, inputStr);

		} while (inputStr == "");

		sumStr = add(sumStr, inputStr);

	}

	print(sumStr);
	cout << endl;

	return 0;
}

string add(string numStr1, string numStr2)
{
	if (numStr1.length() < numStr2.length())
		swap(numStr1, numStr2);

	int diffLength = numStr1.length() - numStr2.length();

	string resultStr;
	resultStr.resize(numStr1.length() + 1);
	int remain = 0;
	for (int i = numStr1.length() - 1; i >= 0; i--)
	{
		int num1 = numStr1.at(i) - '0';
		int num2 = i - diffLength < numStr2.length() ? numStr2.at(i - diffLength) - '0' : 0;
		int sum = num1 + num2 + remain;

		resultStr.at(i + 1) = (sum % 10) + '0';
		remain = sum / 10;
	}

	if (remain == 0)
	{
		return removeFirstZero(resultStr);
	}
	else
	{
		resultStr.at(0) = remain + '0';
		return resultStr;
	}
}

void print(string inputStr)
{
	for (int i = 0; i < inputStr.length(); i++)
		cout << inputStr.at(i);
}

void swap(string &str1, string &str2)
{
	auto temp = str1;
	str1 = str2;
	str2 = temp;
}

string removeFirstZero(string inputStr)
{
	string result;
	result.resize(inputStr.length() - 1);

	for (size_t i = 0; i < inputStr.length() - 1; i++)
		result.at(i) = inputStr.at(i + 1);
	return result;
}
