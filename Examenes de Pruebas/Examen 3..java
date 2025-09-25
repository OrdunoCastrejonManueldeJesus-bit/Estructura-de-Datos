import java.util.Scanner;

public class ContadorParesImpares {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int[] numeros = new int[10];
        int contadorPares = 0;
        int contadorImpares = 0;
        int suma = 0;
        
        System.out.println("Contador de Pares/Impares");
        System.out.println("\n");
        System.out.println("Ingresa 10 numeros:");
        
        // Leer los 10 números
        for (int i = 0; i < 10; i++) {
            System.out.print("Numero " + (i + 1) + ": ");
            numeros[i] = scanner.nextInt();
            suma += numeros[i];
            // Contar pares e impares
            if (numeros[i] % 2 == 0) {
                contadorPares++;
            } else {
                contadorImpares++;
            }
        }
        
        // Calcular promedio
        double promedio = (double) suma / 10;
        
        // Mostrar resultados
        System.out.println("\n");
        System.out.println("---------------------RESULTADOS--------------------");
        System.out.println("Numeros ingresados:");
        for (int i = 0; i < 10; i++) {
            System.out.print(numeros[i] + " ");
        }
        
        System.out.println("Cantidad de numeros pares: " + contadorPares);
        System.out.println("\n");
        
        System.out.println("Cantidad de numeros impares: " + contadorImpares);
        System.out.println("\n");
        
        System.out.println("Suma total: " + suma);
        System.out.println("\n");
         
        System.out.println("Promedio: " + promedio);
        System.out.println("\n");
        
         System.out.println("------------------------------------------------");
        scanner.close();
    }
}
