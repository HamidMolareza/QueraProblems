// Copy from Quera

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scan = new Scanner (System.in);
        int n = scan.nextInt() , k = scan.nextInt();
        int i , capacity = 0,tmp;
        for(i = 0 ; i < n ; i++) {
            tmp = scan.nextInt();
            capacity+= tmp;
        }
        int res = capacity/k;
        if(capacity%k != 0)
            res++;
        System.out.print(n - res);
    }
}