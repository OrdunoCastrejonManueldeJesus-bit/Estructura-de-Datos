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
        
        System.out.println("\nRecorrido por columnas:");
        // Recorrido por columnas
        for (int columna = 0; columna < 3; columna++) {
            System.out.print("Columna " + (columna + 1) + ": ");
            for (int fila = 0; fila < 3; fila++) {
                System.out.print(matriz[fila][columna] + " ");
            }
            System.out.println();
        }
        
        System.out.println("\nRecorrido completo por columnas:");
        // Recorrido completo columna por columna
        for (int columna = 0; columna < 3; columna++) {
            for (int fila = 0; fila < 3; fila++) {
                System.out.println("Elemento [" + fila + "][" + columna + "] = " 
                     + matriz[fila][columna]);
            }
        }
    }
}