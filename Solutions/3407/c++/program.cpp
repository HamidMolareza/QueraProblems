// Copy from Quera

#include <iostream>
using namespace std;

int a[100][100];

int main()
{
    int n,m,k,x,y;
    cin>>n>>m>>k;
    for(int i=0;i<k;i++)
    {
            cin>>x>>y;
            a[x-1][y-1]=-100;
            if(y-2<m && y-2>-1 && x-1<n && x-1>-1)a[x-1][y-2]++;
            if(y<m && y>-1 && x-1<n && x-1>-1)a[x-1][y]++;
            if(y-1<m && y-1>-1 && x-2<n && x-2>-1)a[x-2][y-1]++;
            if(y-1<m && y-1>-1 && x<n && x>-1)a[x][y-1]++;
            if(y<m && y>-1 && x<n && x>-1)a[x][y]++;
            if(y-2<m && y-2>-1 && x<n && x>-1)a[x][y-2]++;
            if(y<m && y>-1 && x-2<n && x-2>-1)a[x-2][y]++;
            if(y-2<m && y-2>-1 && x-2<n && x-2>-1)a[x-2][y-2]++;
    }
    for(int i=0;i<n;i++)
    {
        for(int j=0;j<m;j++)
        {
            if(a[i][j]<0)cout<<"* ";
            else cout<<a[i][j]<<" ";
        }
        cout<<endl;
    }
    return 0;
}
