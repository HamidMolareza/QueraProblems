from typing import List


def get_matrix(num_of_rows: int) -> List[List[int]]:
    matrix = []
    for i in range(num_of_rows):
        row = [int(x) for x in input().split()]
        matrix.append(row)
    return matrix


def calculate_cost(begin: int, end: int, cost_table: List[List[int]]) -> int:
    return cost_table[begin - 1][end - 1]


num_of_areas, num_of_trips = map(int, input().split())

costs = get_matrix(num_of_areas)
trips = get_matrix(num_of_trips)

summation = 0
for trip in trips:
    summation += calculate_cost(trip[0], trip[1], costs)

print(summation)
