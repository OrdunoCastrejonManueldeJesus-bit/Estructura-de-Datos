import java.util.*;

public class BucketSort {
    public static void bucketSort(float[] arr, int n) {
        @SuppressWarnings("unchecked")
        List<Float>[] buckets = new ArrayList[n];
        
        
        for (int i = 0; i < n; i++) {
            buckets[i] = new ArrayList<>();
        }
        
        for (int i = 0; i < n; i++) {
            int bucketIndex = (int)(n * arr[i]);
            if (bucketIndex == n) bucketIndex = n - 1;
            buckets[bucketIndex].add(arr[i]);
        }
        
        for (int i = 0; i < n; i++) {
            Collections.sort(buckets[i]);
        }
        
        int index = 0;
        for (int i = 0; i < n; i++) {
            for (float value : buckets[i]) {
                arr[index++] = value;
            }
        }
    }

    // Generar número aleatorio entre 0 y 1
    public static float generarAleatorio(Random random) {
        return random.nextFloat();
    }

    // Imprimir el arreglo
    public static void imprimirArray(float[] arr, int n) {
        for (int i = 0; i < n; i++) {
            System.out.printf("%.3f ", arr[i]);
        }
        System.out.println();
    }

    public static void main(String[] args) {
        Random random = new Random();

        final int TAMANO = 15;
        float[] arr = new float[TAMANO];

        // Generar 15 números aleatorios entre 0 y 1
        System.out.println("Generando 15 numeros aleatorios entre 0 y 1");
        for (int i = 0; i < TAMANO; i++) {
            arr[i] = generarAleatorio(random);
        }

        // Mostrar array original
        System.out.println("\nArray original:");
        imprimirArray(arr, TAMANO);

        // Aplicar Bucket Sort
        bucketSort(arr, TAMANO);

        System.out.println("\nArray ordenado con Bucket Sort:");
        imprimirArray(arr, TAMANO);
    }
}
