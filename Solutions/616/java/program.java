// Copy from Quera

import java.util.*;

class Main
{
	public static void main (String[] args)
	{
		Scanner input = new Scanner(System.in);
		int n = input.nextInt();
		int ans=1;
		while(ans<=n)
		    ans*=2;
		System.out.println(ans);
	}
}