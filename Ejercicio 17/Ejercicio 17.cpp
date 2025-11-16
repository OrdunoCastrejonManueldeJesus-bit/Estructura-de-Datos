#include <iostream>
#include <cstdlib>
#include <ctime>

using namespace std;

// Estructura del nodo
struct Nodo {
    int valor;
    Nodo* siguiente;
    Nodo* anterior;
    
    Nodo(int v) : valor(v), siguiente(nullptr), anterior(nullptr) {}
};

// Clase de Lista Circular Doble
class ListaCircularDoble {
private:
    Nodo* cabeza;
    int tamaño;
    
public:
    // Constructor
    ListaCircularDoble() : cabeza(nullptr), tamaño(0) {}
    
    // Destructor
    ~ListaCircularDoble() {
        if (cabeza == nullptr) return;
        
        Nodo* actual = cabeza;
        Nodo* temp;
        
        do {
            temp = actual;
            actual = actual->siguiente;
            delete temp;
        } while (actual != cabeza);
        
        cabeza = nullptr;
        tamaño = 0;
    }
    
    // Insertar al final
    void insertarFinal(int valor) {
        Nodo* nuevoNodo = new Nodo(valor);
        
        if (cabeza == nullptr) {
            cabeza = nuevoNodo;
            cabeza->siguiente = cabeza;
            cabeza->anterior = cabeza;
        } else {
            Nodo* ultimo = cabeza->anterior;
            
            ultimo->siguiente = nuevoNodo;
            nuevoNodo->anterior = ultimo;
            nuevoNodo->siguiente = cabeza;
            cabeza->anterior = nuevoNodo;
        }
        
        tamaño++;
    }
    
    // Mostrar lista en sentido horario
    void mostrarHorario() {
        if (cabeza == nullptr) {
            cout << "Lista vacía" << endl;
            return;
        }
        
        cout << "Lista en sentido horario: ";
        Nodo* actual = cabeza;
        
        do {
            cout << actual->valor << " ";
            actual = actual->siguiente;
        } while (actual != cabeza);
        
        cout << endl;
    }
    
    // Mostrar lista en sentido antihorario
    void mostrarAntihorario() {
        if (cabeza == nullptr) {
            cout << "Lista vacía" << endl;
            return;
        }
        
        cout << "Lista en sentido antihorario: ";
        Nodo* actual = cabeza->anterior;
        
        do {
            cout << actual->valor << " ";
            actual = actual->anterior;
        } while (actual != cabeza->anterior);
        
        cout << endl;
    }
    
    // Obtener tamaño
    int getTamaño() {
        return tamaño;
    }
    
    // Generar lista con valores aleatorios
    void generarListaAleatoria(int cantidad) {
        // Semilla para números aleatorios
        srand(time(0));
        
        for (int i = 0; i < cantidad; i++) {
            int valor = rand() % 100 + 1; // Valores entre 1 y 100
            insertarFinal(valor);
        }
    }
};

// Función principal
int main() {
    ListaCircularDoble lista;
    
    cout << "=== LISTA ENLAZADA CIRCULAR DOBLE ===" << endl;
    cout << "Generando 15 valores aleatorios" << endl << endl;
    
    // Generar lista con 15 valores aleatorios
    lista.generarListaAleatoria(15);
    
    // Mostrar resultados
    cout << "Tamaño de la lista: " << lista.getTamaño() << endl;
    
    lista.mostrarHorario();
    lista.mostrarAntihorario();
    
    return 0;
}