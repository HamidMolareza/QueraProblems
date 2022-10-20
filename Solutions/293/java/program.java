// Copy from Quera

import java.util.Scanner;

public class Main
{

    public static void main(String[] args)
    {
        Scanner scanner=new Scanner(System.in);
        int a = scanner.nextInt();
        int b = scanner.nextInt();
        for(int i = a; i <= b ;i ++)
        {
            if(is_prime(i) == 1)
                System.out.println(i);
        }
    }

    private static int is_prime(int num)
    {
        if(num == 1)
            return 0;
        for(int i = 2; i < num; i ++)
        {
            if(num % i == 0)
                return 0;
        }
        return 1;
    }
}