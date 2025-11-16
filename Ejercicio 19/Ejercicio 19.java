import java.util.Scanner;

// Clase Nodo para la lista enlazada
class Nodo {
    public String dato;
    public Nodo siguiente;
    
    // Constructor
    public Nodo(String valor) {
        dato = valor;
        siguiente = null;
    }
}

// Clase Pila implementada con lista enlazada
class Pila {
    private Nodo top;      // Referencia al tope de la pila
    private int capacidad;  // Capacidad máxima de la pila
    private int tamaño;     // Tamaño actual de la pila

    /**
     * @brief Constructor de la pila
     * @param cap Capacidad máxima de la pila
     */
    public Pila(int cap) {
        top = null;
        capacidad = cap;
        tamaño = 0;
        System.out.println("\n");
        System.out.println("Pila creada con capacidad: " + capacidad);
    }

    /**
     * @brief Verifica si la pila está completamente llena
     * @return true si la pila está llena, false en caso contrario
     */
    public boolean isFull() {
        return tamaño == capacidad;
    }

    /**
     * @brief Verifica si la pila está vacía
     * @return true si la pila está vacía, false en caso contrario
     */
    public boolean isEmpty() {
        return top == null;
    }

    /**
     * @brief Inserta un elemento en la cima de la pila (operación PUSH)
     * @param valor El valor a insertar
     */
    public void push(String valor) {
        if (isFull()) {
            System.out.println("ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar '" + valor + "'.");
        } else {
            // Crear nuevo nodo
            Nodo nuevoNodo = new Nodo(valor);
            
            // Insertar al inicio (tope de la pila)
            nuevoNodo.siguiente = top;
            top = nuevoNodo;
            tamaño++;
            
            System.out.println("PUSH: Elemento '" + valor + "' insertado en la pila.");
        }
    }

    /**
     * @brief Elimina y devuelve el elemento de la cima de la pila (operación POP)
     * @return El elemento eliminado, o cadena vacía si la pila está vacía
     */
    public String pop() {
        if (isEmpty()) {
            System.out.println("ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar.");
            return ""; // Cadena vacía para indicar error
        } else {
            // Recuperar el elemento del tope
            String valorEliminado = top.dato;
            
            // Eliminar el nodo del tope
            Nodo temp = top;
            top = top.siguiente;
            // En Java el garbage collector se encarga de liberar la memoria
            tamaño--;
            
            System.out.println("POP: Elemento '" + valorEliminado + "' eliminado de la pila.");
            return valorEliminado;
        }
    }

    /**
     * @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK)
     * @return El elemento de la cima, o cadena vacía si la pila está vacía
     */
    public String peek() {
        if (isEmpty()) {
            System.out.println("ERROR: La pila está vacía. No hay elementos para ver (Peek).");
            return ""; // Cadena vacía para indicar error
        } else {
            return top.dato;
        }
    }

    /**
     * @brief Muestra todos los elementos de la pila
     */
    public void mostrar() {
        if (isEmpty()) {
            System.out.println("La pila está vacía.");
        } else {
            System.out.println("Elementos en la pila (desde el tope):");
            Nodo actual = top;
            int posicion = 1;
            
            while (actual != null) {
                System.out.println(posicion + ". " + actual.dato);
                actual = actual.siguiente;
                posicion++;
            }
        }
    }

    /**
     * @brief Obtiene el tamaño actual de la pila
     * @return Número de elementos en la pila
     */
    public int getTamaño() {
        return tamaño;
    }

    /**
     * @brief Obtiene la capacidad máxima de la pila
     * @return Capacidad máxima
     */
    public int getCapacidad() {
        return capacidad;
    }
}

public class Main {
    private static Scanner scanner = new Scanner(System.in);
    
    /**
     * @brief Función para mostrar el menú de opciones
     */
    static void mostrarMenu() {
        System.out.println("\n---- MENU DE OPERACIONES ---");
        System.out.println("1. PUSH (Insertar elemento)");
        System.out.println("2. POP (Eliminar elemento)");
        System.out.println("3. PEEK (Ver elemento superior)");
        System.out.println("4. MOSTRAR (Mostrar todos los elementos)");
        System.out.println("5. ESTADO (Ver estado de la pila)");
        System.out.println("6. SALIR");
        System.out.println("-----------------------------");
        System.out.println("\n");
        System.out.print("Seleccione una opción: ");
    }

    /**
     * @brief Función principal
     */
    public static void main(String[] args) {
        System.out.println("--- Pila con Lista Enlazada Simple ---");
        System.out.print("Ingrese la capacidad de la pila: ");
        int capacidad = Integer.parseInt(scanner.nextLine());
        
        // Crear la pila
        Pila miPila = new Pila(capacidad);
        
        int opcion;
        String valor;
        
        do {
            mostrarMenu();
            opcion = Integer.parseInt(scanner.nextLine());
            
            switch (opcion) {
                case 1: // PUSH
                    System.out.println("\n");
                    System.out.print("Ingrese el valor a insertar: ");
                    valor = scanner.nextLine();
                    miPila.push(valor);
                    System.out.println("\n");
                    break;
                    
                case 2: // POP
                    System.out.println("\n");
                    miPila.pop();
                    break;
                    
                case 3: // PEEK
                    System.out.println("\n");
                    String tope = miPila.peek();
                    if (tope != null && !tope.isEmpty()) {
                        System.out.println("Elemento en el tope: " + tope);
                    }
                    System.out.println("\n");
                    break;
                    
                case 4: // MOSTRAR
                    System.out.println("\n");
                    miPila.mostrar();
                    break;
                    
                case 5: // ESTADO
                    System.out.println("\n--- ESTADO DE LA PILA ---");
                    System.out.println("Capacidad máxima: " + miPila.getCapacidad());
                    System.out.println("Elementos actuales: " + miPila.getTamaño());
                    System.out.println("¿Está vacía? " + (miPila.isEmpty() ? "Sí" : "No"));
                    System.out.println("¿Está llena? " + (miPila.isFull() ? "Sí" : "No"));
                    System.out.println("\n");
                    break;
                    
                case 6: // SALIR
                    System.out.println("\n");
                    System.out.println("Saliendo del programa...");
                    break;
                    
                default:
                    System.out.println("Opción no válida. Intente nuevamente.");
                    break;
            }
            
        } while (opcion != 6);
        
        scanner.close();
    }
}