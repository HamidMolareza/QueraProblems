// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner=new Scanner(System.in);
        int n = scanner.nextInt(),m=scanner.nextInt();
        String [] a = new String[51];
        
        for(int i = 0; i < n; i++)
            a[i]=scanner.next();
        
        String pattern = scanner.next();

        int ans = 0;
        for(int i = 0; i < n; i++)
            for(int j = 0; j + pattern.length() <= m; j++)
            {
                boolean equal = true;
                for(int k = 0; k < pattern.length(); k++)
                    if(pattern.charAt(k) != a[i].charAt(j + k))
                    {
                        equal=false;
                        break;
                    }
                if(equal) ans++;
            }
        for(int i = 0; i + pattern.length() <= n; i++)
            for(int j = 0; j < m; j++)
            {
                boolean equal = true;
                for(int k = 0; k < pattern.length(); k++)
                    if(pattern.charAt(k) != a[i + k].charAt(j))
                    {
                        equal = false;
                        break;
                    }
                if(equal) ans++;
            }
        System.out.println(ans);
    }

}