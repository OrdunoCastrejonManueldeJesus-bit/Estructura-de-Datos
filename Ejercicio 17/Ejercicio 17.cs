using System;

// Clase del nodo
public class Nodo 
{
    public int valor;
    public Nodo siguiente;
    public Nodo anterior;
    
    public Nodo(int v) 
    {
        valor = v;
        siguiente = null;
        anterior = null;
    }
}

// Clase de Lista Circular Doble
public class ListaCircularDoble 
{
    private Nodo cabeza;
    private int tamaño;
    
    // Constructor
    public ListaCircularDoble() 
    {
        cabeza = null;
        tamaño = 0;
    }
    
    // Insertar al final
    public void InsertarFinal(int valor) 
    {
        Nodo nuevoNodo = new Nodo(valor);
        
        if (cabeza == null) 
        {
            cabeza = nuevoNodo;
            cabeza.siguiente = cabeza;
            cabeza.anterior = cabeza;
        } 
        else 
        {
            Nodo ultimo = cabeza.anterior;
            
            ultimo.siguiente = nuevoNodo;
            nuevoNodo.anterior = ultimo;
            nuevoNodo.siguiente = cabeza;
            cabeza.anterior = nuevoNodo;
        }
        
        tamaño++;
    }
    
    // Mostrar lista en sentido horario
    public void MostrarHorario() 
    {
        if (cabeza == null) 
        {
            Console.WriteLine("Lista vacía");
            return;
        }
        
        Console.Write("Lista en sentido horario: ");
        Nodo actual = cabeza;
        
        do 
        {
            Console.Write(actual.valor + " ");
            actual = actual.siguiente;
        } while (actual != cabeza);
        
        Console.WriteLine();
    }
    
    // Mostrar lista en sentido antihorario
    public void MostrarAntihorario() 
    {
        if (cabeza == null) 
        {
            Console.WriteLine("Lista vacía");
            return;
        }
        
        Console.Write("Lista en sentido antihorario: ");
        Nodo actual = cabeza.anterior;
        
        do 
        {
            Console.Write(actual.valor + " ");
            actual = actual.anterior;
        } while (actual != cabeza.anterior);
        
        Console.WriteLine();
    }
    
    // Obtener tamaño
    public int GetTamaño() 
    {
        return tamaño;
    }
    
    // Generar lista con valores aleatorios
    public void GenerarListaAleatoria(int cantidad) 
    {
        Random rand = new Random();
        
        for (int i = 0; i < cantidad; i++) 
        {
            int valor = rand.Next(1, 101); // Valores entre 1 y 100
            InsertarFinal(valor);
        }
    }
}

// Clase principal
public class Program 
{
    public static void Main(string[] args) 
    {
        ListaCircularDoble lista = new ListaCircularDoble();
        
        Console.WriteLine("=== LISTA ENLAZADA CIRCULAR DOBLE ===");
        Console.WriteLine("Generando 15 valores aleatorios");
        Console.WriteLine();
        
        // Generar lista con 15 valores aleatorios
        lista.GenerarListaAleatoria(15);
        
        // Mostrar resultados
        Console.WriteLine("Tamaño de la lista: " + lista.GetTamaño());
        
        lista.MostrarHorario();
        lista.MostrarAntihorario();
    }
}