// Copy from Quera

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        
        Scanner scanner=new Scanner(System.in);
        
        int hour = scanner.nextInt(), minute = scanner.nextInt();
        
        hour = 12 - hour;
        hour = hour % 12;
        
        minute = 60 - minute;
        minute = minute % 60;
        
        System.out.printf("%02d:%02d", hour, minute);
    }
}