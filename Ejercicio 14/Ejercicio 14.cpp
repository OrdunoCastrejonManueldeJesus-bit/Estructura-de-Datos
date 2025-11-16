#include <iostream>
#include <cstdlib>
#include <ctime>

using namespace std;
//  Listas Enlazadas
struct Nodo {
    int valor;
    Nodo* siguiente;
};

int main() {
    srand(time(0));
    
    Nodo* cabeza = nullptr;
    Nodo* actual = nullptr;
    
    cout << "Generando 15 valores aleatorios..." << endl;
    
    // Crear 15 nodos con valores aleatorios
    for (int i = 0; i < 15; i++) {
        Nodo* nuevoNodo = new Nodo;
        nuevoNodo->valor = 1 + rand() % 100;
        nuevoNodo->siguiente = nullptr;
        
        if (cabeza == nullptr) {
            cabeza = nuevoNodo;
            actual = cabeza;
        } else {
            actual->siguiente = nuevoNodo;
            actual = nuevoNodo;
        }
    }
    
    // Mostrar la lista
    cout << "Lista Enlazada:" << endl;
    actual = cabeza;
    int contador = 1;
    while (actual != nullptr) {
        cout << "Nodo " << contador << ": " << actual->valor << endl;
        actual = actual->siguiente;
        contador++;
    }
    
    // Liberar memoria
    actual = cabeza;
    while (actual != nullptr) {
        Nodo* temp = actual;
        actual = actual->siguiente;
        delete temp;
    }
    
    return 0;
}