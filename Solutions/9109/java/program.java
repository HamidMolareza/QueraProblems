// Copy from Quera

import java.util.Scanner;

public class Main {
    final static int maxn = 100;
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int n = scanner.nextInt();
        int[] counter = new int[maxn];
        for (int i = 0; i < n; i++) {
            int x = scanner.nextInt();
            counter[x - 1]++;
        }

        int mn = -1;
        for (int i = 0; i < maxn; i++)
            if (counter[i] > 0 && (mn == -1 || counter[i] < counter[mn]))
                mn = i;

        System.out.println(mn + 1);
    }
}