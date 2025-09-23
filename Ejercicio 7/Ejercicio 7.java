import java.util.Random;

public class OrdenamientoSeleccion {
    public static void main(String[] args) {
        // Crear array con 15 valores aleatorios
        int[] array = new int[15];
        Random random = new Random();
        
        // Llenar el array con valores aleatorios entre 1 y 100
        System.out.println("Array original:");
        for (int i = 0; i < array.length; i++) {
            array[i] = random.nextInt(100) + 1;
            System.out.print(array[i] + " ");
        }
        System.out.println();
        
        // Aplicar ordenamiento por selección
        ordenamientoPorSeleccion(array);
        
        // Mostrar array ordenado
        System.out.println("Array ordenado:");
        for (int num : array) {
            System.out.print(num + " ");
        }
    }
    
    public static void ordenamientoPorSeleccion(int[] array) {
        int n = array.length;
        
        // repeat (numOfElements - 1) times
        for (int i = 0; i < n - 1; i++) {
            // set the first unsorted element as the minimum
            int indiceMinimo = i;
            
            // for each of the unsorted elements
            for (int j = i + 1; j < n; j++) {
                // if element < currentMinimum
                if (array[j] < array[indiceMinimo]) {
                    // set element as new minimum
                    indiceMinimo = j;
                }
            }
            
            // swap minimum with first unsorted position
            if (indiceMinimo != i) {
                int temp = array[i];
                array[i] = array[indiceMinimo];
                array[indiceMinimo] = temp;
            }
        }
    }
}