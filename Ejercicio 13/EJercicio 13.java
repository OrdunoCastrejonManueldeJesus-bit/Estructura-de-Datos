import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class RadixSort {
    private static final int ARRAY_SIZE = 15;
    private static final int MIN_VAL = 1;
    private static final int MAX_VAL = 1000;

    // Función para imprimir el arreglo
    public static void printArray(List<Integer> arr) {
        for (int val : arr) {
            System.out.print(val + " ");
        }
        System.out.println();
    }

    // 1. Obtener el valor máximo en el arreglo
    public static int getMax(List<Integer> arr) {
        int maxVal = arr.get(0);
        for (int i = 1; i < arr.size(); i++) {
            if (arr.get(i) > maxVal) {
                maxVal = arr.get(i);
            }
        }
        return maxVal;
    }

    // 2. Función de Counting Sort 
    public static void countSort(List<Integer> arr, int exp) {
        int n = arr.size();
        List<Integer> output = new ArrayList<>(n);
        for (int i = 0; i < n; i++) {
            output.add(0);
        }
        
        List<Integer> count = new ArrayList<>(10);
        for (int i = 0; i < 10; i++) {
            count.add(0);
        }

        for (int i = 0; i < n; i++) {
            int digit = (arr.get(i) / exp) % 10;
            count.set(digit, count.get(digit) + 1);
        }

        for (int i = 1; i < 10; i++) {
            count.set(i, count.get(i) + count.get(i - 1));
        }

        for (int i = n - 1; i >= 0; i--) {
            int digit = (arr.get(i) / exp) % 10;
            output.set(count.get(digit) - 1, arr.get(i));
            count.set(digit, count.get(digit) - 1);
        }

        for (int i = 0; i < n; i++) {
            arr.set(i, output.get(i));
        }
    }

    // 3. Función principal de Radix Sort
    public static void radixSort(List<Integer> arr) {
        int m = getMax(arr);

        for (int exp = 1; m / exp > 0; exp *= 10) {
            countSort(arr, exp);
            System.out.print(" -> Arreglo después de la pasada con exp = " + exp + ": ");
            printArray(arr);
        }
    }

    public static void main(String[] args) {
        Random rand = new Random();

        // 4. Generar el arreglo de 15 valores aleatorios entre 1 y 1000
        List<Integer> data = new ArrayList<>(ARRAY_SIZE);
        for (int i = 0; i < ARRAY_SIZE; i++) {
            // Genera números en el rango [1, 1000]
            data.add(rand.nextInt(MAX_VAL - MIN_VAL + 1) + MIN_VAL);
        }

        System.out.println("--- Radix Sort (Simplificado) ---");
        System.out.println("Valores a ordenar: " + ARRAY_SIZE);
        System.out.println("Rango de valores: [" + MIN_VAL + ", " + MAX_VAL + "]");
        
        // Imprimir el arreglo no ordenado
        System.out.print("\nArreglo Original: ");
        printArray(data);

        // Llamar al algoritmo Radix Sort
        System.out.println("\nIniciando Radix Sort...");
        radixSort(data);

        // Imprimir el arreglo ordenado final
        System.out.print("\nArreglo Final Ordenado: ");
        printArray(data);
        System.out.println("-----------------------------------");
    }
}