from typing import List


def group_items(items: List[object]) -> dict:
    result = {}
    for item in items:
        if result.get(item) is None:
            result[item] = 1
        else:
            result[item] = result[item] + 1
    return result


prices = list(map(int, input().split()))
num_of_persons = 3

times = []
for i in range(num_of_persons):
    begin, end = map(int, input().split())
    times.extend(list(range(begin, end)))

cost = 0
for count in group_items(times).values():
    cost += prices[count - 1] * count

print(cost)
