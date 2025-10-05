import java.util.Arrays;
import java.util.Random;

public class MergeSort {
    
    // Método principal que implementa Merge Sort
    public static void mergeSort(int[] array, int left, int right) {
        if (left < right) {
            // Encuentra el punto medio
            int middle = (left + right) / 2;
            
            // Ordena la primera y segunda mitad
            mergeSort(array, left, middle);
            mergeSort(array, middle + 1, right);
            
            // Combina las mitades ordenadas
            merge(array, left, middle, right);
        }
    }
    
    // Método para combinar dos subarrays ordenados
    private static void merge(int[] array, int left, int middle, int right) {
        // Tamaños de los subarrays temporales
        int n1 = middle - left + 1;
        int n2 = right - middle;
        
        // Arrays temporales
        int[] leftArray = new int[n1];
        int[] rightArray = new int[n2];
        
        // Copia datos a los arrays temporales
        for (int i = 0; i < n1; i++) {
            leftArray[i] = array[left + i];
        }
        for (int j = 0; j < n2; j++) {
            rightArray[j] = array[middle + 1 + j];
        }
        
        // Combina los arrays temporales
        int i = 0, j = 0;
        int k = left;
        
        while (i < n1 && j < n2) {
            if (leftArray[i] <= rightArray[j]) {
                array[k] = leftArray[i];
                i++;
            } else {
                array[k] = rightArray[j];
                j++;
            }
            k++;
        }
        
        // Copia los elementos restantes de leftArray (si hay)
        while (i < n1) {
            array[k] = leftArray[i];
            i++;
            k++;
        }
        
        // Copia los elementos restantes de rightArray (si hay)
        while (j < n2) {
            array[k] = rightArray[j];
            j++;
            k++;
        }
    }
    
    // Método para generar números aleatorios
    public static int[] generarArrayAleatorio(int tamaño, int maximo) {
        Random random = new Random();
        int[] array = new int[tamaño];
        
        for (int i = 0; i < tamaño; i++) {
            array[i] = random.nextInt(maximo) + 1; // Números entre 1 y maximo
        }
        
        return array;
    }
    
    // Método principal
    public static void main(String[] args) {
        // Generar 15 números aleatorios entre 1 y 100
        int[] numeros = generarArrayAleatorio(15, 100);
        
        System.out.println("=== ORDENAMIENTO MERGE SORT ===");
        System.out.println("Array original:");
        System.out.println(Arrays.toString(numeros));
        
        // Aplicar Merge Sort
        mergeSort(numeros, 0, numeros.length - 1);
        
        System.out.println("\nArray ordenado:");
        System.out.println(Arrays.toString(numeros));
        
        // Mostrar paso a paso (opcional)
        System.out.println("\n=== DEMOSTRACION PASO A PASO ===");
        demostrarMergeSort();
    }
    
    // Método adicional para demostrar el proceso paso a paso
    public static void demostrarMergeSort() {
        int[] demo = {38, 27, 43, 3, 9, 82, 10};
        System.out.println("Ejemplo paso a paso con array pequeño:");
        System.out.println("Array inicial: " + Arrays.toString(demo));
        
        mergeSort(demo, 0, demo.length - 1);
        
        System.out.println("Array final: " + Arrays.toString(demo));
    }
}