using System;

class Program
{
    // Definimos el tamaño máximo de la pila.
    const int MAX_SIZE = 5;

    // Arreglo que almacena los elementos de la pila.
    static int[] stack = new int[MAX_SIZE];

    // Variable 'top' que indica el índice del elemento superior de la pila.
    // Inicializamos 'top' a -1 para indicar que la pila está vacía.
    static int top = -1;

    /**
     * @brief Verifica si la pila está completamente llena.
     * @return true si la pila está llena, false en caso contrario.
     */
    static bool IsFull()
    {
        // Si 'top' es igual al último índice del arreglo (MAX_SIZE - 1), la pila está llena.
        return top == MAX_SIZE - 1;
    }

    /**
     * @brief Verifica si la pila está vacía.
     * @return true si la pila está vacía, false en caso contrario.
     */
    static bool IsEmpty()
    {
        // Si 'top' es igual a -1, la pila está vacía.
        return top == -1;
    }

    /**
     * @brief Inserta un elemento en la cima de la pila (operación PUSH).
     * @param item El valor a insertar.
     */
    static void Push(int item)
    {
        if (IsFull())
        {
            Console.WriteLine($"ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar {item}.");
        }
        else
        {
            // 1. Incrementamos 'top' para apuntar a la siguiente posición libre.
            top = top + 1;
            // 2. Insertamos el elemento.
            stack[top] = item;
            Console.WriteLine($"PUSH: Elemento {item} insertado en la pila.");
        }
    }

    /**
     * @brief Elimina y devuelve el elemento de la cima de la pila (operación POP).
     * @return El elemento eliminado, o un valor de error si la pila está vacía.
     */
    static int Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar.");
            // Devolvemos el valor entero más bajo posible para indicar un error.
            return int.MinValue;
        }
        else
        {
            // 1. Recuperamos el elemento de la cima.
            int popped_item = stack[top];
            // 2. Decrementamos 'top' para "eliminar" el elemento (deja de ser visible).
            top = top - 1;
            Console.WriteLine($"POP: Elemento {popped_item} eliminado de la pila.");
            return popped_item;
        }
    }

    /**
     * @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK).
     * @return El elemento de la cima, o un valor de error si la pila está vacía.
     */
    static int Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("ERROR: La pila está vacía. No hay elementos para ver (Peek).");
            // Devolvemos el valor entero más bajo posible para indicar un error.
            return int.MinValue;
        }
        else
        {
            // Devolvemos el elemento en la posición 'top' sin modificar 'top'.
            return stack[top];
        }
    }

    /**
     * @brief Función principal para demostrar el uso de la pila.
     */
    static void Main()
    {
        Console.WriteLine("--- Demostración de la Pila (Stack) ---");
        
        // Inserción de elementos (PUSH)
        Push(10); // top = 0
        Push(20); // top = 1
        Push(30); // top = 2
        
        // Verificamos si está vacía e imprimimos el elemento superior
        Console.WriteLine("\n");
        Console.WriteLine("--- Estado Actual ---");
        if (IsEmpty())
        {
            Console.WriteLine("La pila está vacía.");
        }
        else
        {
            Console.WriteLine("La pila NO está vacía.");
            Console.WriteLine($"Elemento superior (Peek): {Peek()}"); // Debería ser 30
        }
        Console.WriteLine($"Índice de 'top' actual: {top}"); // Debería ser 2

        // Eliminación de elementos (POP)
        Console.WriteLine("\n");
        Console.WriteLine("--- Operaciones POP ---");
        Pop(); // Elimina 30, top = 1
        Pop(); // Elimina 20, top = 0
        
        // Verificamos el estado después de las eliminaciones
        Console.WriteLine("\n");
        Console.WriteLine("--- Estado Final ---");
        Console.WriteLine($"¿Está la pila vacía? (False=No, True=Sí): {IsEmpty()}"); // Debería ser False (todavía queda 10)
        
        Pop(); // Elimina 10, top = -1
        
        Console.WriteLine($"¿Está la pila vacía? (False=No, True=Sí): {IsEmpty()}"); // Debería ser True
        Pop(); // Intento de pop en pila vacía (Underflow)

        Console.ReadKey();
    }
}