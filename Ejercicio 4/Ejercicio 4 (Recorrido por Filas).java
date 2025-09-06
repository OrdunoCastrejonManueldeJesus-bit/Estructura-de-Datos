public class Main {
    public static void main(String[] args) {
        // Declarar e inicializar una matriz 3x3
        int[][] matriz = {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };
        
        System.out.println("Matriz 3x3 original:");
        // Mostrar la matriz original
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                System.out.print(matriz[i][j] + " ");
            }
            System.out.println();
        }
        
        System.out.println("\nRecorrido por filas:");
        // Recorrido por filas
        for (int fila = 0; fila < 3; fila++) {
            System.out.print("Fila " + (fila + 1) + ": ");
            for (int columna = 0; columna < 3; columna++) {
                System.out.print(matriz[fila][columna] + " ");
            }
            System.out.println();
        }
        
        System.out.println("\nRecorrido completo por filas:");
        // Recorrido completo fila por fila
        for (int fila = 0; fila < 3; fila++) {
            for (int columna = 0; columna < 3; columna++) {
                System.out.println("Elemento [" + fila + "][" + columna + "] = " 
                     + matriz[fila][columna]);
            }
        }
    }
}