from typing import List


def can_valid(number: int, divisors: List[int]) -> bool:
    for divisor in divisors:
        if number % divisor != 0:
            return False
    return True


def main():
    _ = input()
    divisors = list(map(int, input().split()))

    count = 0
    for i in range(1, 1001):
        if can_valid(i, divisors):
            count += 1

    print(count)


if __name__ == '__main__':
    main()
