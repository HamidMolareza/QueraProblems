// 2018-08-01

#include <iostream>
#include <string>
#include <exception>

using namespace std;

//Defines:
enum ENumOfNumber
{
	number1, number2, other
};

//Prototypes:
ENumOfNumber numOfMinimumNumber(string, string);
string safeGetLine();
void print(string left, string right, char c);

int main()
{
	string numStr1, numStr2;
	numStr1 = safeGetLine();
	numStr2 = safeGetLine();

	auto result = numOfMinimumNumber(numStr1, numStr2);
	switch (result)
	{
	case ENumOfNumber::number1:
		print(numStr1, numStr2, '<');
		break;
	case ENumOfNumber::number2:
		print(numStr2, numStr1, '<');
		break;
	case ENumOfNumber::other:
		print(numStr1, numStr2, '=');
		break;
	default:
		throw exception();
		break;
	}

	return 0;
}

ENumOfNumber numOfMinimumNumber(string str1, string str2)
{
	if (str1.length() != str2.length()) throw new invalid_argument("length is not equal.");

	for (int i = str1.length() - 1; i >= 0; i--)
	{
		if (str1.at(i) < str2.at(i))
			return ENumOfNumber::number1;
		if (str1.at(i) > str2.at(i))
			return ENumOfNumber::number2;
	}
	return ENumOfNumber::other;
}

string safeGetLine()
{
	string str;
	do
	{
		getline(cin, str);
	} while (str == "");
	return str;
}

void print(string left, string right, char c)
{
	cout << left << " " << c << " " << right;
}
