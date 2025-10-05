using System;
using System.Collections.Generic;

public class MergeSort
{
    // Método para combinar dos subarrays ordenados
    private static void Merge(int[] array, int left, int middle, int right)
    {
        // Tamaños de los subarrays temporales
        int n1 = middle - left + 1;
        int n2 = right - middle;
        
        // Arrays temporales
        int[] leftArray = new int[n1];
        int[] rightArray = new int[n2];
        
        // Copia datos a los arrays temporales
        for (int i = 0; i < n1; i++)
        {
            leftArray[i] = array[left + i];
        }
        for (int j = 0; j < n2; j++)
        {
            rightArray[j] = array[middle + 1 + j];
        }
        
        // Combina los arrays temporales
        int i_idx = 0, j_idx = 0;
        int k = left;
        
        while (i_idx < n1 && j_idx < n2)
        {
            if (leftArray[i_idx] <= rightArray[j_idx])
            {
                array[k] = leftArray[i_idx];
                i_idx++;
            }
            else
            {
                array[k] = rightArray[j_idx];
                j_idx++;
            }
            k++;
        }
        
        // Copia los elementos restantes de leftArray (si hay)
        while (i_idx < n1)
        {
            array[k] = leftArray[i_idx];
            i_idx++;
            k++;
        }
        
        // Copia los elementos restantes de rightArray (si hay)
        while (j_idx < n2)
        {
            array[k] = rightArray[j_idx];
            j_idx++;
            k++;
        }
    }
    
    // Método principal que implementa Merge Sort
    public static void MergeSortArray(int[] array, int left, int right)
    {
        if (left < right)
        {
            // Encuentra el punto medio
            int middle = (left + right) / 2;
            
            // Ordena la primera y segunda mitad
            MergeSortArray(array, left, middle);
            MergeSortArray(array, middle + 1, right);
            
            // Combina las mitades ordenadas
            Merge(array, left, middle, right);
        }
    }
    
    // Método para generar números aleatorios
    public static int[] GenerarArrayAleatorio(int tamaño, int maximo)
    {
        int[] array = new int[tamaño];
        Random random = new Random();
        
        for (int i = 0; i < tamaño; i++)
        {
            array[i] = random.Next(1, maximo + 1); // Números entre 1 y maximo
        }
        
        return array;
    }
    
    // Función para imprimir un array
    public static void ImprimirArray(int[] array)
    {
        Console.Write("[");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i]);
            if (i < array.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.Write("]");
    }
    
    // Método adicional para demostrar el proceso paso a paso
    public static void DemostrarMergeSort()
    {
        int[] demo = {38, 27, 43, 3, 9, 82, 10};
        Console.WriteLine("Ejemplo paso a paso con array pequeño:");
        Console.Write("Array inicial: ");
        ImprimirArray(demo);
        Console.WriteLine();
        
        MergeSortArray(demo, 0, demo.Length - 1);
        
        Console.Write("Array final: ");
        ImprimirArray(demo);
        Console.WriteLine();
    }
    
    // Método principal
    public static void Main(string[] args)
    {
        // Generar 15 números aleatorios entre 1 y 100
        int[] numeros = GenerarArrayAleatorio(15, 100);
        
        Console.WriteLine("=== ORDENAMIENTO MERGE SORT ===");
        Console.WriteLine("Array original:");
        ImprimirArray(numeros);
        Console.WriteLine();
        
        // Aplicar Merge Sort
        MergeSortArray(numeros, 0, numeros.Length - 1);
        
        Console.WriteLine("\nArray ordenado:");
        ImprimirArray(numeros);
        Console.WriteLine();
        
        // Mostrar paso a paso (opcional)
        Console.WriteLine("\n=== DEMOSTRACION PASO A PASO ===");
        DemostrarMergeSort();
    }
}