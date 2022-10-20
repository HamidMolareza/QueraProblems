// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner=new Scanner(System.in);
        int a = scanner.nextInt(), b = scanner.nextInt(), x = scanner.nextInt();
        int ans = 0;
        for (int i = 1; i <= a; i++)
            for (int j = 1; j <= b; j++)
                if (a % i == 0 && b % j == 0 && i + j <= x)
                    ans++;
        System.out.println(ans);
    }
}