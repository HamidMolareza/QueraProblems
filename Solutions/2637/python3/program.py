import math


def is_odd(number: int) -> int:
    return number % 2 != 0


def get_sequence_value(index: int) -> int:
    if index < 1:
        return 0
    if not is_odd(index):
        index -= 1
    return int(math.floor(index / 2.0) + 2)


def sum_of_sequence(index: int) -> int:
    if index < 1:
        return 0
    sequence_value = get_sequence_value(index)
    result = 2 * (sequence_value * (sequence_value + 1) / 2 - 1)
    if is_odd(index):
        result -= sequence_value
    return int(result)


def get_maximum_squares(nom_of_lines: int) -> int:
    return 2 + sum_of_sequence(nom_of_lines - 1)


n = int(input())
maximum_squares = get_maximum_squares(n)
print(maximum_squares)
