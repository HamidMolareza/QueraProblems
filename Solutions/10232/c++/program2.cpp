// 2018-09-19

#include <iostream>

using namespace std;

//Define:

//Prototypes:
int howLongTake(int, int, int[], int[], int[]);


int main()
{
	int numOfLights;
	int length;
	cin >> numOfLights >> length;

	auto distant = new int[numOfLights];
	auto redTime = new int[numOfLights];
	auto greenTime = new int[numOfLights];

	for (int i = 0; i < numOfLights; i++)
		cin >> distant[i] >> redTime[i] >> greenTime[i];

	cout << howLongTake(numOfLights, length, distant, redTime, greenTime);
	cout << endl;

	return 0;
}


int howLongTake(int numOfLights, int length, int distant[], int redTime[], int greenTime[])
{
	if (numOfLights < 0 || length < 0 || distant == NULL || redTime == NULL || greenTime == NULL)
		throw invalid_argument("Argument is not valid.");

	if (numOfLights == 0)
		return length;

	int time = 0;
	for (int i = 0; i < numOfLights; i++)
	{
		time += (i == 0 ? distant[i] : distant[i] - distant[i - 1]);

		auto remain = time % (redTime[i] + greenTime[i]);
		if (remain < redTime[i])
			time += redTime[i] - remain;
	}

	time += length - distant[numOfLights - 1];

	return time;
}