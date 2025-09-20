import java.util.Random;

public class OrdenamientoInsercionSimple {
    public static void main(String[] args) {
        int[] array = new int[15];
        Random random = new Random();
        
        // Generar números aleatorios
        for (int i = 0; i < array.length; i++) {
            array[i] = random.nextInt(100) + 1;
        }
        
        System.out.println("Array original:");
        mostrarArray(array);
        
        // Ordenamiento por inserción
        for (int i = 1; i < array.length; i++) {
            int elementoActual = array[i];
            int j = i - 1;
            
            while (j >= 0 && array[j] > elementoActual) {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = elementoActual;
        }
        
        System.out.println("Array ordenado:");
        mostrarArray(array);
    }
    
    public static void mostrarArray(int[] array) {
        for (int num : array) {
            System.out.print(num + " ");
        }
        System.out.println();
    }
}