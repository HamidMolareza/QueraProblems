import math


def is_odd(number: int) -> bool:
    return number % 2 != 0


def is_prime(number: int) -> bool:
    for i in range(2, int(math.sqrt(number)) + 1):
        if (n % i) == 0:
            return False
    return True


n = int(input())

if is_odd(n) and is_prime(n):
    print("zoj")
else:
    print("fard")
