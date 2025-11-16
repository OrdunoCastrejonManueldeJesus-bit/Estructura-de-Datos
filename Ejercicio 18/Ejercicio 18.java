public class StackExample {
    // Definimos el tamaño máximo de la pila.
    private static final int MAX_SIZE = 5;

    // Arreglo que almacena los elementos de la pila.
    private static int[] stack = new int[MAX_SIZE];

    // Variable 'top' que indica el índice del elemento superior de la pila.
    // Inicializamos 'top' a -1 para indicar que la pila está vacía.
    private static int top = -1;

    /**
     * @brief Verifica si la pila está completamente llena.
     * @return true si la pila está llena, false en caso contrario.
     */
    private static boolean isFull() {
        // Si 'top' es igual al último índice del arreglo (MAX_SIZE - 1), la pila está llena.
        return top == MAX_SIZE - 1;
    }

    /**
     * @brief Verifica si la pila está vacía.
     * @return true si la pila está vacía, false en caso contrario.
     */
    private static boolean isEmpty() {
        // Si 'top' es igual a -1, la pila está vacía.
        return top == -1;
    }

    /**
     * @brief Inserta un elemento en la cima de la pila (operación PUSH).
     * @param item El valor a insertar.
     */
    private static void push(int item) {
        if (isFull()) {
            System.out.println("ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar " + item + ".");
        } else {
            // 1. Incrementamos 'top' para apuntar a la siguiente posición libre.
            top = top + 1;
            // 2. Insertamos el elemento.
            stack[top] = item;
            System.out.println("PUSH: Elemento " + item + " insertado en la pila.");
        }
    }

    /**
     * @brief Elimina y devuelve el elemento de la cima de la pila (operación POP).
     * @return El elemento eliminado, o un valor de error si la pila está vacía.
     */
    private static int pop() {
        if (isEmpty()) {
            System.out.println("ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar.");
            // Devolvemos el valor entero más bajo posible para indicar un error.
            return Integer.MIN_VALUE;
        } else {
            // 1. Recuperamos el elemento de la cima.
            int popped_item = stack[top];
            // 2. Decrementamos 'top' para "eliminar" el elemento (deja de ser visible).
            top = top - 1;
            System.out.println("POP: Elemento " + popped_item + " eliminado de la pila.");
            return popped_item;
        }
    }

    /**
     * @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK).
     * @return El elemento de la cima, o un valor de error si la pila está vacía.
     */
    private static int peek() {
        if (isEmpty()) {
            System.out.println("ERROR: La pila está vacía. No hay elementos para ver (Peek).");
            // Devolvemos el valor entero más bajo posible para indicar un error.
            return Integer.MIN_VALUE;
        } else {
            // Devolvemos el elemento en la posición 'top' sin modificar 'top'.
            return stack[top];
        }
    }

    /**
     * @brief Función principal para demostrar el uso de la pila.
     */
    public static void main(String[] args) {
        System.out.println("--- Demostración de la Pila (Stack) ---");
        
        // Inserción de elementos (PUSH)
        push(10); // top = 0
        push(20); // top = 1
        push(30); // top = 2
        
        // Verificamos si está vacía e imprimimos el elemento superior
        System.out.println("\n");
        System.out.println("--- Estado Actual ---");
        if (isEmpty()) {
            System.out.println("La pila está vacía.");
        } else {
            System.out.println("La pila NO está vacía.");
            System.out.println("Elemento superior (Peek): " + peek()); // Debería ser 30
        }
        System.out.println("Índice de 'top' actual: " + top); // Debería ser 2

        // Eliminación de elementos (POP)
        System.out.println("\n");
        System.out.println("--- Operaciones POP ---");
        pop(); // Elimina 30, top = 1
        pop(); // Elimina 20, top = 0
        
        // Verificamos el estado después de las eliminaciones
        System.out.println("\n");
        System.out.println("--- Estado Final ---");
        System.out.println("¿Está la pila vacía? (False=No, True=Sí): " + isEmpty()); // Debería ser False (todavía queda 10)
        
        pop(); // Elimina 10, top = -1
        
        System.out.println("¿Está la pila vacía? (False=No, True=Sí): " + isEmpty()); // Debería ser True
        pop(); // Intento de pop en pila vacía (Underflow)
    }
}