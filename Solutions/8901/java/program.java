// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {

        Scanner sc = new Scanner(System.in);

        int n = sc.nextInt();
        char x = sc.next().charAt(0);

        char ans = x;

        for (int i = 0; i < n; i ++){
            char a = sc.next().charAt(0);
            char b = sc.next().charAt(0);

            if( a == ans )
                ans = b;
            else if ( b == ans )
                ans = a;
        }
        System.out.println(ans);
    }
}