from typing import List


def generate_khayyam_triangle(n: int) -> List[List[int]]:
    triangle = [[1], [1, 1], [1, 2, 1]]
    for i in range(n - 3):
        row = [1]
        prev_state = triangle[-1]
        for j in range(len(prev_state) - 1):
            row.append(prev_state[j] + prev_state[j + 1])
        row.append(1)
        triangle.append(row)
    return triangle[0:n]


def main():
    n = int(input())
    triangle = generate_khayyam_triangle(n)
    for i in range(len(triangle)):
        print(*triangle[i])


if __name__ == '__main__':
    main()
