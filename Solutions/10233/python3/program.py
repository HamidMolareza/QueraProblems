from typing import Optional, List


def get_smaller_digit(sorted_digits: List[str], bigger_than: str) -> Optional[str]:
    for i in range(len(sorted_digits)):
        if sorted_digits[i] > bigger_than:
            return sorted_digits[i]
    return None


def solve(number: str) -> str:
    for i in range(len(number) - 2, -1, -1):
        remain_digits = sorted(number[i:])
        smaller_digit = get_smaller_digit(remain_digits, number[i])
        if smaller_digit is None:
            continue
        remain_digits.remove(smaller_digit)
        return number[0:i] + smaller_digit + ''.join(remain_digits)
    return '0'


def main():
    number = input()
    result = solve(number)
    print(result)


main()
