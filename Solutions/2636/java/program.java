// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        int kingRequired   = 1;
        int queenRequired  = 1;
        int rookRequired   = 2;
        int bishopRequired = 2;
        int knightRequired = 2;
        int pawnRequired   = 8;
        
        Scanner scanner = new Scanner(System.in);
        
        int king   = scanner.nextInt();
        int queen  = scanner.nextInt();
        int rook   = scanner.nextInt();
        int bishop = scanner.nextInt();
        int knight = scanner.nextInt();
        int pawn   = scanner.nextInt();
        
        System.out.println((kingRequired - king)      + " " + 
                            (queenRequired - queen)   + " " + 
                            (rookRequired - rook)     + " " +
                            (bishopRequired - bishop) + " " + 
                            (knightRequired - knight) + " " + 
                            (pawnRequired - pawn));
    }
}
