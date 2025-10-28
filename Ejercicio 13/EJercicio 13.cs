using System;
using System.Collections.Generic;

class Program
{
    const int ARRAY_SIZE = 15;
    const int MIN_VAL = 1;
    const int MAX_VAL = 1000;

    // Función para imprimir el arreglo
    static void PrintArray(List<int> arr)
    {
        foreach (int val in arr)
        {
            Console.Write(val + " ");
        }
        Console.WriteLine();
    }

    // 1. Obtener el valor máximo en el arreglo
    static int GetMax(List<int> arr)
    {
        int maxVal = arr[0];
        for (int i = 1; i < arr.Count; i++)
        {
            if (arr[i] > maxVal)
            {
                maxVal = arr[i];
            }
        }
        return maxVal;
    }

    // 2. Función de Counting Sort 
    static void CountSort(List<int> arr, int exp)
    {
        int n = arr.Count;
        List<int> output = new List<int>(new int[n]);
        List<int> count = new List<int>(new int[10]);

        for (int i = 0; i < n; i++)
        {
            int digit = (arr[i] / exp) % 10;
            count[digit]++;
        }

        for (int i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
        }

        for (int i = n - 1; i >= 0; i--)
        {
            int digit = (arr[i] / exp) % 10;
            output[count[digit] - 1] = arr[i];
            count[digit]--;
        }

        for (int i = 0; i < n; i++)
        {
            arr[i] = output[i];
        }
    }

    // 3. Función principal de Radix Sort
    static void RadixSort(List<int> arr)
    {
        int m = GetMax(arr);

        for (int exp = 1; m / exp > 0; exp *= 10)
        {
            CountSort(arr, exp);
            Console.Write(" -> Arreglo después de la pasada con exp = " + exp + ": ");
            PrintArray(arr);
        }
    }

    static void Main()
    {
        Random rand = new Random();

        // 4. Generar el arreglo de 15 valores aleatorios entre 1 y 1000
        List<int> data = new List<int>(ARRAY_SIZE);
        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            // Genera números en el rango [1, 1000]
            data.Add(rand.Next(MIN_VAL, MAX_VAL + 1));
        }

        Console.WriteLine("--- Radix Sort (Simplificado) ---");
        Console.WriteLine("Valores a ordenar: " + ARRAY_SIZE);
        Console.WriteLine("Rango de valores: [" + MIN_VAL + ", " + MAX_VAL + "]");
        
        // Imprimir el arreglo no ordenado
        Console.Write("\nArreglo Original: ");
        PrintArray(data);

        // Llamar al algoritmo Radix Sort
        Console.WriteLine("\nIniciando Radix Sort...");
        RadixSort(data);

        // Imprimir el arreglo ordenado final
        Console.Write("\nArreglo Final Ordenado: ");
        PrintArray(data);
        Console.WriteLine("-----------------------------------");
    }
}