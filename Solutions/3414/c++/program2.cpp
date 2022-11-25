// 2018-07-09

#include <iostream>

using namespace std;

int main()
{
	int inputs[4];
	for (size_t i = 0; i < 4; i++)
		cin >> inputs[i];

	if (inputs[0] == inputs[2]) cout << "Vertical";
	else if (inputs[1] == inputs[3]) cout << "Horizontal";
	else cout << "Try again";

	return 0;
}