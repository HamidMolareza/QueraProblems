// Copy from Quera

#include <iostream>

using namespace std ;

int main()
{
	long long a , b , c ;
	cin >> a >> b >> c ;
	if (a == b && b == c)
	{
		cout << 0 << endl ;
	}
	else if (abs(a - b) == abs(b - c) || abs(a - c) == abs(b - c) || abs(a - b) == abs(a - c) || (a == b && a != c && b != c) || (a == c && a != b && b != c) || (b == c && a != b && a != c))
	{
		cout << 1 << endl ;
	}
	else
	{
		cout << 2 << endl ;
	}
	return 0 ;
}