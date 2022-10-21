from typing import List


def fibonacci(limit: int) -> List[int]:
    if limit <= 0:
        return []
    if limit <= 1:
        return [1]
    result = [1, 2]
    if limit <= 2:
        return result

    while True:
        new_item = result[-1] + result[-2]
        if new_item > limit:
            return result
        result.append(new_item)


def main():
    n = int(input())
    result = fibonacci(n)
    index = 0
    for i in range(1, n + 1):
        if index >= len(result) or i < result[index]:
            print("-", end="")
        else:
            print("+", end="")
            index += 1


if __name__ == '__main__':
    main()
