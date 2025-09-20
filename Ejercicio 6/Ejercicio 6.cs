using System;

class OrdenamientoInsercionSimple
{
    static void MostrarArray(int[] array)
    {
        foreach (int num in array)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        const int TAMANO = 15;
        int[] array = new int[TAMANO];
        
        // Semilla para números aleatorios
        Random random = new Random();
        
        // Generar números aleatorios
        for (int i = 0; i < TAMANO; i++)
        {
            array[i] = random.Next(1, 101); // 1 a 100
        }
        
        Console.WriteLine("Array original:");
        MostrarArray(array);
        
        // Ordenamiento por inserción
        for (int i = 1; i < TAMANO; i++)
        {
            int elementoActual = array[i];
            int j = i - 1;
            
            while (j >= 0 && array[j] > elementoActual)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = elementoActual;
        }
        
        Console.WriteLine("Array ordenado:");
        MostrarArray(array);
    }
}