// 2018-09-26

#include <iostream>

using namespace std;

//Define:
#define BASE_HOUR 6
#define BASE_MINUTE 30

struct STime
{
	int hour;
	int minute;

	STime()
	{
		hour = minute = 0;
	}
	void print();
};

//Prototypes:
STime mirrorTime2MainTime(STime);


int main()
{
	STime mirrorTime;
	cin >> mirrorTime.hour;
	cin >> mirrorTime.minute;

	auto mainTime = mirrorTime2MainTime(mirrorTime);
	mainTime.print();
	cout << endl;

	return 0;
}


STime mirrorTime2MainTime(STime mirrorTime)
{
	if (mirrorTime.hour < 0 || mirrorTime.hour > 11 ||
		mirrorTime.minute < 0 || mirrorTime.minute > 59)
	{
		throw invalid_argument("Argument is not valid.");
	}

	STime mainTime;

	//Hour:
	mainTime.hour = (BASE_HOUR - mirrorTime.hour) + BASE_HOUR;
	if (mainTime.hour == 12)
		mainTime.hour = 0;

	//Minute:
	mainTime.minute = (BASE_MINUTE - mirrorTime.minute) + BASE_MINUTE;
	if (mainTime.minute == 60)
		mainTime.minute = 0;

	return mainTime;
}

void STime::print()
{
	if (hour < 0 || hour > 11 || minute < 0 || minute > 59)
		throw invalid_argument("Argument is not valid.");

	//Hour:
	if (hour < 10)
		cout << "0";
	cout << hour << ":";

	//Minute:
	if (minute < 10)
		cout << "0";
	cout << minute;
}
