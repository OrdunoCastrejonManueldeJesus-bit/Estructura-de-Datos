import java.util.Random;

public class QuickSortSimplificado {
    
    public static void main(String[] args) {
        // Generar 15 valores aleatorios
        int[] array = generarArrayAleatorio(15);
        
        System.out.println("Array original:");
        imprimirArray(array);
        
        // Ordenar con Quick Sort simplificado
        quickSort(array, 0, array.length - 1);
        
        System.out.println("\nArray ordenado:");
        imprimirArray(array);
    }
    
    // Generar array con valores aleatorios entre 1 y 100
    public static int[] generarArrayAleatorio(int tamaño) {
        int[] array = new int[tamaño];
        Random random = new Random();
        
        for (int i = 0; i < tamaño; i++) {
            array[i] = random.nextInt(100) + 1; // Valores entre 1 y 100
        }
        
        return array;
    }
    
    // Algoritmo Quick Sort simplificado
    public static void quickSort(int[] array, int izquierda, int derecha) {
        if (izquierda < derecha) {
            int indicePivote = particion(array, izquierda, derecha);
            
            // Ordenar recursivamente las dos particiones
            quickSort(array, izquierda, indicePivote - 1);
            quickSort(array, indicePivote + 1, derecha);
        }
    }
    
    // Función de partición según la descripción
    public static int particion(int[] array, int izquierda, int derecha) {
        // El primer elemento como pivote
        int pivote = array[izquierda];
        int indicePivote = izquierda;
        int storeIndex = izquierda + 1;
        
        Random random = new Random();
        
        for (int i = izquierda + 1; i <= derecha; i++) {
            boolean debeIntercambiar = false;
            
            // Verificar si debe intercambiar según las condiciones
            if (array[i] < pivote) {
                debeIntercambiar = true;
            } else if (array[i] == pivote) {
                // 50% de probabilidad cuando son iguales
                if (random.nextDouble() < 0.5) {
                    debeIntercambiar = true;
                }
            }
            
            if (debeIntercambiar) {
                intercambiar(array, i, storeIndex);
                storeIndex++;
            }
        }
        
        // Colocar el pivote en su posición final
        intercambiar(array, indicePivote, storeIndex - 1);
        
        return storeIndex - 1;
    }
    
    // Función auxiliar para intercambiar elementos
    public static void intercambiar(int[] array, int i, int j) {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    
    // Función para imprimir el array
    public static void imprimirArray(int[] array) {
        for (int i = 0; i < array.length; i++) {
            System.out.print(array[i]);
            if (i < array.length - 1) {
                System.out.print(", ");
            }
        }
        System.out.println();
    }
}