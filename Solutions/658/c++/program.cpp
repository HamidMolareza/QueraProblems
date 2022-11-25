// 2018-08-07

#include <iostream>

using namespace std;

//Defines:

//Prototypes:
int maximumProfit(int*, int);
int maximumSubsequence(int*, int, int);

int main()
{
	int n;
	cin >> n;

	int *profits = new int[n];
	for (int i = 0; i < n; i++)
		cin >> profits[i];

	cout << maximumProfit(profits, n) << endl;

	return 0;
}

int maximumProfit(int *profits, int size)
{
	if (profits == NULL) throw new invalid_argument("argument is null.");
	if (size < 1) return 0;
	if (size == 1) return profits[0] >= 0 ? profits[0] : 0;

	int max = -1;
	for (int i = 1; i <= size; i++)
	{
		auto subsequence = maximumSubsequence(profits, size, i);
		if (subsequence > max)
			max = subsequence;
	}

	return max >= 0 ? max : 0;
}

int maximumSubsequence(int *profits, int size, int subSize)
{
	if (profits == NULL) throw new invalid_argument("argument is null.");
	if (subSize > size) throw new invalid_argument("subSize > size");
	if (size < 1 || subSize < 1) return 0;
	if (size == 1) return profits[0] >= 0 ? profits[0] : 0;

	int max = -1;
	for (int i = 0; i <= size - subSize; i++)
	{
		int sum = 0;
		for (int j = i; j < subSize + i; j++)
			sum += profits[j];
		if (sum > max)
			max = sum;
	}

	return max >= 0 ? max : 0;
}
