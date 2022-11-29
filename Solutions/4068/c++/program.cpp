// Copy from Quera

#include <iostream>
using namespace std;

int main ()
{
	long long n, k;
	cin >> n >> k;
	if ( k >n / 2 )
	{
		cout << "Impossible" << endl;
	}
	else
	{
	    if(n % 2 == 0 || n % 2 == 1)
	    {
		    for(int i = (n / 2); i > 0; i --)
		    {
			    cout << i << " " << i + (n / 2) << " ";
		    }
		    cout << n;
	    }
	}
}
