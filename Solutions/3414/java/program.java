// Copy from Quera

import java.util.Scanner;

public class p2q2 {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int x0 = scanner.nextInt(), y0 = scanner.nextInt(), x1 = scanner.nextInt(), y1 = scanner.nextInt();
        if(x0 == x1)
            System.out.println("Vertical");
        else if(y0 == y1)
            System.out.println("Horizontal");
        else
            System.out.println("Try again");
    }
}
