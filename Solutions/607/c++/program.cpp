// 2018-08-31

#include <iostream>

using namespace std;

//Define:
struct SMatrix
{
	SMatrix() {}
	SMatrix(int r, int c)
	{
		row = r;
		column = c;
	}

	int row;
	int column;
	double **matrixArray;
};

//Prototype:
double ** generateNewArray(int, int);
double ** generateNewArray(SMatrix);
void init0(double**, int, int);
bool isMatrixSizeValid(int, int);
bool isMatrixSizeValid(SMatrix);
void cinMatrix(SMatrix);
SMatrix mutliply(SMatrix, SMatrix);
void print(SMatrix);

int main()
{
	SMatrix matrix1;
	cin >> matrix1.row >> matrix1.column;

	SMatrix matrix2;
	matrix2.row = matrix1.column;
	cin >> matrix2.column;

	matrix1.matrixArray = generateNewArray(matrix1);
	cinMatrix(matrix1);

	matrix2.matrixArray = generateNewArray(matrix2);
	cinMatrix(matrix2);

	auto resultMatrix = mutliply(matrix1, matrix2);
	print(resultMatrix);

	return 0;
}


double ** generateNewArray(int row, int column)
{
	if (!isMatrixSizeValid(row, column))
		throw new invalid_argument("Argument is not valid.");

	auto result = new double*[row];
	for (size_t i = 0; i < row; i++)
		result[i] = new double[column];

	init0(result, row, column);

	return result;
}

double ** generateNewArray(SMatrix matrix)
{
	return generateNewArray(matrix.row, matrix.column);
}

void init0(double ** inputArray, int row, int column)
{
	if (!isMatrixSizeValid(row, column))
		throw new invalid_argument("Argument is not valid.");

	for (size_t r = 0; r < row; r++)
	{
		for (size_t c = 0; c < column; c++)
			inputArray[r][c] = 0;
	}
}

bool isMatrixSizeValid(int row, int column)
{
	return (row >= 1 && column >= 1);
}

bool isMatrixSizeValid(SMatrix matrix)
{
	return isMatrixSizeValid(matrix.row, matrix.column);
}

void cinMatrix(SMatrix inputMatrix)
{
	if (!isMatrixSizeValid(inputMatrix))
		throw new invalid_argument("Argument is not valid.");

	for (size_t r = 0; r < inputMatrix.row; r++)
	{
		for (size_t c = 0; c < inputMatrix.column; c++)
			cin >> inputMatrix.matrixArray[r][c];
	}
}

SMatrix mutliply(SMatrix m1, SMatrix m2)
{
	if (!isMatrixSizeValid(m1) || !isMatrixSizeValid(m2) || m1.column != m2.row)
		throw new invalid_argument("Argument size is not valid.");

	SMatrix resultMatrix(m1.row, m2.column);
	resultMatrix.matrixArray = generateNewArray(resultMatrix);

	for (size_t r = 0; r < m1.row; r++)
	{
		for (size_t c = 0; c < m2.column; c++)
		{
			for (size_t i = 0; i < m1.column; i++)
				resultMatrix.matrixArray[r][c] += m1.matrixArray[r][i] * m2.matrixArray[i][c];
		}
	}

	return resultMatrix;
}

void print(SMatrix matrix)
{
	if (!isMatrixSizeValid(matrix))
		throw new invalid_argument("Argument is not valid.");

	for (size_t r = 0; r < matrix.row; r++)
	{
		for (size_t c = 0; c < matrix.column; c++)
			cout << matrix.matrixArray[r][c] << " ";
		cout << endl;
	}
}