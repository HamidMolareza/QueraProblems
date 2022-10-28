//Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner=new Scanner(System.in);
        int n=scanner.nextInt(),l=scanner.nextInt();
        int time=0;
        int lastPlace=0;
        for(int i=0;i<n;i++)
        {
            int d=scanner.nextInt(),r=scanner.nextInt(),g=scanner.nextInt();
            time+=d-lastPlace;
            lastPlace=d;
            if(time%(r+g)<r)// cheragh ghermeze
            {
                time+=r-(time%(r+g));
            }
        }
        time+=l-lastPlace;
        System.out.println(time);
    }
}