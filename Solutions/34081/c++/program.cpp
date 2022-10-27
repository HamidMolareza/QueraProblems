// Copy from Quera

#include <iostream>

using namespace std ;

int main()
{
	int n,k;
	cin>>n>>k;
	int i=0 ;
	bool cmd=true ;
	int sum=0 ;
	while(true)
	{
		if(i==0 && cmd==false)
			break;
		cmd = false ;
		i+=k;
		if(i>=n)
			i-=n;
		sum++;
	}
	cout<<sum;
	return 0 ;
}
