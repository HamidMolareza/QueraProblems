// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int n = scanner.nextInt(), x = scanner.nextInt(), y = scanner.nextInt();
        for(int i = 0; i <= n / x; i++)
            if((n - i * x) % y == 0)
            {
                System.out.println(i + " " + (n - i * x) / y);
                return ;
            }
        System.out.println(-1);
    }

}