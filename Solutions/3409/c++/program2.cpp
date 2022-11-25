// 2018-07-03

#include <iostream>

using namespace std;

int main()
{
	int n;
	cin >> n;

	for (size_t i = 1; i <= n; i++)
	{
		for (size_t j = 1; j <= n; j++)
			cout << i * j << " ";
		cout << endl;
	}

	return 0;
}