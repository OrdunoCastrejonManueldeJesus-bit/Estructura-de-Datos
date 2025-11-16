using System;

class Program
{
    // listas enlazadas
    class Nodo
    {
        public int valor;
        public Nodo siguiente;
    }

    static void Main()
    {
        Random rand = new Random();
        
        Nodo cabeza = null;
        Nodo actual = null;
        
        Console.WriteLine("Generando 15 valores aleatorios");
        Console.WriteLine(" ");
        // Crear 15 nodos con valores aleatorios
        for (int i = 0; i < 15; i++)
        {
            Nodo nuevoNodo = new Nodo();
            nuevoNodo.valor = 1 + rand.Next(100); // 1-100
            nuevoNodo.siguiente = null;
            
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                actual = cabeza;
            }
            else
            {
                actual.siguiente = nuevoNodo;
                actual = nuevoNodo;
            }
        }
        
        // Mostrar la lista
        Console.WriteLine("Lista Enlazada:");
        actual = cabeza;
        int contador = 1;
        while (actual != null)
        {
            Console.WriteLine($"Nodo {contador}: {actual.valor}");
            actual = actual.siguiente;
            contador++;
        }
    }
}