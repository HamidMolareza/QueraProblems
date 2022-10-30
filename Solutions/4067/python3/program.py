def get_base_index(x: int, y: int) -> int:
    if x == 0 and y == 0:
        return 0
    if x == 1 and y == 1:
        return 1
    if x == 2 and y == 0:
        return 2
    if x == 3 and y == 1:
        return 3
    return -1


def get_time(x: int, y: int) -> int:
    cycle_number = int(min(x / 2, y / 2))
    x -= cycle_number * 2
    y -= cycle_number * 2
    base_index = get_base_index(x, y)
    if base_index == -1:
        return -1
    return base_index + (4 * cycle_number)


def main():
    num_of_lines = int(input())
    for i in range(num_of_lines):
        x, y = map(int, input().split())
        time = get_time(x, y)
        print(time)


if __name__ == '__main__':
    main()
