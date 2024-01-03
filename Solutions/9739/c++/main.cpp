#include <iostream>
#include <vector>
#include <stack>

using namespace std;

int calculateLargestRectangleArea(const vector<int>& heights) {
    stack<int> s;
    int maxArea = 0;
    int i = 0;

    while (i < heights.size()) {
        if (s.empty() || heights[s.top()] <= heights[i]) {
            s.push(i++);
        } else {
            int top = s.top();
            s.pop();
            int areaWithTop = heights[top] * (s.empty() ? i : i - s.top() - 1);

            if (maxArea < areaWithTop) {
                maxArea = areaWithTop;
            }
        }
    }

    while (!s.empty()) {
        int top = s.top();
        s.pop();
        int areaWithTop = heights[top] * (s.empty() ? i : i - s.top() - 1);

        if (maxArea < areaWithTop) {
            maxArea = areaWithTop;
        }
    }

    return maxArea;
}

int main() {
    int n;
    cin >> n;

    vector<int> heights(n);

    for (int i = 0; i < n; i++) {
        cin >> heights[i];
    }

    int result = calculateLargestRectangleArea(heights);
    cout << result << endl;

    return 0;
}
