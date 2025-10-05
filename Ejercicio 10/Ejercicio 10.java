import java.util.Random;

public class HeapSort {
    
    public static void heapSort(int[] arr) {
        int n = arr.length;
        
        for (int i = n / 2 - 1; i >= 0; i--) {
            heapify(arr, n, i);
        }
        for (int i = n - 1; i > 0; i--) {
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            
            heapify(arr, i, 0);
        }
    }

    static void heapify(int[] arr, int n, int i) {
        int largest = i; 
        int left = 2 * i + 1; 
        int right = 2 * i + 2; 
        
        if (left < n && arr[left] > arr[largest]) {
            largest = left;
        }
        
        if (right < n && arr[right] > arr[largest]) {
            largest = right;
        }
        
        if (largest != i) {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;
            
            heapify(arr, n, largest);
        }
    }
    
    static void printArray(int[] arr) {
        for (int i = 0; i < arr.length; i++) {
            System.out.print(arr[i] + " ");
        }
        System.out.println();
    }
    
    static int[] generarArrayAleatorio(int tamaño, int min, int max) {
        int[] arr = new int[tamaño];
        Random random = new Random();
        
        for (int i = 0; i < tamaño; i++) {
            arr[i] = random.nextInt(max - min + 1) + min;
        }
        return arr;
    }
    
    public static void main(String[] args) {
        int[] arr = generarArrayAleatorio(15, 1, 100);
        
        System.out.println("Array original (15 valores aleatorios):");
        printArray(arr);
        
        heapSort(arr);
        
        System.out.println("\nArray ordenado con Heap Sort:");
        printArray(arr);
    }
}