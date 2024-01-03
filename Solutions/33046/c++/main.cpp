#include <iostream>
#include <vector>
#include <queue>

class MaxConnectedComponentSizeFinder {
private:
    int numberOfNodes;
    std::vector<std::vector<int>> adjacencyList;

public:
    MaxConnectedComponentSizeFinder(int number_of_nodes) : numberOfNodes(number_of_nodes),
                                                           adjacencyList(number_of_nodes + 1) {}

    void addEdge(int nodeA, int nodeB) {
        adjacencyList[nodeA].push_back(nodeB);
        adjacencyList[nodeB].push_back(nodeA);
    }

    int findMaxComponentSize() {
        std::queue<int> queue;
        std::vector<int> visited(numberOfNodes + 1, 0);
        int maxSize = -1;

        queue.push(1);

        while (!queue.empty()) {
            int currentNode = queue.front();
            queue.pop();

            if (visited[currentNode] != 0) {
                continue;
            }

            visited[currentNode] = 1;
            int currentComponentSize = 0;

            for (int neighbor : adjacencyList[currentNode]) {
                currentComponentSize++;
                queue.push(neighbor);
            }

            maxSize = std::max(maxSize, currentComponentSize);
        }

        return maxSize;
    }
};

int main() {
    int numberOfNodes;
    std::cin >> numberOfNodes;

    MaxConnectedComponentSizeFinder maxConnectedComponentFinder(numberOfNodes);

    for (int i = 0; i < numberOfNodes - 1; i++) {
        int nodeA, nodeB;
        std::cin >> nodeA >> nodeB;
        maxConnectedComponentFinder.addEdge(nodeA, nodeB);
    }

    int result = maxConnectedComponentFinder.findMaxComponentSize();
    std::cout << result << std::endl;

    return 0;
}
