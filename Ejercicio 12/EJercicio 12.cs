using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    
    static void BucketSort(float[] arr, int n)
    {
        List<float>[] buckets = new List<float>[n];
        
        for (int i = 0; i < n; i++)
        {
            buckets[i] = new List<float>();
        }
        
        for (int i = 0; i < n; i++)
        {
            int bucketIndex = (int)(n * arr[i]);
            buckets[bucketIndex].Add(arr[i]);
        }
        
        for (int i = 0; i < n; i++)
        {
            buckets[i].Sort();
        }
        
        int index = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < buckets[i].Count; j++)
            {
                arr[index++] = buckets[i][j];
            }
        }
    }

    static float GenerarAleatorio()
    {
        Random rand = new Random();
        return (float)rand.Next(1000) / 1000.0f;
    }

    static void ImprimirArray(float[] arr, int n)
    {
        for (int i = 0; i < n; i++)
        {
            Console.Write(arr[i].ToString("F3") + " ");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        const int TAMANO = 15;
        float[] arr = new float[TAMANO];
        
        // Generar 15 números aleatorios entre 0 y 1
        Console.WriteLine("Generando 15 numeros aleatorios entre 0 y 1");
        for (int i = 0; i < TAMANO; i++)
        {
            arr[i] = GenerarAleatorio();
        }
        
        // Mostrar array original
        Console.WriteLine("\nArray original:");
        ImprimirArray(arr, TAMANO);
        
        // Aplicar Bucket Sort
        BucketSort(arr, TAMANO);
        
        // Mostrar array ordenado
        Console.WriteLine("\nArray ordenado con Bucket Sort:");
        ImprimirArray(arr, TAMANO);
    }
}