// Copy from Quera

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int n = scanner.nextInt(), m = scanner.nextInt();
        
        char[][] a = new char[n][m];
        for (int i = 0; i < n; i++)
            a[i] = scanner.next().toCharArray();
        
        int ans = 0;
        for (int i = 0; i < n; i++) 
            for (int j = 0; j < m; j++)
                if (a[i][j] == '*')
                    ans++;

        System.out.println(ans);
    }
}