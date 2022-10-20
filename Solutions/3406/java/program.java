// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner=new Scanner(System.in);
        int a=scanner.nextInt(),b=scanner.nextInt();
        int newA=(a/100)+((a/10)%10)*10+(a%10)*100;
        int newB=(b/100)+((b/10)%10)*10+(b%10)*100;
        if(newA==newB)
        {
            System.out.println(a+" = "+b);
        }
        else if(newA<newB)
        {
            System.out.println(a+" < "+b);
        }
        else
        {
            System.out.println(b+" < "+a);
        }
    }
}
