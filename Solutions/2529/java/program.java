// Copy from Quera

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        
        int n = scanner.nextInt();
        
        int answer = 0;
        for (int i = 0; i < n; i++) {
            
            String word = scanner.next();
            char[] s = word.toCharArray();
            int distinctCharacters = word.length();
            
            for (int j = 0; j < s.length; j++) {
                
                int duplicate = 0;
                
                for (int k = 0; k < j; k++)
                    if (s[k] == s[j])
                        duplicate = 1;
            
                distinctCharacters -= duplicate;
            }
            
            if (answer < distinctCharacters)
                answer = distinctCharacters;
        }
        
        System.out.println(answer);
    }
}