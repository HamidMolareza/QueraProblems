from typing import List


def is_prime(number: int) -> bool:
    if number < 2:
        return False  # 1 is not prime

    for i in range(2, number // 2 + 1):
        if number % i == 0:
            return False
    return True


def get_primes(begin: int, end: int) -> List[int]:
    result = []
    for i in range(begin, end + 1):
        if is_prime(i):
            result.append(i)
    return result


a = int(input())
b = int(input())

for prime_number in get_primes(a, b):
    print(prime_number)
