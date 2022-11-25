// 2018-08-28

#include <iostream>
#include <string>
#include <iomanip>
#include <math.h>

using namespace std;

//Defines:
#define NUM_OF_INPUTS 4
#define NUM_OF_FLOAT 6

//Prototypes:
double min(double[], int);
double max(double[], int);
double average(double[], int);
double sum(double[], int);
double product(double[], int);
void printLine(string, double);

int main()
{
	double inputArray[NUM_OF_INPUTS];
	for (int i = 0; i < NUM_OF_INPUTS; i++)
		cin >> inputArray[i];

	printLine("Sum", sum(inputArray, NUM_OF_INPUTS));
	printLine("Average", average(inputArray, NUM_OF_INPUTS));
	printLine("Product", product(inputArray, NUM_OF_INPUTS));
	printLine("MAX", max(inputArray, NUM_OF_INPUTS));
	printLine("MIN", min(inputArray, NUM_OF_INPUTS));

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

double product(double inputArray[], int inputSize)
{
	if (inputArray == NULL)
		throw new invalid_argument("Argument is null.");
	if (inputSize < 1)
		throw new invalid_argument("The size is less than 1");

	double prd = 1;
	for (size_t i = 0; i < inputSize; i++)
		prd *= inputArray[i];
	return prd;
}

void printLine(string keyName, double value)
{
	cout << keyName << " : ";
	value = floor(1000000 * value) / 1000000;
	cout << fixed << setprecision(NUM_OF_FLOAT) << value << endl;
}