// Copy from Quera

import java.util.Scanner;

public class Main {

	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		int q = input.nextInt();
		int p[] = new int[q];
		for (int i = 0; i < q; i++)
			p[i] = input.nextInt();

		int k = 0;
		for (int i = 1; i <= 1000; i++) {
			boolean c = false;
			for (int z = 0; z < q; z++)
				if (i % p[z] == 0)
					c = true;
				else {
					c = false;
					break;
				}
			if (c == true)
				k++;
		}
		System.out.println(k);

	}

}