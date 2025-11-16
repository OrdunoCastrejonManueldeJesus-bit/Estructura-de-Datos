using System;

// Estructura del nodo
public class Nodo
{
    public int valor;
    public Nodo siguiente;
    
    public Nodo(int val)
    {
        valor = val;
        siguiente = null;
    }
}

// Clase Lista Circular
public class ListaCircular
{
    private Nodo cabeza;
    private Nodo cola;
    
    // Constructor
    public ListaCircular()
    {
        cabeza = null;
        cola = null;
    }
    
    // Destructor (implementado mediante IDisposable)
    ~ListaCircular()
    {
        if (cabeza == null) return;
        
        Nodo actual = cabeza;
        Nodo temp;
        
        do {
            temp = actual;
            actual = actual.siguiente;
            // En C# no se necesita delete, el garbage collector se encarga
        } while (actual != cabeza);
    }
    
    // Insertar al final de la lista
    public void Insertar(int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);
        
        if (cabeza == null)
        {
            // Lista vacía
            cabeza = nuevoNodo;
            cola = nuevoNodo;
            nuevoNodo.siguiente = cabeza;
        }
        else
        {
            // Insertar al final
            cola.siguiente = nuevoNodo;
            cola = nuevoNodo;
            cola.siguiente = cabeza;
        }
    }
    
    // Mostrar la lista circular
    public void Mostrar()
    {
        if (cabeza == null)
        {
            Console.WriteLine("Lista vacía");
            return;
        }
        
        Nodo actual = cabeza;
        int contador = 0;
        
        Console.Write("Lista Circular: ");
        do {
            Console.Write(actual.valor);
            actual = actual.siguiente;
            contador++;
            
            if (actual != cabeza)
            {
                Console.Write(" -> ");
            }
            
            // Salto de línea cada 5 elementos para mejor visualización
            if (contador % 5 == 0 && actual != cabeza)
            {
                Console.WriteLine();
                Console.Write("               ");
            }
        } while (actual != cabeza);
        Console.WriteLine(" -> (vuelve al inicio)");
    }
    
    // Verificar si la lista está vacía
    public bool EstaVacia()
    {
        return cabeza == null;
    }
    
    // Obtener tamaño de la lista
    public int ObtenerTamano()
    {
        if (cabeza == null) return 0;
        
        int contador = 0;
        Nodo actual = cabeza;
        
        do {
            contador++;
            actual = actual.siguiente;
        } while (actual != cabeza);
        
        return contador;
    }
}

// Clase principal con el método Main
public class Program
{
    // Función para generar número aleatorio entre 1 y 100
    private static int GenerarAleatorio()
    {
        Random rand = new Random();
        return rand.Next(1, 101);
    }
    
    public static void Main()
    {
        // Semilla para números aleatorios (se maneja internamente en Random)
        
        // Crear lista circular
        ListaCircular lista = new ListaCircular();
        
        Console.WriteLine("=== LISTA ENLAZADA CIRCULAR CON 15 VALORES ALEATORIOS ===");
        Console.WriteLine("Generando valores aleatorios");
        Console.WriteLine();
        
        // Insertar 15 valores aleatorios
        for (int i = 0; i < 15; i++)
        {
            int valor = GenerarAleatorio();
            lista.Insertar(valor);
            Console.WriteLine("Insertado: " + valor);
        }
        
        Console.WriteLine();
        
        // Mostrar la lista completa
        lista.Mostrar();
    }
}