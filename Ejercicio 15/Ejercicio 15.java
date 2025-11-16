import java.util.Random;

// Clase del nodo
class Nodo {
    public int valor;
    public Nodo siguiente;
    
    public Nodo(int val) {
        valor = val;
        siguiente = null;
    }
}

// Clase Lista Circular
class ListaCircular {
    private Nodo cabeza;
    private Nodo cola;
    
    // Constructor
    public ListaCircular() {
        cabeza = null;
        cola = null;
    }
    
    // En Java no hay destructores explícitos como en C#,
    // el garbage collector se encarga automáticamente
    
    // Insertar al final de la lista
    public void insertar(int valor) {
        Nodo nuevoNodo = new Nodo(valor);
        
        if (cabeza == null) {
            // Lista vacía
            cabeza = nuevoNodo;
            cola = nuevoNodo;
            nuevoNodo.siguiente = cabeza;
        } else {
            // Insertar al final
            cola.siguiente = nuevoNodo;
            cola = nuevoNodo;
            cola.siguiente = cabeza;
        }
    }
    
    // Mostrar la lista circular
    public void mostrar() {
        if (cabeza == null) {
            System.out.println("Lista vacía");
            return;
        }
        
        Nodo actual = cabeza;
        int contador = 0;
        
        System.out.print("Lista Circular: ");
        do {
            System.out.print(actual.valor);
            actual = actual.siguiente;
            contador++;
            
            if (actual != cabeza) {
                System.out.print(" -> ");
            }
            
            // Salto de línea cada 5 elementos para mejor visualización
            if (contador % 5 == 0 && actual != cabeza) {
                System.out.println();
                System.out.print("               ");
            }
        } while (actual != cabeza);
        System.out.println(" -> (vuelve al inicio)");
    }
    
    // Verificar si la lista está vacía
    public boolean estaVacia() {
        return cabeza == null;
    }
    
    // Obtener tamaño de la lista
    public int obtenerTamano() {
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

// Clase principal
public class Main {
    // Función para generar número aleatorio entre 1 y 100
    private static int generarAleatorio() {
        Random rand = new Random();
        return rand.nextInt(100) + 1;
    }
    
    public static void main(String[] args) {
        // Crear lista circular
        ListaCircular lista = new ListaCircular();
        
        System.out.println("=== LISTA ENLAZADA CIRCULAR CON 15 VALORES ALEATORIOS ===");
        System.out.println("Generando valores aleatorios");
        System.out.println();
        
        // Insertar 15 valores aleatorios
        for (int i = 0; i < 15; i++) {
            int valor = generarAleatorio();
            lista.insertar(valor);
            System.out.println("Insertado: " + valor);
        }
        
        System.out.println();
        
        // Mostrar la lista completa
        lista.mostrar();
    }
}