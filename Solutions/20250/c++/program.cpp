#include <iostream>
#include <vector>
#include <algorithm>
#include <map>

using namespace std;

bool frequent (vector <int> v)
{
    for (int i : v)
    {
        int freq = count(v.begin(), v.end(), i);
        
        if (freq == 1)
        {
            return true;
        }
        
        else
        {
            return false;
        }
        
    }
}

int solve (int n)
{
    if (n == 1)
    {
        return 2;
    }

    return solve (n - 1) + n;
}

int main()
{
    int n;
    cin >> n;

    map <int, int> lines;
    vector <int> gradients;
    vector <int> y_incs;

    for (int i = 0; i < n; i++)
    {
        int gradient, y_inc;
        cin >> gradient >> y_inc;

        gradients.push_back(gradient);
        y_incs.push_back(y_inc);
        lines[gradient] = y_inc; 
    }

    if (frequent(gradients))
    {
        cout << solve(n) << endl;
    }


    return 0;
}