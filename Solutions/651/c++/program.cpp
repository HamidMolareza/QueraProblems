// 2018-08-03

#include <iostream>
#include <string>

using namespace std;

//Defines:

//Prototypes:
long long convert(long long, int, int);
long long increaseTo10(long long, int);
long long decreaseFrom10(long long, int);
bool isMirror(long long);
string toString(long long);
int numOfFigure(long long);
long long reverse(long long);

int main()
{
	long long number;
	int base, goal;
	cin >> number >> base >> goal;

	auto resultConvert = convert(number, base, goal);
	auto resultMirror = isMirror(resultConvert);
	cout << (resultMirror ? "YES" : "NO");

	return 0;
}

long long convert(long long number, int base, int goal)
{
	if (number < 0 || base < 2 || base > 10 || goal < 2 || goal > 10)
		throw new invalid_argument("argument is not valid.");
	if (base == goal) return number;

	if (base != 10) number = increaseTo10(number, base);
	return decreaseFrom10(number, goal);
}

long long increaseTo10(long long number, int base)
{
	if (number < 0 || base < 2 || base > 10)
		throw new invalid_argument("argument is not valid.");
	if (base == 10) return number;

	long long value = 1;
	long long result = 0;
	while (number > 0)
	{
		result += (number % 10)*value;
		value *= base;
		number /= 10;
	}

	return result;
}

long long decreaseFrom10(long long number, int goal)
{
	if (number < 0 || goal < 2 || goal > 10)
		throw new invalid_argument("argument is not valid.");
	if (goal == 10) return number;

	bool firstNonZero = false;
	int numOfFirstZero = 0;
	long long result = 0;
	while (number > 0)
	{
		auto remain = number % goal;

		if (!firstNonZero)
		{
			if(remain == 0)
				numOfFirstZero++;
			else
				firstNonZero = true;
		}
		result = result * 10 + remain;
		number /= goal;
	}

	result = reverse(result);

	for (int i = 0; i < numOfFirstZero; i++)
		result *= 10;

	return result;
}

bool isMirror(long long number)
{
	if (number < 0) number *= -1;
	auto numberToStr = toString(number);

	for (int i = 0; i < (int)(numberToStr.length()/2); i++)
	{
		if (numberToStr.at(i) != numberToStr.at(numberToStr.length() - i - 1))
			return false;
	}
	return true;
}

string toString(long long number)
{
	bool isNegative = number < 0;
	auto size = numOfFigure(number);

	string str;
	int endIndex;
	if (isNegative)
	{
		str.resize(size + 1);
		str.at(0) = '-';
		endIndex = size;
		number *= -1;
	}
	else
	{
		str.resize(size);
		endIndex = size - 1;
	}

	for (int i = 0; i < size; i++)
	{
		str.at(endIndex - i) = (number % 10) + '0';
		number /= 10;
	}

	return str;
}

int numOfFigure(long long number)
{
	if (number < 0) number *= -1;
	if (number == 0) return 1;

	int counter = 0;
	while (number > 0)
	{
		number /= 10;
		counter++;
	}
	return counter;
}

long long reverse(long long number)
{
	bool isNegative = false;
	if (number < 0)
	{
		isNegative = true;
		number *= -1;
	}

	long long result = 0;
	while (number > 0)
	{
		auto remain = number % 10;
		result = result * 10 + remain;
		number /= 10;
	}

	if (isNegative) result *= -1;

	return result;
}