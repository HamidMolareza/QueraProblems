// 2018-08-01

#include <iostream>

using namespace std;

//Defines:
void ShowFibNth(long, long);

//Prototypes:

int main()
{
	long n1, n2;
	cin >> n1 >> n2;
	ShowFibNth(n1, n2);

	return 0;
}

void ShowFibNth(long n1, long n2)
{
	if (n1 == 1 && n2 == 1)
	{
		cout << "1" << endl;
		return;
	}

	cout << n1 << endl;
	ShowFibNth(n2 - n1, n1);
}
