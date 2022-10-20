// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        Scanner scanner=new Scanner(System.in);
        int x=scanner.nextInt(),y=scanner.nextInt();
        int x1=scanner.nextInt(),y1=scanner.nextInt();
        if(x1>x) System.out.println("Right");
        else System.out.println("Left");
    }
}