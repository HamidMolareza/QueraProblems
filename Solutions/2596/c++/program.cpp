// Copy from Quera

#include <iostream>
using namespace std;

const int maxn = 101;
int n;
int tm[maxn];
int main()
{
	cin >> n;
	for(int i = 0; i < n; i++)
		cin >> tm[i];

	int count = 0;
	int tmp = 0;
	for(int i = 1; i <= 1000; i++)
	{
		for(int j = 0; j < n; j++)
		{
			if(i%tm[j] == 0)
			{
				count ++;
			}
		}
		if(count == n) tmp++;
		count = 0;
	}
	cout << tmp;
	return 0;
}