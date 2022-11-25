// 2018-07-28

#include <iostream>

using namespace std;

//Prototypes:

int main()
{
	int a, b, c;
	cin >> a >> b >> c;

	if (a*a + b * b == c * c || a * a + c * c == b * b || c * c + b * b == a * a)
		cout << "YES";
	else
		cout << "NO";

	return 0;
}