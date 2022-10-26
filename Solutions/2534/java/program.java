// Copy from Quera

import java.util.Scanner;

public class Main
{
    static Scanner scanner=new Scanner(System.in);
    static int n,sum=0,ans=0;
    public static void main(String[] args)
    {
        n=scanner.nextInt();
        function(n);
        System.out.println(ans);
    }

    static void function(int id)
    {
        if(id==0)return ;
        int x=scanner.nextInt();
        sum+=x;
        function(id-1);
        if(x>sum/n)
        {
            ans+=x-sum/n;// onaii ke bishtr az miangin hstn byd enghd be baghie bdn ke be miangin brsn hme
        }
    }


}