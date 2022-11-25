// 2019-02-11

#include <iostream>
#include <iomanip>
#include <math.h>

using namespace std;

//Define:

//Prototypes:
double exponential(double, int);
long long factorial(int n);

int main()
{
	int x, n;
	cin >> x >> n;

	auto result = exponential(x, n);
	cout << fixed << std::setprecision(3) << result;
	return 0;
}


double exponential(double x, int n)
{
	if (x == 0)
		return 1;

	double sum = 1;
	for (int i = 0; i < n - 1; i++)
		sum += pow(x, i + 1) / factorial(i + 1);
	return sum;
}

long long factorial(int n)
{
	long long result = 1;
	while (n > 1)
	{
		result *= n;
		n--;
	}
	return result;
}
