// 2018-08-24

#include <iostream>

using namespace std;

//Defines:
struct Point
{
	int x;
	int y;
};

//Prototypes:
Point GetCoordinateSnail(int);
Point recursiveCoordinateSnail(Point, int);
int increaseNumber(int);

void print(Point);

int main()
{
	int n;
	cin >> n;

	auto result = GetCoordinateSnail(n);
	print(result);
	cout << endl;

	return 0;
}

Point GetCoordinateSnail(int n)
{
	Point beginPoint{ 0,0 };
	return recursiveCoordinateSnail(beginPoint, n);
}

Point recursiveCoordinateSnail(Point basePoint, int counter)
{
	if (counter <= 1) return basePoint;

	if (basePoint.x == basePoint.y)
		basePoint.x = increaseNumber(basePoint.x);
	else
		basePoint.y = increaseNumber(basePoint.y);

	return recursiveCoordinateSnail(basePoint, counter - 1);

	return Point();
}

int increaseNumber(int number)
{
	return number <= 0 ? (number*-1) + 1 : number * -1;
}

void print(Point p)
{
	cout << p.x << " " << p.y;
}
