using System;
using System.Collections.Generic;

public class HashSortSimplificado
{
    public static void Main(string[] args)
    {
        int[] valores = GenerarValoresAleatorios(15, 100);
        
        Console.WriteLine("Valores originales:");
        Console.WriteLine(ArrayToString(valores));
        int[] valoresOrdenados = HashSortSimplificadoMethod(valores);
        
        Console.WriteLine("\nValores ordenados:");
        Console.WriteLine(ArrayToString(valoresOrdenados));
    }
    
    public static string ArrayToString(int[] arr)
    {
        return "[" + string.Join(", ", arr) + "]";
    }

    public static int[] GenerarValoresAleatorios(int cantidad, int max)
    {
        Random random = new Random();
        int[] valores = new int[cantidad];
        
        for (int i = 0; i < cantidad; i++)
        {
            valores[i] = random.Next(max);
        }
        
        return valores;
    }

    public static int[] HashSortSimplificadoMethod(int[] valores)
    {
        if (valores.Length == 0)
        {
            return new int[0];
        }
        
        int max = EncontrarMaximo(valores);
        
        bool[] tablaHash = new bool[max + 1];
        
        foreach (int valor in valores)
        {
            if (valor >= 0 && valor < tablaHash.Length)
            {
                tablaHash[valor] = true;
            }
        }
        
        List<int> ordenados = new List<int>();
        for (int i = 0; i < tablaHash.Length; i++)
        {
            if (tablaHash[i])
            {
                ordenados.Add(i);
            }
        }
        
        return ordenados.ToArray();
    }
    
    public static int EncontrarMaximo(int[] valores)
    {
        int max = valores[0];
        for (int i = 1; i < valores.Length; i++)
        {
            if (valores[i] > max)
            {
                max = valores[i];
            }
        }
        return max;
    }
    
    public static int[] HashSortConTreeSet(int[] valores)
    {
        SortedSet<int> set = new SortedSet<int>();
        
        foreach (int valor in valores)
        {
            set.Add(valor);
        }
        
        int[] resultado = new int[set.Count];
        int index = 0;
        foreach (int valor in set)
        {
            resultado[index++] = valor;
        }
        
        return resultado;
    }
}