// Copy from Quera

import java.util.Scanner;
import java.lang.Math;
public class Main {
    static Scanner s=new Scanner(System.in);
    public static void main(String[] args) {
        
        int a = s.nextInt(), b = s.nextInt(), c = s.nextInt(), d = s.nextInt();
        
        double Sum = a + b + c + d;
        double Average = (double)(a + b + c + d) / 4;
        double Product = (a * b * c * d);
        double Max = Math.max(Math.max(a, b), Math.max(c, d));
        double Min = Math.min(Math.min(a, b), Math.min(c, d));
        
        System.out.printf("Sum : %.6f\n", Sum);
        System.out.printf("Average : %.6f\n", Average);
        System.out.printf("Product : %.6f\n", Product);
        System.out.printf("Max : %.6f\n", Max);
        System.out.printf("Min : %.6f\n", Min);
    }
}