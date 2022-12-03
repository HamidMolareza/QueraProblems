// Copy from Quera

#include <iostream>
using namespace std;

const int maxn = 103;
string m, k[maxn], n[maxn], s[maxn];
long long n1, count[maxn][3], sum[3];
string a;


int main()
{
	int max = 0;
	cin >> n1 >> m;

	for(int i = 1; i <= n1; i++)
	{
		if(i % 6 == 1 or i % 6 == 2) k[i] = '3';
		else if(i % 6 == 3 or i % 6 == 4) k[i] = '1';
		else k[i] = '2';
	}

	for(int i = 1; i <= n1; i++)
	{
		if(i % 3 == 1) n[i] = '1';
		else if(i % 3 == 2) n[i] = '2';
		else n[i] = '3';
	}

	for(int i = 1; i <= n1; i++)
	{
		if(i % 4 == 1 or i % 4 == 3) s[i] = '2';
		else if(i % 4 == 2) s[i] = '1';
		else s[i] = '3';
	}

	for(int i = 1; i <= n1; i++)
	{
		a = m[i-1];
		if(a == k[i]) count[i][0] = 1;
		if(a == n[i]) count[i][1] = 1;
		if(a == s[i]) count[i][2] = 1;
		sum[0] += count[i][0];
		if(max < sum[0]) max = sum[0];
		sum[1] += count[i][1];
		if(max < sum[1]) max = sum[1];
		sum[2] += count[i][2];
		if(max < sum[2]) max = sum[2];
	}

	cout << max << "\n";
	if(max == sum[0] && max == sum[1] && max == sum[2]) cout << "keyvoon" << "\n" << "nezam" << "\n" << "shir farhad";
	else if(max == sum[0] && max == sum[1] && max != sum[2]) cout << "keyvoon" << "\n" << "nezam";
	else if(max == sum[0] && max != sum[1] && max == sum[2]) cout << "keyvoon" << "\n" << "shir farhad";
	else if(max != sum[0] && max == sum[1] && max == sum[2]) cout << "nezam" << "\n" << "shir farhad";
	else if (max == sum[0]) cout << "keyvoon";
	else if(max == sum[1]) cout << "nezam";
	else cout << "shir farhad";
	return 0;
}
