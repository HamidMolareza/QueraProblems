// Copy from Quera

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int weight = scanner.nextInt();
        double height = scanner.nextDouble();

        double bmi = weight/(height * height);
        System.out.format("%.2f", bmi);
        System.out.println();
        if(bmi < 18.5) {
            System.out.println("Underweight");
        } else if(bmi >= 18.5 && bmi < 25) {
            System.out.println("Normal");
        } else if(bmi >= 25 && bmi < 30) {
            System.out.println("Overweight");
        } else if(bmi >= 30) {
            System.out.println("Obese");
        }
    }
}
