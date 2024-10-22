# https://quera.org/problemset/158166

import math


def are_same_axis(x1: int, y1: int, x2: int, y2: int):
    return x1 == x2 or y1 == y2


def get_non_zero_points(x1: int, y1: int, x2: int, y2: int):
    return [point for point in list([x1, y1, x2, y2]) if abs(point) > 0]


def calc_direct_distance(point1: int, point2: int) -> int:
    return abs(point1 - point2)


def calc_curve_distance(point1: int, point2: int) -> float:
    r = min(point1, point2)
    return (2 * math.pi * r) / 4


def calc_shortest_distance(x1: int, y1: int, x2: int, y2: int) -> float:
    points = get_non_zero_points(x1, y1, x2, y2)
    if len(points) == 0:
        return 0  # Both are in the center
    elif len(points) == 1:
        return abs(points[0])  # One is in the center

    point1, point2 = points
    if are_same_axis(x1, y1, x2, y2):
        return calc_direct_distance(point1, point2)
    else:
        point1, point2 = map(abs, [point1, point2])  # It does not matter whether the situation is negative or positive.
        result = calc_direct_distance(point1, point2) + calc_curve_distance(point1, point2)
        return result


def main():
    num_of_inputs = int(input())
    for i in range(num_of_inputs):
        x1, y1, x2, y2 = map(int, input().split())
        result = calc_shortest_distance(x1, y1, x2, y2)
        print(result)


if __name__ == '__main__':
    main()
