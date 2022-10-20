// Copy from Quera

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int[] arr = new int[1000];
        
        int num = scanner.nextInt();
        int i = 0;
        
        while(num != 0) {
            arr[i] = num;
            num = scanner.nextInt();
            i ++;
        }

        for (int j = i-1; j >= 0; j--)
            System.out.println(arr[j]);        
    }
}