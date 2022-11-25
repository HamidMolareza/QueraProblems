// 2018-08-02

#include <iostream>

using namespace std;

//Defines:

//Prototypes:
long long bmm(long long, long long);
void mySwap(long long&, long long&);

int main()
{
	long long num1, num2;
	cin >> num1 >> num2;

	auto result = bmm(num1, num2);
	cout << result;

	return 0;
}

long long bmm(long long num1, long long num2)
{
	if (num1 < 0) num1 *= -1;
	if (num2 < 0) num2 *= -1;
	if (num1 < num2) mySwap(num1, num2);

	if (num2 == 0)
		return num1;

	return bmm(num2, num1%num2);
}

void mySwap(long long &num1, long long &num2)
{
	auto temp = num1;
	num1 = num2;
	num2 = temp;
}
