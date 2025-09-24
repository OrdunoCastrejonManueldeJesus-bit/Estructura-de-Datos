using System;
using System.Collections.Generic;

public class QuickSortSimplificado
{
    private static Random random = new Random();
    
    public static void Main(string[] args)
    {
        // Generar 15 valores aleatorios
        int[] array = GenerarArrayAleatorio(15);
        
        Console.WriteLine("Array original:");
        ImprimirArray(array);
        
        // Ordenar con Quick Sort simplificado
        QuickSort(array, 0, array.Length - 1);
        
        Console.WriteLine("\nArray ordenado:");
        ImprimirArray(array);
    }
    
    // Generar array con valores aleatorios entre 1 y 100
    public static int[] GenerarArrayAleatorio(int tamaño)
    {
        int[] array = new int[tamaño];
        
        for (int i = 0; i < tamaño; i++)
        {
            array[i] = random.Next(1, 101); // Valores entre 1 y 100
        }
        
        return array;
    }
    
    // Algoritmo Quick Sort simplificado
    public static void QuickSort(int[] array, int izquierda, int derecha)
    {
        if (izquierda < derecha)
        {
            int indicePivote = Particion(array, izquierda, derecha);
            
            // Ordenar recursivamente las dos particiones
            QuickSort(array, izquierda, indicePivote - 1);
            QuickSort(array, indicePivote + 1, derecha);
        }
    }
    
    // Función de partición según la descripción
    public static int Particion(int[] array, int izquierda, int derecha)
    {
        // El primer elemento como pivote
        int pivote = array[izquierda];
        int indicePivote = izquierda;
        int storeIndex = izquierda + 1;
        
        for (int i = izquierda + 1; i <= derecha; i++)
        {
            bool debeIntercambiar = false;
            
            // Verificar si debe intercambiar según las condiciones
            if (array[i] < pivote)
            {
                debeIntercambiar = true;
            }
            else if (array[i] == pivote)
            {
                // 50% de probabilidad cuando son iguales
                if (random.NextDouble() < 0.5)
                {
                    debeIntercambiar = true;
                }
            }
            
            if (debeIntercambiar)
            {
                Intercambiar(array, i, storeIndex);
                storeIndex++;
            }
        }
        
        // Colocar el pivote en su posición final
        Intercambiar(array, indicePivote, storeIndex - 1);
        
        return storeIndex - 1;
    }
    
    // Función auxiliar para intercambiar elementos
    public static void Intercambiar(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    
    // Función para imprimir el array
    public static void ImprimirArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i]);
            if (i < array.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine();
    }
}