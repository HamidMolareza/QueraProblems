// Copy from Quera

import java.util.Scanner;
public class Main{
    public static void main(String[] args){
        Scanner scanner = new Scanner(System.in);
        int n = scanner.nextInt(), k = scanner.nextInt();
        int [] arr = new int[n+1];
        arr[1] = 1;
        for(int i = 2; i<= n; i++){
            if(i == n && arr[i-1] + k <= n ){
                arr[i] = arr[i-1] + k;
                break;
            }
            if(i%2 == 0){
                if(arr[i-1]+(k + 1) > n){
                    System.out.println("Impossible");
                    return;
                }
                arr[i] = arr[i-1] + (k + 1);
            }else if(i % 2 == 1){
                arr[i] = arr[i-1] - k;
            }
        }
        for(int i = 1; i <=n; i++)System.out.print(arr[i] + " ");
    }
}