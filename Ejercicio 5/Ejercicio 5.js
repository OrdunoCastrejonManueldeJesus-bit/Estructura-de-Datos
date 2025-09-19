import java.util.Random;

public class OrdenamientoAleatorio {
    public static void main(String[] args) {
        // Crear un array de 15 elementos
        int[] array = new int[15];
        Random random = new Random();
        
        // Generar 15 valores aleatorios entre 1 y 100
        System.out.println("Array original (valores aleatorios):");
        for (int i = 0; i < array.length; i++) {
            array[i] = random.nextInt(100) + 1; // Números entre 1 y 100
            System.out.print(array[i] + " ");
        }
        System.out.println();
        
        // Aplicar el algoritmo Bubble Sort
        int swapCounter = bubbleSort(array);
        
        // Mostrar el array ordenado
        System.out.println("\nArray ordenado:");
        for (int i = 0; i < array.length; i++) {
            System.out.print(array[i] + " ");
        }
        
        System.out.println("\n\nTotal de intercambios realizados: " + swapCounter);
    }
    
    public static int bubbleSort(int[] arr) {
        int n = arr.length;
        boolean swapped;
        int swapCounter = 0;
        
        do {
            swapped = false;
            for (int i = 1; i < n; i++) {
                if (arr[i - 1] > arr[i]) {
                    // Intercambiar elementos
                    int temp = arr[i - 1];
                    arr[i - 1] = arr[i];
                    arr[i] = temp;
                    
                    swapped = true;
                    swapCounter++;
                }
            }
            // Reducir el rango en cada iteración ya que el último elemento está en su posición correcta
            n--;
        } while (swapped);
        
        return swapCounter;
    }
}