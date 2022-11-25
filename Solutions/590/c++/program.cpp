// 2018-07-28

#include <iostream>

using namespace std;

//Prototypes:
long long bmm(long long, long long);
long long kmm(long long, long long);

int main()
{
	long long m, n;
	cin >> m >> n;

	cout << bmm(m, n) << " " << kmm(m, n) << endl;

	return 0;
}

long long bmm(long long m, long long n)
{
	auto biggest = m >= n ? m : n;
	auto smallest = m >= n ? n : m;

	long long remain = biggest % smallest;

	while (remain != 0)
	{
		biggest = smallest;
		smallest = remain;
		remain = biggest % smallest;
	}
	return smallest;
}

long long kmm(long long m, long long n)
{
	return (m*n) / bmm(m, n);
}