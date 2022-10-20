// Copy from Quera

#include <iostream>

using namespace std;

int main()
{
    int n, answer = 0;
    string s;

    cin >> n;

    for (int i = 0; i < n; i++)
    {

        cin >> s;

        int distinct_characters = s.length();
        for (int j = 0; s[j] != 0; j++)
        {
            int duplicate = 0;
            for (int k = 0; k < j; k++)
                if (s[k] == s[j])
                    duplicate = 1;

            distinct_characters -= duplicate;
        }

        if (answer < distinct_characters)
            answer = distinct_characters;
    }

    cout << answer << '\n';
    return 0;
}
