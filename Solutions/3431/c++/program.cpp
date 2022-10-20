// Copy from Quera

#include <iostream>
#include <string>
using namespace std;

string arr[55], s;

int main()
{
    int n, m;
    cin >> n >> m;
    for (int i = 0; i < n; i++)
        cin >> arr[i];
    cin >> s;
    int len = s.size();
    int answer = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
        {
            bool flag = false;
            for (int k = 0; s[k] && arr[i][j + k]; k++)
                if (s[k] != arr[i][j + k])
                    flag = true;
            if (j + len > m)
                flag = true;
            answer = answer + !flag;

            flag = false;
            for (int k = 0; s[k] && arr[i + k][j]; k++)
                if (s[k] != arr[i + k][j])
                    flag = true;
            if (i + len > n)
                flag = true;
            answer = answer + !flag;
        }
    cout << answer << '\n';
    return 0;
}
