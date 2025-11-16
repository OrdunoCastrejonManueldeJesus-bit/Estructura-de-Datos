#include <iostream>
#include <cstdlib>
#include <ctime>

using namespace std;

// Estructura del nodo
struct Nodo {
    int dato;
    Nodo* siguiente;
    Nodo* anterior;
    
    Nodo(int valor) {
        dato = valor;
        siguiente = nullptr;
        anterior = nullptr;
    }
};

// Clase de Lista Doblemente Enlazada
class ListaDoble {
private:
    Nodo* cabeza;
    Nodo* cola;
    
public:
    // Constructor
    ListaDoble() {
        cabeza = nullptr;
        cola = nullptr;
    }
    
    // Método para insertar al final
    void insertarFinal(int valor) {
        Nodo* nuevoNodo = new Nodo(valor);
        
        if (cabeza == nullptr) {
            // Lista vacía
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        } else {
            // Insertar al final
            cola->siguiente = nuevoNodo;
            nuevoNodo->anterior = cola;
            cola = nuevoNodo;
        }
    }
    
    // Método para mostrar la lista de inicio a fin
    void mostrarAdelante() {
        if (cabeza == nullptr) {
            cout << "La lista está vacía." << endl;
            return;
        }
        
        Nodo* actual = cabeza;
        cout << "Lista (adelante): ";
        while (actual != nullptr) {
            cout << actual->dato << " ";
            actual = actual->siguiente;
        }
        cout << endl;
    }
    
    // Método para mostrar la lista de fin a inicio
    void mostrarAtras() {
        if (cola == nullptr) {
            cout << "La lista está vacía." << endl;
            return;
        }
        
        Nodo* actual = cola;
        cout << "Lista (atrás): ";
        while (actual != nullptr) {
            cout << actual->dato << " ";
            actual = actual->anterior;
        }
        cout << endl;
    }
    
    // Método para generar lista con valores aleatorios
    void generarListaAleatoria(int cantidad) {
        // Semilla para números aleatorios
        srand(time(0));
        
        for (int i = 0; i < cantidad; i++) {
            int valor = rand() % 100 + 1; // Números entre 1 y 100
            insertarFinal(valor);
        }
    }
    
    // Destructor para liberar memoria
    ~ListaDoble() {
        Nodo* actual = cabeza;
        while (actual != nullptr) {
            Nodo* temp = actual;
            actual = actual->siguiente;
            delete temp;
        }
    }
};

// Función principal
int main() {
    ListaDoble lista;
    
    cout << "=== LISTA DOBLEMENTE ENLAZADA CON VALORES ALEATORIOS ===" << endl;
    
    // Generar lista con 15 valores aleatorios
    lista.generarListaAleatoria(15);
    
    // Mostrar la lista en ambas direcciones
    lista.mostrarAdelante();
    lista.mostrarAtras();
    
    return 0;
}