// Copy from Quera

import java.util.*;
import java.lang.Math;

class Main
{
	public static void main (String[] args)
	{
	    Scanner input = new Scanner(System.in);
	    long a = input.nextLong(), b = input.nextLong(), c = input.nextLong();
		if (a == b && b == c)
    		System.out.println("0");
    	else if (Math.abs(a - b) == Math.abs(b - c) ||
            	Math.abs(a - c) == Math.abs(b - c) ||
            	Math.abs(a - b) == Math.abs(a - c) ||
            	(a == b && a != c && b != c) ||
            	(a == c && a != b && b != c) ||
            	(b == c && a != b && a != c))
    	    System.out.println("1");
    	else
    	    System.out.println("2");
	}
}
