// Copy from Quera

#include <iostream>
#include <iomanip>

using namespace std;

int main()
{
    double weight, height, bmi;
    cin >> weight >> height;
    bmi = weight / (height * height);
    cout << setprecision(2) << fixed << bmi << "\n";

    if (bmi < 18.5)
    {
        cout << "Underweight" << endl;
    }
    else if (bmi > 18.5 && bmi < 25)
    {
        cout << "Normal" << endl;
    }
    else if (bmi > 25 && bmi < 30)
    {
        cout << "Overweight" << endl;
    }
    else
    {
        cout << "Obese" << endl;
    }
    return 0;
}