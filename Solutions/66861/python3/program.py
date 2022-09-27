def num_of_divisors(number: int) -> int:
    count = 0
    for i in range(1, number + 1):
        if number % i == 0:
            count += 1
    return count


k = int(input())

n = 1
while True:
    summation = n * (n + 1) // 2
    if num_of_divisors(summation) >= k:
        print(summation)
        break
    n += 1
