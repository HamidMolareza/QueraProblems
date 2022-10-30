// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner=new Scanner(System.in);

        int n = scanner.nextInt();
        long sum = 0;
        int x = n / 2;
        sum = (long)(x - 1) * x / 2;
        sum *= 2;
        sum += x * (n % 2 + 1);
        double ans = (double)sum / (n + 1);
        System.out.printf("%.6f\n", ans);
    }
}