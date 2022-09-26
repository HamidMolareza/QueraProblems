from typing import List


def divisors(number: int) -> List[int]:
    result = []
    for i in range(1, number + 1):
        if number % i == 0:
            result.append(i)
    return result


a, b, x = map(int, input().split())

count = 0
for bag1 in divisors(a):
    for bag2 in divisors(b):
        if bag1 + bag2 <= x:
            count += 1

print(count)
