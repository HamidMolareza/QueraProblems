// Copy from Quera

// In the name of God

#include <iostream>
using namespace std;

int main()
{
    // Set up standard number of chess pieces
    int standard_king = 1, standard_queen = 1, standard_rook = 2;
    int standard_bishop = 2, standard_knight = 2, standard_soldier = 8;

    // Get the number of chess pieces from input
    int my_king, my_queen, my_rook, my_bishop, my_knight, my_soldier;
    cin >> my_king >> my_queen >> my_rook >> my_bishop >> my_knight >> my_soldier;

    // Output the differences
    cout << standard_king - my_king << " " << standard_queen - my_queen << " ";
    cout << standard_rook - my_rook << " " << standard_bishop - my_bishop << " ";
    cout << standard_knight - my_knight << " " << standard_soldier - my_soldier << "\n";

    return 0;
}
