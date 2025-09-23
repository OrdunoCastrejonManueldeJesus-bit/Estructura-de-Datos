using System;

class OrdenamientoSeleccion
{
    static void Main()
    {
        // Semilla para números aleatorios
        Random random = new Random();
        
        // Crear array con 15 valores aleatorios
        const int TAMANO = 15;
        int[] array = new int[TAMANO];
        
        // Llenar el array con valores aleatorios entre 1 y 100
        Console.WriteLine("Array original:");
        for (int i = 0; i < TAMANO; i++)
        {
            array[i] = random.Next(1, 101);
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
        
        // Aplicar ordenamiento por selección
        OrdenamientoPorSeleccion(array);
        
        // Mostrar array ordenado
        Console.WriteLine("Array ordenado:");
        foreach (int num in array)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
    
    static void OrdenamientoPorSeleccion(int[] array)
    {
        int n = array.Length;
        
        // repeat (numOfElements - 1) times
        for (int i = 0; i < n - 1; i++)
        {
            // set the first unsorted element as the minimum
            int indiceMinimo = i;
            
            // for each of the unsorted elements
            for (int j = i + 1; j < n; j++)
            {
                // if element < currentMinimum
                if (array[j] < array[indiceMinimo])
                {
                    // set element as new minimum
                    indiceMinimo = j;
                }
            }
            
            // swap minimum with first unsorted position
            if (indiceMinimo != i)
            {
                int temp = array[i];
                array[i] = array[indiceMinimo];
                array[indiceMinimo] = temp;
            }
        }
    }
}