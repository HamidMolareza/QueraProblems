// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int a = scanner.nextInt() , b = scanner.nextInt() , c = scanner.nextInt();
        if ((a%2 == 0) || ((b % 2 == 0) && (c % 2 == 0))){
            System.out.print("good");}
        else {
            System.out.print("bad");
        }
        

    }
}