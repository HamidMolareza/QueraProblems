// 2018-08-03

#include <iostream>
#include <string>

using namespace std;

//Defines:

//Prototypes:

int main()
{
	int n;
	cin >> n;

	string str;
	do
	{
		getline(cin, str);
	} while (str == "");

	str = str.substr(1, str.length() - 1);

	for (int i = 0; i < n; i++)
		cout << "copy of ";
	cout << str;

	return 0;
}