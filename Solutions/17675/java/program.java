//Copy from Quera

import java.util.Scanner;

public class Main
{

    public static void main(String[] args)
    {
        Scanner scanner=new Scanner(System.in);
        int n=scanner.nextInt();
        boolean []isFib=new boolean[101];
        for(int i=1;;i++)
        {
            int x=fib(i);
            if(x>n)break;
            isFib[x]=true;
        }
        for(int i=1;i<=n;i++)
        {
            if(isFib[i]) System.out.print("+");
            else System.out.print("-");
        }

    }

    private static int fib(int x)
    {
        if(x==1)return 1;
        if(x==2)return 2;
        return fib(x-1)+fib(x-2);
    }


}