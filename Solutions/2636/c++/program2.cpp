// 2018-08-21

#include <iostream>

using namespace std;

//Defines:
#define NUM_OF_INPUTS 6
const int CHESS_PIECES[NUM_OF_INPUTS] = { 1,1,2,2,2,8 };

//Prototypes:


int main()
{
	int inputs[NUM_OF_INPUTS];
	for (int i = 0; i < NUM_OF_INPUTS; i++)
		cin >> inputs[i];

	for (int i = 0; i < NUM_OF_INPUTS; i++)
		cout << CHESS_PIECES[i] - inputs[i] << " ";

	return 0;
}