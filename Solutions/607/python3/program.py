from typing import List


class Matrix:
    num_of_rows: int
    num_of_columns: int
    matrix: List[List[int]]

    def __init__(self, num_of_rows: int, num_of_columns: int):
        self.num_of_rows = num_of_rows
        self.num_of_columns = num_of_columns

    def get_matrix(self):
        result = []
        for _ in range(self.num_of_rows):
            result.append(list(map(int, input().split())))
        self.matrix = result

    def __mul__(self, other):
        if not type(other) is Matrix:
            raise Exception("Only matrix multiplication is supported.")
        matrix2: Matrix = other

        if self.num_of_columns != matrix2.num_of_rows:
            raise Exception("The number of columns of the first matrix must be equal to the number of rows of the "
                            "second matrix.")

        data = []
        for i in range(self.num_of_rows):
            row = []
            for j in range(matrix2.num_of_columns):
                summation = 0
                for k in range(self.num_of_columns):
                    summation += self.matrix[i][k] * matrix2.matrix[k][j]
                row.append(summation)
            data.append(row)

        result = Matrix(self.num_of_rows, matrix2.num_of_columns)
        result.matrix = data
        return result

    def print_matrix(self):
        for row in range(self.num_of_rows):
            print(*self.matrix[row])


def main():
    a, b, c = map(int, input().split())
    matrix1 = Matrix(a, b)
    matrix1.get_matrix()
    matrix2 = Matrix(b, c)
    matrix2.get_matrix()

    result: Matrix = matrix1 * matrix2
    result.print_matrix()


if __name__ == '__main__':
    main()
