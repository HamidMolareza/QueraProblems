// Copy from Quera

import java.util.Scanner;
public class Main {
    public static void main(String[] args) {
        Scanner s = new Scanner(System.in);
        String label = s.next();
        int R = 0, G = 0, Y = 0;
        
        if(label.charAt(0) == 'R') R++;
        else if(label.charAt(0) == 'G') G++;
        else Y++;
        
        if(label.charAt(1) == 'R') R++;
        else if(label.charAt(1) == 'G') G++;
        else Y++;
        
        if(label.charAt(2) == 'R') R++;
        else if(label.charAt(2) == 'G') G++;
        else Y++;
        
        if(label.charAt(3) == 'R') R++;
        else if(label.charAt(3) == 'G') G++;
        else Y++;
        
        if(label.charAt(4) == 'R') R++;
        else if(label.charAt(4) == 'G') G++;
        else Y++;
        
        if( (R>=3) || (R==2 && Y>=2) || (G==0) ) System.out.println("nakhor lite");
        else System.out.println("rahat baash");
    }
}