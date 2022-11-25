// 2018-08-04

#include <iostream>

using namespace std;

//Defines:
#define NUM_OF_MEMBER 3

struct SPerson
{
	int start;
	int stop;
	bool isInside(int minute);
};

//Prototypes:
int getMinimumPeriod(SPerson[], int);
int getMaximumPeriod(SPerson[], int);
double calculateExpense(SPerson[], double[], int);
int numOfPresent(SPerson[], int, int);

int main()
{
	double prices[NUM_OF_MEMBER];
	for (int i = 0; i < NUM_OF_MEMBER; i++)
		cin >> prices[i];

	SPerson persons[NUM_OF_MEMBER];
	for (int i = 0; i < NUM_OF_MEMBER; i++)
	{
		cin >> persons[i].start;
		cin >> persons[i].stop;
	}

	auto expense = calculateExpense(persons, prices, NUM_OF_MEMBER);
	cout << expense << endl;

	return 0;
}

bool SPerson::isInside(int minute)
{
	return minute >= start && minute < stop;
}

int getMinimumPeriod(SPerson persons[], int size)
{
	if (size < 0) throw new invalid_argument("argument is not valid.");
	if (persons == NULL) throw new invalid_argument("argument is null.");

	int min = persons[0].start;
	for (int i = 1; i < size; i++)
	{
		if (min > persons[i].start)
			min = persons[i].start;
	}
	return min;
}

int getMaximumPeriod(SPerson persons[], int size)
{
	if (size < 0) throw new invalid_argument("argument is not valid.");
	if (persons == NULL) throw new invalid_argument("argument is null.");

	int max = persons[0].stop;
	for (int i = 1; i < size; i++)
	{
		if (max < persons[i].stop)
			max = persons[i].stop;
	}
	return max;
}

double calculateExpense(SPerson persons[], double prices[], int size)
{
	if(persons == NULL || prices == NULL) throw new invalid_argument("argument is null.");
	if(size < 0) throw new invalid_argument("argument is not valid.");

	auto minimumPeriod = getMinimumPeriod(persons, NUM_OF_MEMBER);
	auto maximumPeriod = getMaximumPeriod(persons, NUM_OF_MEMBER);

	double expense = 0;
	for (int i = minimumPeriod; i < maximumPeriod; i++)
	{
		auto numOfPresentPeople = numOfPresent(persons, size, i);
		expense += numOfPresentPeople * prices[numOfPresentPeople - 1];
	}

	return expense;
}

int numOfPresent(SPerson persons[], int numOfPersons, int minute)
{
	if (persons == NULL) throw new invalid_argument("argument is null.");
	if (numOfPersons < 0) throw new invalid_argument("argument is not valid.");

	size_t counter = 0;
	for (int i = 0; i < numOfPersons; i++)
	{
		if (persons[i].isInside(minute))
			counter++;
	}
	return counter;
}
