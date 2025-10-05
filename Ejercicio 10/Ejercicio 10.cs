using System;

public class HeapSort
{
    public static void HeapSortArray(int[] arr)
    {
        int n = arr.Length;
        
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Heapify(arr, n, i);
        }
        
        for (int i = n - 1; i > 0; i--)
        {
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            
            Heapify(arr, i, 0);
        }
    }

    static void Heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        
        if (left < n && arr[left] > arr[largest])
        {
            largest = left;
        }
        
        if (right < n && arr[right] > arr[largest])
        {
            largest = right;
        }
        
        if (largest != i)
        {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;
            
            Heapify(arr, n, largest);
        }
    }
    
    static void PrintArray(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();
    }
    
    static int[] GenerarArrayAleatorio(int tamaño, int min, int max)
    {
        int[] arr = new int[tamaño];
        Random random = new Random();
        
        for (int i = 0; i < tamaño; i++)
        {
            arr[i] = random.Next(min, max + 1);
        }
        return arr;
    }
    
    public static void Main(string[] args)
    {
        int[] arr = GenerarArrayAleatorio(15, 1, 100);
        
        Console.WriteLine("Array original (15 valores aleatorios):");
        PrintArray(arr);
        
        HeapSortArray(arr);
        
        Console.WriteLine("\nArray ordenado con Heap Sort:");
        PrintArray(arr);
    }
}