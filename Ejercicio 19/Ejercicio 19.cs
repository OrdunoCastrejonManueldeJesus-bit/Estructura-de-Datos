using System;

// Clase Nodo para la lista enlazada
class Nodo
{
    public string dato;
    public Nodo siguiente;
    
    // Constructor
    public Nodo(string valor)
    {
        dato = valor;
        siguiente = null;
    }
}

// Clase Pila implementada con lista enlazada
class Pila
{
    private Nodo top;      // Referencia al tope de la pila
    private int capacidad;  // Capacidad máxima de la pila
    private int tamaño;     // Tamaño actual de la pila

    /**
     * @brief Constructor de la pila
     * @param cap Capacidad máxima de la pila
     */
    public Pila(int cap)
    {
        top = null;
        capacidad = cap;
        tamaño = 0;
        Console.WriteLine("\n");
        Console.WriteLine($"Pila creada con capacidad: {capacidad}");
    }

    /**
     * @brief Verifica si la pila está completamente llena
     * @return true si la pila está llena, false en caso contrario
     */
    public bool IsFull()
    {
        return tamaño == capacidad;
    }

    /**
     * @brief Verifica si la pila está vacía
     * @return true si la pila está vacía, false en caso contrario
     */
    public bool IsEmpty()
    {
        return top == null;
    }

    /**
     * @brief Inserta un elemento en la cima de la pila (operación PUSH)
     * @param valor El valor a insertar
     */
    public void Push(string valor)
    {
        if (IsFull())
        {
            Console.WriteLine($"ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar '{valor}'.");
        }
        else
        {
            // Crear nuevo nodo
            Nodo nuevoNodo = new Nodo(valor);
            
            // Insertar al inicio (tope de la pila)
            nuevoNodo.siguiente = top;
            top = nuevoNodo;
            tamaño++;
            
            Console.WriteLine($"PUSH: Elemento '{valor}' insertado en la pila.");
        }
    }

    /**
     * @brief Elimina y devuelve el elemento de la cima de la pila (operación POP)
     * @return El elemento eliminado, o cadena vacía si la pila está vacía
     */
    public string Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar.");
            return ""; // Cadena vacía para indicar error
        }
        else
        {
            // Recuperar el elemento del tope
            string valorEliminado = top.dato;
            
            // Eliminar el nodo del tope
            Nodo temp = top;
            top = top.siguiente;
            // En C# el garbage collector se encarga de liberar la memoria
            tamaño--;
            
            Console.WriteLine($"POP: Elemento '{valorEliminado}' eliminado de la pila.");
            return valorEliminado;
        }
    }

    /**
     * @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK)
     * @return El elemento de la cima, o cadena vacía si la pila está vacía
     */
    public string Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("ERROR: La pila está vacía. No hay elementos para ver (Peek).");
            return ""; // Cadena vacía para indicar error
        }
        else
        {
            return top.dato;
        }
    }

    /**
     * @brief Muestra todos los elementos de la pila
     */
    public void Mostrar()
    {
        if (IsEmpty())
        {
            Console.WriteLine("La pila está vacía.");
        }
        else
        {
            Console.WriteLine("Elementos en la pila (desde el tope):");
            Nodo actual = top;
            int posicion = 1;
            
            while (actual != null)
            {
                Console.WriteLine($"{posicion}. {actual.dato}");
                actual = actual.siguiente;
                posicion++;
            }
        }
    }

    /**
     * @brief Obtiene el tamaño actual de la pila
     * @return Número de elementos en la pila
     */
    public int GetTamaño()
    {
        return tamaño;
    }

    /**
     * @brief Obtiene la capacidad máxima de la pila
     * @return Capacidad máxima
     */
    public int GetCapacidad()
    {
        return capacidad;
    }
}

class Program
{
    /**
     * @brief Función para mostrar el menú de opciones
     */
    static void MostrarMenu()
    {
        Console.WriteLine("\n---- MENU DE OPERACIONES ---");
        Console.WriteLine("1. PUSH (Insertar elemento)");
        Console.WriteLine("2. POP (Eliminar elemento)");
        Console.WriteLine("3. PEEK (Ver elemento superior)");
        Console.WriteLine("4. MOSTRAR (Mostrar todos los elementos)");
        Console.WriteLine("5. ESTADO (Ver estado de la pila)");
        Console.WriteLine("6. SALIR");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("\n");
        Console.Write("Seleccione una opción: ");
    }

    /**
     * @brief Función principal
     */
    static void Main()
    {
        Console.WriteLine("--- Pila con Lista Enlazada Simple ---");
        Console.Write("Ingrese la capacidad de la pila: ");
        int capacidad = int.Parse(Console.ReadLine());
        
        // Crear la pila
        Pila miPila = new Pila(capacidad);
        
        int opcion;
        string valor;
        
        do
        {
            MostrarMenu();
            opcion = int.Parse(Console.ReadLine());
            
            switch (opcion)
            {
                case 1: // PUSH
                    Console.WriteLine("\n");
                    Console.Write("Ingrese el valor a insertar: ");
                    valor = Console.ReadLine();
                    miPila.Push(valor);
                    Console.WriteLine("\n");
                    break;
                    
                case 2: // POP
                    Console.WriteLine("\n");
                    miPila.Pop();
                    break;
                    
                case 3: // PEEK
                    Console.WriteLine("\n");
                    string tope = miPila.Peek();
                    if (!string.IsNullOrEmpty(tope))
                    {
                        Console.WriteLine($"Elemento en el tope: {tope}");
                    }
                    Console.WriteLine("\n");
                    break;
                    
                case 4: // MOSTRAR
                    Console.WriteLine("\n");
                    miPila.Mostrar();
                    break;
                    
                case 5: // ESTADO
                    Console.WriteLine("\n--- ESTADO DE LA PILA ---");
                    Console.WriteLine($"Capacidad máxima: {miPila.GetCapacidad()}");
                    Console.WriteLine($"Elementos actuales: {miPila.GetTamaño()}");
                    Console.WriteLine($"¿Está vacía? {(miPila.IsEmpty() ? "Sí" : "No")}");
                    Console.WriteLine($"¿Está llena? {(miPila.IsFull() ? "Sí" : "No")}");
                    Console.WriteLine("\n");
                    break;
                    
                case 6: // SALIR
                    Console.WriteLine("\n");
                    Console.WriteLine("Saliendo del programa...");
                    break;
                    
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
            
        } while (opcion != 6);
    }
}