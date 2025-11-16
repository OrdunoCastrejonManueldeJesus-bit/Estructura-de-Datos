using System;

// Clase del nodo
public class Nodo
{
    public int dato;
    public Nodo siguiente;
    public Nodo anterior;
    
    public Nodo(int valor)
    {
        dato = valor;
        siguiente = null;
        anterior = null;
    }
}

// Clase de Lista Doblemente Enlazada
public class ListaDoble
{
    private Nodo cabeza;
    private Nodo cola;
    
    // Constructor
    public ListaDoble()
    {
        cabeza = null;
        cola = null;
    }
    
    // Método para insertar al final
    public void InsertarFinal(int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);
        
        if (cabeza == null)
        {
            // Lista vacía
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            // Insertar al final
            cola.siguiente = nuevoNodo;
            nuevoNodo.anterior = cola;
            cola = nuevoNodo;
        }
    }
    
    // Método para mostrar la lista de inicio a fin
    public void MostrarAdelante()
    {
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }
        
        Nodo actual = cabeza;
        Console.Write("Lista (adelante): ");
        while (actual != null)
        {
            Console.Write(actual.dato + " ");
            actual = actual.siguiente;
        }
        Console.WriteLine();
    }
    
    // Método para mostrar la lista de fin a inicio
    public void MostrarAtras()
    {
        if (cola == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }
        
        Nodo actual = cola;
        Console.Write("Lista (atrás): ");
        while (actual != null)
        {
            Console.Write(actual.dato + " ");
            actual = actual.anterior;
        }
        Console.WriteLine();
    }
    
    // Método para generar lista con valores aleatorios
    public void GenerarListaAleatoria(int cantidad)
    {
        Random random = new Random();
        
        for (int i = 0; i < cantidad; i++)
        {
            int valor = random.Next(1, 101); // Números entre 1 y 100
            InsertarFinal(valor);
        }
    }
}

// Clase principal
public class Program
{
    public static void Main()
    {
        ListaDoble lista = new ListaDoble();
        
        Console.WriteLine("=== LISTA DOBLEMENTE ENLAZADA CON VALORES ALEATORIOS ===");
        
        // Generar lista con 15 valores aleatorios
        lista.GenerarListaAleatoria(15);
        
        // Mostrar la lista en ambas direcciones
        lista.MostrarAdelante();
        lista.MostrarAtras();
    }
}