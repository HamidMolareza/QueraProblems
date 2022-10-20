// Copy from Quera

import java.util.Scanner;

public class Main {
    final static int maxn = 100000;
    public static void main(String[] args) {
        int[] output = new int[maxn];
        int n = 1;
        output[0] = 1;

        while (n < maxn) {
            for (int i = 0; i < n; i++)
                if (i + n < maxn)
                    output[i + n] = 1 - output[i];
        
            n *= 2;
        }

        Scanner scanner = new Scanner(System.in);
        int l = scanner.nextInt(), r = scanner.nextInt();
        
        for (int i = l - 1; i < r; i++)
            System.out.print(output[i]);
        System.out.println();
    }
}