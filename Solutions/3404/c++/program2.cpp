// 2018-08-08

#include <iostream>
#include <string>

using namespace std;

//Defines:

//Prototypes:
double getBmi(int, double);
string bmiStatus(double);

int main()
{
	int weight;
	cin >> weight;

	double height;
	cin >> height;

	auto bmi = getBmi(weight, height);
	cout.precision(2);
	cout << fixed << bmi << endl;

	cout << bmiStatus(bmi) << endl;

	return 0;
}

double getBmi(int weight, double height)
{
	return weight / (height*height);
}

string bmiStatus(double bmi)
{
	if (bmi < 18.5) return "Underweight";
	if (bmi < 25) return "Normal";
	if (bmi < 30) return "Overweight";
	return "Obese";
}
