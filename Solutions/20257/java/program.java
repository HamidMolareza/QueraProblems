// Copy from Quera

import java.util.Scanner;

public class Main {
    static Scanner s = new Scanner(System.in);
    public static void main(String[] args) {
        long k = s.nextInt(), a = s.nextInt(), b = s.nextInt();
        long a0 = a, b0 = b;
        long t = 0;
        while (a < 0) {
            a += k;
            b += k;
        }
        long mem = a % k;
        if (mem > k/2) {
            a += k - mem;
            t += k - mem;
        } else {
            a -= mem;
            t += mem;
        }
        mem = b % k;
        if (mem > k/2) {
            b += k - mem;
            t += k - mem;
        } else {
            b -= mem;
            t += mem;
        }
        t += (b - a) / k;
        if(t > b0 - a0) {
            System.out.println(b0 - a0);
            return;
        }
        System.out.println(t);
    }
}
