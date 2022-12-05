// Copy from Quera

import java.util.Scanner;
public class Main {
    static Scanner s = new Scanner(System.in);
    public static void main(String[] args) {
        int n = s.nextInt();
        long F = 1, S = 2;
        for (int i = 0; i < n - 1; i++) {
            long mem = S + F;
            F = S % (long)(Math.pow(10, 9) + 7);
            S = mem;
        }
        System.out.println(F);
    }
}