#include <iostream>
#include <vector>
#include <algorithm>

int main() {
    // Input the size of the arrays
    int n;
    std::cin >> n;

    // Input the first array of integers
    std::vector<int> n1(n);
    for (int i = 0; i < n; ++i) {
        std::cin >> n1[i];
    }

    // Input the second array of integers
    std::vector<int> n2(n);
    for (int i = 0; i < n; ++i) {
        std::cin >> n2[i];
    }

    // Use a lambda function to filter elements based on the condition
    std::vector<int> m;
    for (int i = 0; i < n; ++i) {
        if (n2[i] == 1) {
            m.push_back(n1[i]);
        }
    }

    // Sort the filtered list
    std::sort(m.begin(), m.end());

    // Print the elements separated by a space
    for (int t : m) {
        std::cout << t << " ";
    }

    return 0;
}
