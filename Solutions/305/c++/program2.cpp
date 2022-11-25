// 2018-07-29

#include <iostream>

using namespace std;

//Prototypes:
long long gcd(long long, long long);
void mySwab(long long&, long long&);

int main()
{
	long long a, b;
	cin >> a >> b;

	cout << gcd(a, b);

	return 0;
}

long long gcd(long long a, long long b)
{
	if (a < b) mySwab(a, b);

	if (a%b == 0)
		return b;

	return gcd(b, a%b);
}

void mySwab(long long &a, long long &b)
{
	auto temp = a;
	a = b;
	b = temp;
}