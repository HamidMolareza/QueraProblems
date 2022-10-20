// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int n = scanner.nextInt();
        long fact = 1;
        for (int i = 0; i< n; i++)
            fact *= i+1;
        System.out.println(fact);
    }
}
