import java.util.Random;

// Clase del nodo
class Nodo {
    public int valor;
    public Nodo siguiente;
    public Nodo anterior;
    
    public Nodo(int v) {
        valor = v;
        siguiente = null;
        anterior = null;
    }
}

// Clase de Lista Circular Doble
class ListaCircularDoble {
    private Nodo cabeza;
    private int tamaño;
    
    // Constructor
    public ListaCircularDoble() {
        cabeza = null;
        tamaño = 0;
    }
    
    // Insertar al final
    public void insertarFinal(int valor) {
        Nodo nuevoNodo = new Nodo(valor);
        
        if (cabeza == null) {
            cabeza = nuevoNodo;
            cabeza.siguiente = cabeza;
            cabeza.anterior = cabeza;
        } else {
            Nodo ultimo = cabeza.anterior;
            
            ultimo.siguiente = nuevoNodo;
            nuevoNodo.anterior = ultimo;
            nuevoNodo.siguiente = cabeza;
            cabeza.anterior = nuevoNodo;
        }
        
        tamaño++;
    }
    
    // Mostrar lista en sentido horario
    public void mostrarHorario() {
        if (cabeza == null) {
            System.out.println("Lista vacía");
            return;
        }
        
        System.out.print("Lista en sentido horario: ");
        Nodo actual = cabeza;
        
        do {
            System.out.print(actual.valor + " ");
            actual = actual.siguiente;
        } while (actual != cabeza);
        
        System.out.println();
    }
    
    // Mostrar lista en sentido antihorario
    public void mostrarAntihorario() {
        if (cabeza == null) {
            System.out.println("Lista vacía");
            return;
        }
        
        System.out.print("Lista en sentido antihorario: ");
        Nodo actual = cabeza.anterior;
        
        do {
            System.out.print(actual.valor + " ");
            actual = actual.anterior;
        } while (actual != cabeza.anterior);
        
        System.out.println();
    }
    
    // Obtener tamaño
    public int getTamaño() {
        return tamaño;
    }
    
    // Generar lista con valores aleatorios
    public void generarListaAleatoria(int cantidad) {
        Random rand = new Random();
        
        for (int i = 0; i < cantidad; i++) {
            int valor = rand.nextInt(100) + 1; // Valores entre 1 y 100
            insertarFinal(valor);
        }
    }
}

// Clase principal
public class Main {
    public static void main(String[] args) {
        ListaCircularDoble lista = new ListaCircularDoble();
        
        System.out.println("=== LISTA ENLAZADA CIRCULAR DOBLE ===");
        System.out.println("Generando 15 valores aleatorios");
        System.out.println();
        
        // Generar lista con 15 valores aleatorios
        lista.generarListaAleatoria(15);
        
        // Mostrar resultados
        System.out.println("Tamaño de la lista: " + lista.getTamaño());
        
        lista.mostrarHorario();
        lista.mostrarAntihorario();
    }
}