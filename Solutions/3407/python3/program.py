from typing import List

ADJACENT_CELLS = [[0, -1], [-1, -1], [-1, 0], [-1, 1], [0, 1], [1, 1], [1, 0], [1, -1]]  # [[Row, Column], ...]
BOMB_NUMBER = -1


def is_cell_exist(table: List[List[int]], row: int, column: int) -> bool:
    if row < 0 or row >= len(table):
        return False
    if column < 0 or column >= len(table[0]):
        return False
    return True


def count_bombs(table: List[List[int]], row: int, column: int) -> int:
    count = 0
    for adjacent_cell in ADJACENT_CELLS:
        adjacent_row, adjacent_column = row + adjacent_cell[0], column + adjacent_cell[1]
        if is_cell_exist(table, adjacent_row, adjacent_column) and table[adjacent_row][adjacent_column] == BOMB_NUMBER:
            count += 1
    return count


def main():
    num_of_rows, num_of_columns = map(int, input().split())
    num_of_bombs = int(input())

    # Init table
    table = [[0] * num_of_columns for _ in range(num_of_rows)]

    # Set Bombs in table
    for i in range(num_of_bombs):
        x, y = map(int, input().split())
        table[x - 1][y - 1] = BOMB_NUMBER

    # Calculate Bombs and Print
    for row in range(num_of_rows):
        for column in range(num_of_columns):
            if table[row][column] == BOMB_NUMBER:
                print("*", end=" ")
            else:
                table[row][column] = count_bombs(table, row, column)
                print(table[row][column], end=" ")
        print("")


if __name__ == '__main__':
    main()
