using System;

class Program
{
    static void Main()
    {
        // Declarar e inicializar una matriz 3x3
        int[,] matriz = {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };
        
        Console.WriteLine("Matriz 3x3 original:");
        // Mostrar la matriz original
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(matriz[i, j] + " ");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine("\nRecorrido por filas:");
        // Recorrido por filas
        for (int fila = 0; fila < 3; fila++)
        {
            Console.Write("Fila " + (fila + 1) + ": ");
            for (int columna = 0; columna < 3; columna++)
            {
                Console.Write(matriz[fila, columna] + " ");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine("\nRecorrido completo por filas:");
        // Recorrido completo fila por fila
        for (int fila = 0; fila < 3; fila++)
        {
            for (int columna = 0; columna < 3; columna++)
            {
                Console.WriteLine("Elemento [" + fila + "][" + columna + "] = " 
                     + matriz[fila, columna]);
            }
        }
    }
}