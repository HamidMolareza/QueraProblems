// 2018-08-26

#include <iostream>
#include <string>
#include <iomanip>
#include <math.h>

using namespace std;

//Defines:
#define NUM_OF_FLOAT 3

//Prototypes:
double min(double[], int);
double max(double[], int);
double average(double[], int);
double sum(double[], int);
void printLine(string, double);

int main()
{


	int numOfInputs;
	cin >> numOfInputs;

	auto inputArray = new double[numOfInputs];
	for (int i = 0; i < numOfInputs; i++)
		cin >> inputArray[i];

	printLine("Max", max(inputArray, numOfInputs));
	printLine("Min", min(inputArray, numOfInputs));
	printLine("Avg", average(inputArray, numOfInputs));

	return 0;
}

double min(double inputArray[], int inputSize)
{
	if (inputArray == NULL)
		throw new invalid_argument("Argument is null.");
	if (inputSize < 1)
		throw new invalid_argument("The size is less than 1");

	auto minimum = inputArray[0];
	for (size_t i = 1; i < inputSize; i++)
	{
		if (minimum > inputArray[i])
			minimum = inputArray[i];
	}
	return minimum;
}

double max(double inputArray[], int inputSize)
{
	if (inputArray == NULL)
		throw new invalid_argument("Argument is null.");
	if (inputSize < 1)
		throw new invalid_argument("The size is less than 1");

	auto maximum = inputArray[0];
	for (size_t i = 1; i < inputSize; i++)
	{
		if (maximum < inputArray[i])
			maximum = inputArray[i];
	}
	return maximum;
}

double average(double inputArray[], int inputSize)
{
	if (inputArray == NULL)
		throw new invalid_argument("Argument is null.");
	if (inputSize < 1)
		throw new invalid_argument("The size is less than 1");

	return sum(inputArray, inputSize) / inputSize;
}

double sum(double inputArray[], int inputSize)
{
	if (inputArray == NULL)
		throw new invalid_argument("Argument is null.");
	if (inputSize < 1)
		throw new invalid_argument("The size is less than 1");

	double sum = 0;
	for (size_t i = 0; i < inputSize; i++)
		sum += inputArray[i];
	return sum;
}

void printLine(string keyName, double value)
{
	cout << keyName << ": ";
	value = floor(1000 * value) / 1000;
	cout<< fixed << setprecision(NUM_OF_FLOAT) << value << endl;
}
