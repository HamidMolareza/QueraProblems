// Copy from Quera

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int n = scanner.nextInt();
		char[] s = scanner.next().toCharArray();
		char[] t = scanner.next().toCharArray();

		int ans = 0;
		for (int i = 0; i < n; i++)
			if (s[i] != t[i]) 
				ans++;

		System.out.println(ans);
    }
}
