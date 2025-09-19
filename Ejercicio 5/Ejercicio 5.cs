using System;

class OrdenamientoAleatorio
{
    static int BubbleSort(int[] arr)
    {
        int n = arr.Length;
        bool swapped;
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

    static void Main()
    {
        // Semilla para números aleatorios
        Random random = new Random();
        
        // Crear un array de 15 elementos
        const int SIZE = 15;
        int[] array = new int[SIZE];
        
        // Generar 15 valores aleatorios entre 1 y 100
        Console.WriteLine("Array original (valores aleatorios):");
        for (int i = 0; i < SIZE; i++) {
            array[i] = random.Next(1, 101); // Números entre 1 y 100
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
        
        // Aplicar el algoritmo Bubble Sort
        int swapCounter = BubbleSort(array);
        
        // Mostrar el array ordenado
        Console.WriteLine("\nArray ordenado:");
        for (int i = 0; i < SIZE; i++) {
            Console.Write(array[i] + " ");
        }
        
        Console.WriteLine("\n\nTotal de intercambios realizados: " + swapCounter);
    }
}