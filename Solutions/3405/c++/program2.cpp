// 2018-07-09

#include <iostream>

using namespace std;

int main()
{
	int inputs[1001];
	int index = -1;
	do
	{
		index++;
		cin >> inputs[index];
	} while (inputs[index] != 0);

	for (int i = index - 1; i >= 0; i--)
		cout << inputs[i] << endl;

	return 0;
}