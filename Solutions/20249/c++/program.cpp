// Copy from Quera

#include <iostream>
using namespace std;

int main() 
{
	int n, m, a, sum=0, zarfiat;
	
	cin >> n >> m ;
	
	zarfiat = n*m;
	
	for ( int i = 0 ; i < n ; ++i )
	{
	    cin >> a;
	    
	    sum += a;
	}
	
	cout << ( zarfiat - sum )/m;
	return 0;
}
