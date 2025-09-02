using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] arreglo = {12, 23, 57, 45, 34, 67, 89, 98};
        int valorBuscado = 23;
        
        List<int> indices = new List<int>();
        
        for (int index = 0; index < arreglo.Length; index++)
        {
            if (arreglo[index] == valorBuscado)
            {
                indices.Add(index);
            }
        }
        
        if (indices.Count > 0)
        {
            Console.WriteLine("Valor encontrado en los índices: " + string.Join(", ", indices));
        }
        else
        {
            Console.WriteLine("Valor no encontrado en el arreglo.");
        }
    }
}