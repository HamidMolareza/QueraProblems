def solve(n: int, bigger_number: int, smaller_number: int) -> (int, int):
    if n % smaller_number == 0:
        return 0, n // smaller_number

    max_try = n // bigger_number
    for i in range(1, max_try + 1):
        remain = n - (i * bigger_number)
        if remain % smaller_number == 0:
            return i, remain // smaller_number
    return -1, -1


def main():
    n, x, y = map(int, input().split())

    if x < y:
        num_of_y, num_of_x = solve(n, y, x)
    else:
        num_of_x, num_of_y = solve(n, x, y)

    if num_of_x == -1:
        print(-1)
    else:
        print(num_of_x, num_of_y)


main()
