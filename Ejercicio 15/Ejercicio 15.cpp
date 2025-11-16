#include <iostream>
#include <cstdlib>
#include <ctime>

using namespace std;

// Estructura del nodo
struct Nodo {
    int valor;
    Nodo* siguiente;
    
    Nodo(int val) : valor(val), siguiente(nullptr) {}
};

// Clase Lista Circular
class ListaCircular {
private:
    Nodo* cabeza;
    Nodo* cola;
    
public:
    // Constructor
    ListaCircular() : cabeza(nullptr), cola(nullptr) {}
    
    // Destructor
    ~ListaCircular() {
        if (cabeza == nullptr) return;
        
        Nodo* actual = cabeza;
        Nodo* temp;
        
        do {
            temp = actual;
            actual = actual->siguiente;
            delete temp;
        } while (actual != cabeza);
    }
    
    // Insertar al final de la lista
    void insertar(int valor) {
        Nodo* nuevoNodo = new Nodo(valor);
        
        if (cabeza == nullptr) {
            // Lista vacía
            cabeza = nuevoNodo;
            cola = nuevoNodo;
            nuevoNodo->siguiente = cabeza;
        } else {
            // Insertar al final
            cola->siguiente = nuevoNodo;
            cola = nuevoNodo;
            cola->siguiente = cabeza;
        }
    }
    
    // Mostrar la lista circular
    void mostrar() {
        if (cabeza == nullptr) {
            cout << "Lista vacía" << endl;
            return;
        }
        
        Nodo* actual = cabeza;
        int contador = 0;
        
        cout << "Lista Circular: ";
        do {
            cout << actual->valor;
            actual = actual->siguiente;
            contador++;
            
            if (actual != cabeza) {
                cout << " -> ";
            }
            
            // Salto de línea cada 5 elementos para mejor visualización
            if (contador % 5 == 0 && actual != cabeza) {
                cout << endl << "               ";
            }
        } while (actual != cabeza);
        cout << " -> (vuelve al inicio)" << endl;
    }
    
    // Verificar si la lista está vacía
    bool estaVacia() {
        return cabeza == nullptr;
    }
    
    // Obtener tamaño de la lista
    int obtenerTamano() {
        if (cabeza == nullptr) return 0;
        
        int contador = 0;
        Nodo* actual = cabeza;
        
        do {
            contador++;
            actual = actual->siguiente;
        } while (actual != cabeza);
        
        return contador;
    }
};

// Función para generar número aleatorio entre 1 y 100
int generarAleatorio() {
    return rand() % 100 + 1;
}

int main() {
    // Semilla para números aleatorios
    srand(time(0));
    
    // Crear lista circular
    ListaCircular lista;
    
    cout << "=== LISTA ENLAZADA CIRCULAR CON 15 VALORES ALEATORIOS ===" << endl;
    cout << "Generando valores aleatorios..." << endl << endl;
    
    // Insertar 15 valores aleatorios
    for (int i = 0; i < 15; i++) {
        int valor = generarAleatorio();
        lista.insertar(valor);
        cout << "Insertado: " << valor << endl;
    }
    
    cout << endl;
    
    // Mostrar la lista completa
    lista.mostrar();
    
    return 0;
}