// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner=new Scanner(System.in);
        int n=scanner.nextInt();
        int ans=1;
        for(int i=1;i<=n;i++)
            if(gcd(i,n)==1)
                ans=lcm(ans,i);
        System.out.println(ans);
    }
    static int gcd(int x, int y)
    {
        for(int i=x+y;i>=1;i--)
            if(x%i==0 && y%i==0)
                return i;
        return 1;// pish nmiad hichvght
    }
    static int lcm(int x, int y)
    {
        return x*y/gcd(x,y);
    }
}