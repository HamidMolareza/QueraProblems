// 2019-01-09

#include <iostream>
#include <string>

using namespace std;

//Define:
struct Colors
{
	int numOfRed = 0;
	int numOfGreen = 0;
	int numOfYellow = 0;
};

//Prototypes:
string cinString();
Colors countColors(string);
bool isDangerous(Colors);


int main()
{
	auto inputStr = cinString();
	auto count = countColors(inputStr);
	auto dangerous = isDangerous(count);
	if (dangerous)
		cout << "nakhor lite" << endl;
	else
		cout << "rahat baash" << endl;

	return 0;
}


string cinString()
{
	string input;
	do
	{
		getline(cin, input);
	} while (input == "");
	return input;
}

Colors countColors(string inputStr)
{
	Colors result;

	for (size_t i = 0; i < inputStr.length(); i++)
	{
		switch (inputStr[i])
		{
		case 'G':
			result.numOfGreen++;
			break;
		case 'Y':
			result.numOfYellow++;
			break;
		case 'R':
			result.numOfRed++;
			break;
		}
	}

	return result;
}

bool isDangerous(Colors colors)
{
	return colors.numOfRed >= 3 ||
		colors.numOfGreen < 1 ||
		(colors.numOfRed >= 2 && colors.numOfYellow >= 2);
}
