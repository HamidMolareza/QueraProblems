// 2018-09-01

#include <iostream>
#include <vector>

using namespace std;

//Define:
#define FIRST_FIBONACH 0
#define SECOND_FIBONACH 1

//Prototype:
class Pibonach
{
public:
	Pibonach();
	static vector<long long> getPibonach(long long);

private:
	static void process(long long, long long, long long, vector<long long>&);

};
void print(int, vector<long long>);

int main()
{
	int input;
	cin >> input;

	Pibonach p;
	auto pib = p.getPibonach(input);
	print(input, pib);

	return 0;
}


Pibonach::Pibonach()
{
}

vector<long long> Pibonach::getPibonach(long long maximumNum)
{
	if (maximumNum < SECOND_FIBONACH)
		throw new invalid_argument("Argument is not valid.");

	vector<long long> result;
	process(FIRST_FIBONACH, SECOND_FIBONACH, maximumNum, result);
	return result;
}

void Pibonach::process(long long num1, long long num2,
	long long maximumNum, vector<long long> &result)
{
	if (num1 < 0 || num2 < 0)
		throw new overflow_error("Overflow");

	if (num2 > maximumNum)
		return;

	result.push_back(num2);
	process(num2, num1 + num2, maximumNum, result);
}

void print(int maximumNum, vector<long long> pib)
{
	if (maximumNum < SECOND_FIBONACH)
		return;

	int index = 1;
	for (int i = 1; i <= maximumNum; i++)
	{
		if (index >= pib.size() || i < pib.at(index))
		{
			cout << "-";
		}
		else
		{
			cout << "+";
			index++;
		}
	}
}