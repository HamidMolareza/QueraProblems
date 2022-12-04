// Copy from Quera

#include <iostream>
using namespace std;


void printVerbsInSequence(int numOfVerbs)
{
	if(numOfVerbs == 0)
	{
		return;
	}
	char str[100];
	cin >> str;
	printVerbsInSequence(numOfVerbs - 1);
	cout << str << " ";
}

int main()
{
	int n;
	cin >> n;
	printVerbsInSequence(n);
}
