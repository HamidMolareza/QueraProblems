// Copy from Quera

#include <iostream>
using namespace std;

long long a[10 * 1000 + 10];
bool seen[10 * 1000 + 10];
long long f(int n)
{
	if (seen[n] == 1)
	{
		return a[n];
	}
	if (n == 1)
	{
		a[n] = 1;
		seen[n] = 1;
		return 1;
	}
	if (n == 2)
	{
		a[n] = 2;
		seen[n] = 1;
		return 2;
	}
	a[n] = (f(n - 1) % 1000000007 + f(n - 2) % 1000000007) % 1000000007;
	seen[n] = 1;
	return a[n];
}

int main()
{
	int n;
	cin >> n;
	long long answer = f(n);
	if (answer == 0)answer = 1000000007;
	cout << answer;
	return 0;
}