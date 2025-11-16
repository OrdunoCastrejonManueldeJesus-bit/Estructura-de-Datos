import java.util.Random;

// Clase del nodo
class Nodo {
    public int dato;
    public Nodo siguiente;
    public Nodo anterior;
    
    public Nodo(int valor) {
        dato = valor;
        siguiente = null;
        anterior = null;
    }
}

// Clase de Lista Doblemente Enlazada
class ListaDoble {
    private Nodo cabeza;
    private Nodo cola;
    
    // Constructor
    public ListaDoble() {
        cabeza = null;
        cola = null;
    }
    
    // Método para insertar al final
    public void insertarFinal(int valor) {
        Nodo nuevoNodo = new Nodo(valor);
        
        if (cabeza == null) {
            // Lista vacía
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        } else {
            // Insertar al final
            cola.siguiente = nuevoNodo;
            nuevoNodo.anterior = cola;
            cola = nuevoNodo;
        }
    }
    
    // Método para mostrar la lista de inicio a fin
    public void mostrarAdelante() {
        if (cabeza == null) {
            System.out.println("La lista está vacía.");
            return;
        }
        
        Nodo actual = cabeza;
        System.out.print("Lista (adelante): ");
        while (actual != null) {
            System.out.print(actual.dato + " ");
            actual = actual.siguiente;
        }
        System.out.println();
    }
    
    // Método para mostrar la lista de fin a inicio
    public void mostrarAtras() {
        if (cola == null) {
            System.out.println("La lista está vacía.");
            return;
        }
        
        Nodo actual = cola;
        System.out.print("Lista (atrás): ");
        while (actual != null) {
            System.out.print(actual.dato + " ");
            actual = actual.anterior;
        }
        System.out.println();
    }
    
    // Método para generar lista con valores aleatorios
    public void generarListaAleatoria(int cantidad) {
        Random random = new Random();
        
        for (int i = 0; i < cantidad; i++) {
            int valor = random.nextInt(100) + 1; // Números entre 1 y 100
            insertarFinal(valor);
        }
    }
}

// Clase principal
public class Main {
    public static void main(String[] args) {
        ListaDoble lista = new ListaDoble();
        
        System.out.println("=== LISTA DOBLEMENTE ENLAZADA CON VALORES ALEATORIOS ===");
        
        // Generar lista con 15 valores aleatorios
        lista.generarListaAleatoria(15);
        
        // Mostrar la lista en ambas direcciones
        lista.mostrarAdelante();
        lista.mostrarAtras();
    }
}