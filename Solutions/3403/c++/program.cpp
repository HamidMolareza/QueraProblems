// Copy from Quera

#include <iostream>
#include <iomanip>
using namespace std;

int main()
{
    double a, b, c, d;
    cin >> a >> b >> c >> d;
    cout << setprecision(6) << fixed << "Sum : " << (a + b + c + d) << endl;
    double ave = a + b + c + d;
    cout << setprecision(6) << fixed << "Average : " << ave / 4 << endl;
    cout << setprecision(6) << fixed << "Product : " << 1ll * a * b * c * d << endl;
    cout << setprecision(6) << fixed << "Max : " << max(a, max(b, max(c, d))) << endl;
    cout << setprecision(6) << fixed << "Min : " << min(a, min(b, min(c, d))) << endl;
    return 0;
}