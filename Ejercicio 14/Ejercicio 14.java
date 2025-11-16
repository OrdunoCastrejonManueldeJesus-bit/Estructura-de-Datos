import java.util.Random;

public class Main {
    
    //Listas enlazadas
    static class Nodo {
        public int valor;
        public Nodo siguiente;
    }

    public static void main(String[] args) {
        Random rand = new Random();
        
        Nodo cabeza = null;
        Nodo actual = null;
        
        System.out.println("Generando 15 valores aleatorios");
        System.out.println(" ");
        
        // Crear 15 nodos con valores aleatorios
        for (int i = 0; i < 15; i++) {
            Nodo nuevoNodo = new Nodo();
            nuevoNodo.valor = 1 + rand.nextInt(100); // 1-100
            nuevoNodo.siguiente = null;
            
            if (cabeza == null) {
                cabeza = nuevoNodo;
                actual = cabeza;
            } else {
                actual.siguiente = nuevoNodo;
                actual = nuevoNodo;
            }
        }
        
        // Mostrar la lista
        System.out.println("Lista Enlazada:");
        actual = cabeza;
        int contador = 1;
        while (actual != null) {
            System.out.println("Nodo " + contador + ": " + actual.valor);
            actual = actual.siguiente;
            contador++;
        }
    }
}