// 2018-06-14

#include <iostream>
#include <string>

using namespace std;

string* split(string, char, int);
string findAnswer(string**, int, string);

int main()
{
	int m, n;
	cin >> m >> n;

	string **keyValue;
	keyValue = new string *[m];
	for (int i = 0; i < m; i++)
		keyValue[i] = new string[2];

	//m line
	string temp;
	for (int i = 0; i < m; i++)
	{
		do
		{
			getline(cin, temp);
		} while (temp == "");
		auto spaceIndex = temp.find_first_of(' ');;
		keyValue[i][0] = temp.substr(0, spaceIndex);
		keyValue[i][1] = temp.substr(spaceIndex + 1, temp.length() - spaceIndex - 1);
	}

	//Last line:
	getline(cin, temp);
	auto goatSpeech = split(temp, ' ', n);
	string result;
	for (int i = 0; i < n; i++)
	{
		auto anser = findAnswer(keyValue, m, goatSpeech[i]);
		if (anser != "")
			result += anser + " kachal!";
		else
			result += "kachal!";

		if (i != n - 1)
			result += " ";
	}

	cout << result;

	return 0;
}

string* split(string refStr, char character, int outputSize)
{
	string* result = new string[outputSize];
	int index = 0;
	int counter = 0;

	for (int i = 0; i < refStr.length(); i++)
	{
		if (refStr[i] == ' ')
		{
			result[counter] = refStr.substr(index, i - index);
			index = i + 1;
			counter++;
		}
	}

	result[counter] = refStr.substr(index, refStr.length() - index);
	return result;
}

string findAnswer(string** keyValue, int sizeKey, string goatSpeech)
{
	for (int i = 0; i < sizeKey; i++)
	{
		if (keyValue[i][0] == goatSpeech)
			return keyValue[i][1];
	}
	return "";
}

/*
4 4
kachal kachal
kalache roghane
kalle pache
salam kachal
salam kalache kalle kachal
*/