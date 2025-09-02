using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numeros = new List<int> { 10, 20, 30, 40, 50 };
        int valorAInsertar = 75;
        int indice = 2;

        if (indice >= 0 && indice <= numeros.Count)
        {
            numeros.Insert(indice, valorAInsertar);
        }
        else
        {
            Console.WriteLine("Índice fuera de rango.");
        }

        // Mostrar el resultado
        Console.WriteLine("Lista actualizada: " + string.Join(", ", numeros));
    }
}