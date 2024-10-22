import re
from typing import List


def find_number_with_hash(numbers: List[str]) -> int:
    for index, number in enumerate(numbers):
        if '#' in number:
            return index
    return -1


def is_format_match(number, format_str: str) -> bool:
    format_number = format_str.replace('#', '')
    match_length = len(str(number)) - len(format_number)
    if match_length < 1:
        return False
    pattern = format_str.replace('#', "\\d{%d}" % match_length)
    return bool(re.match(pattern, str(number)))


def main():
    parts = input().split()
    hash_index = find_number_with_hash(parts)

    a, _, b, _, c = parts
    if hash_index == 0:
        result_number = int(c) - int(b)
    elif hash_index == 2:
        result_number = int(c) - int(a)
    else:
        result_number = int(a) + int(b)

    if is_format_match(result_number, parts[hash_index]):
        parts[hash_index] = result_number
        print(*parts)
    else:
        print(-1)


if __name__ == '__main__':
    main()
