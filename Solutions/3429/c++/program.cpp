// 2018-07-03

#include <iostream>

using namespace std;

int main()
{
	int temperature;
	cin >> temperature;

	if (temperature > 100)
		cout << "Steam";
	else if (temperature < 0)
		cout << "Ice";
	else
		cout << "Water";

	return 0;
}