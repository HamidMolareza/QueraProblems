from collections import deque


class MaxConnectedComponentSizeFinder:
    def __init__(self, number_of_nodes):
        self.number_of_nodes = number_of_nodes
        self.adjacency_list = [[] for _ in range(number_of_nodes + 1)]

    def add_edge(self, node_a, node_b):
        self.adjacency_list[node_a].append(node_b)
        self.adjacency_list[node_b].append(node_a)

    def find_max_component_size(self):
        queue = deque([1])
        visited = [0] * (self.number_of_nodes + 1)
        max_size = -1

        while queue:
            current_node = queue.popleft()
            if visited[current_node] != 0:
                continue

            visited[current_node] = 1
            current_component_size = 0

            for neighbor in self.adjacency_list[current_node]:
                current_component_size += 1
                queue.append(neighbor)

            max_size = max(max_size, current_component_size)

        return max_size


if __name__ == "__main__":
    number_of_nodes = int(input())
    max_connected_component_finder = MaxConnectedComponentSizeFinder(number_of_nodes)

    for _ in range(number_of_nodes - 1):
        node_a, node_b = map(int, input().split())
        max_connected_component_finder.add_edge(node_a, node_b)

    result = max_connected_component_finder.find_max_component_size()
    print(result)
