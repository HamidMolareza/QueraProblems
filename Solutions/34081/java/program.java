// Copy from Quera

import java.util.*;

class Main
{
	public static void main (String[] args)
	{
		Scanner input = new Scanner(System.in);
		int n = input.nextInt(), k = input.nextInt();
		int i=0 ;
    	boolean cmd=true;
    	int sum=0 ;
    	while(true)
    	{
    		if(i==0 && cmd==false)
    			break;
    		cmd = false ;
    		i+=k;
    		if(i>=n)
    			i-=n;
    		sum++;
    	}
		System.out.println(sum);
	}
}