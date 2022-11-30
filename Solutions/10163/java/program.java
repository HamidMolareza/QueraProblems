// Copy from Quera

import java.util.*;

public class Main {

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        int a = in.nextInt(), b = in.nextInt(), c = in.nextInt();
        int a1 = in.nextInt(), a2 = in.nextInt(), b1 = in.nextInt(), b2 = in.nextInt(), c1 = in.nextInt(), c2 = in.nextInt();
        int min = Math.min(Math.min(a1, b1), c1), max = Math.max(Math.max(a2, b2), c2);
        long res = 0;
        for (int i = min; i <= max; i++) {
            int count = 0;
            if (i >= a1 && i < a2) {
                count++;
            }
            if (i >= b1 && i < b2) {
                count++;
            }
            if (i >= c1 && i < c2) {
                count++;
            }
            switch (count) {
                case 1:
                    res += a;
                    break;
                case 2:
                    res += (2 * b);
                    break;
                case 3:
                    res += (3 * c);
                    break;
            }
        }
        System.out.println(res);
    }
}