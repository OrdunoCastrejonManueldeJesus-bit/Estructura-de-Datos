#include <iostream>
#include <limits> // Para numeric_limits
#include <string>

using namespace std;

// Estructura del nodo para la lista enlazada
struct Nodo {
    string dato;
    Nodo* siguiente;
    
    // Constructor
    Nodo(string valor) {
        dato = valor;
        siguiente = nullptr;
    }
};

// Clase Pila implementada con lista enlazada
class Pila {
private:
    Nodo* top;      // Puntero al tope de la pila
    int capacidad;  // Capacidad máxima de la pila
    int tamaño;     // Tamaño actual de la pila

public:
    /**
     * @brief Constructor de la pila
     * @param cap Capacidad máxima de la pila
     */
    Pila(int cap) {
        top = nullptr;
        capacidad = cap;
        tamaño = 0;
        cout << "\n" << endl;
        cout << "Pila creada con capacidad: " << capacidad << endl;
    }

    /**
     * @brief Destructor para liberar memoria
     */
    ~Pila() {
        while (!isEmpty()) {
            pop();
        }
    }

    /**
     * @brief Verifica si la pila está completamente llena
     * @return true si la pila está llena, false en caso contrario
     */
    bool isFull() {
        return tamaño == capacidad;
    }

    /**
     * @brief Verifica si la pila está vacía
     * @return true si la pila está vacía, false en caso contrario
     */
    bool isEmpty() {
        return top == nullptr;
    }

    /**
     * @brief Inserta un elemento en la cima de la pila (operación PUSH)
     * @param valor El valor a insertar
     */
    void push(string valor) {
        if (isFull()) {
            cout << "ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar '" << valor << "'." << endl;
        } else {
            // Crear nuevo nodo
            Nodo* nuevoNodo = new Nodo(valor);
            
            // Insertar al inicio (tope de la pila)
            nuevoNodo->siguiente = top;
            top = nuevoNodo;
            tamaño++;
            
            cout << "PUSH: Elemento '" << valor << "' insertado en la pila." << endl;
        }
    }

    /**
     * @brief Elimina y devuelve el elemento de la cima de la pila (operación POP)
     * @return El elemento eliminado, o cadena vacía si la pila está vacía
     */
    string pop() {
        if (isEmpty()) {
            cout << "ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar." << endl;
            return ""; // Cadena vacía para indicar error
        } else {
            // Recuperar el elemento del tope
            string valorEliminado = top->dato;
            
            // Eliminar el nodo del tope
            Nodo* temp = top;
            top = top->siguiente;
            delete temp;
            tamaño--;
            
            cout << "POP: Elemento '" << valorEliminado << "' eliminado de la pila." << endl;
            return valorEliminado;
        }
    }

    /**
     * @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK)
     * @return El elemento de la cima, o cadena vacía si la pila está vacía
     */
    string peek() {
        if (isEmpty()) {
            cout << "ERROR: La pila está vacía. No hay elementos para ver (Peek)." << endl;
            return ""; // Cadena vacía para indicar error
        } else {
            return top->dato;
        }
    }

    /**
     * @brief Muestra todos los elementos de la pila
     */
    void mostrar() {
        if (isEmpty()) {
            cout << "La pila está vacía." << endl;
        } else {
            cout << "Elementos en la pila (desde el tope):" << endl;
            Nodo* actual = top;
            int posicion = 1;
            
            while (actual != nullptr) {
                cout << posicion << ". " << actual->dato << endl;
                actual = actual->siguiente;
                posicion++;
            }
        }
    }

    /**
     * @brief Obtiene el tamaño actual de la pila
     * @return Número de elementos en la pila
     */
    int getTamaño() {
        return tamaño;
    }

    /**
     * @brief Obtiene la capacidad máxima de la pila
     * @return Capacidad máxima
     */
    int getCapacidad() {
        return capacidad;
    }
};

/**
 * @brief Función para mostrar el menú de opciones
 */
void mostrarMenu() {
    cout << "\n---- MENU DE OPERACIONES ---" << endl;
    cout << "1. PUSH (Insertar elemento)" << endl;
    cout << "2. POP (Eliminar elemento)" << endl;
    cout << "3. PEEK (Ver elemento superior)" << endl;
    cout << "4. MOSTRAR (Mostrar todos los elementos)" << endl;
    cout << "5. ESTADO (Ver estado de la pila)" << endl;
    cout << "6. SALIR" << endl;
    cout << "-----------------------------" << endl;
    cout << "\n" << endl;
    cout << "Seleccione una opción: ";
}

/**
 * @brief Función principal
 */
int main() {
    int capacidad;
    cout << "--- Pila con Lista Enlazada Simple ---" << endl;
    cout << "Ingrese la capacidad de la pila: ";
    cin >> capacidad;
    
    
    // Crear la pila
    Pila miPila(capacidad);
    
    int opcion;
    string valor;
    
    do {
        mostrarMenu();
        cin >> opcion;
        
        switch (opcion) {
            case 1: // PUSH
                cout << "\n" << endl;
                cout << "Ingrese el valor a insertar: ";
                cin.ignore(); // Limpiar buffer
                getline(cin, valor);
                miPila.push(valor);
                cout << "\n" << endl;
                break;
                
            case 2: // POP
                cout << "\n" << endl;
                miPila.pop();
                break;
                
            case 3: // PEEK
                {
                    cout << "\n" << endl;
                    string tope = miPila.peek();
                    if (!tope.empty()) {
                        cout << "Elemento en el tope: " << tope << endl;
                    }
                }
                cout << "\n" << endl;
                break;
                
            case 4: // MOSTRAR
                cout << "\n" << endl;
                miPila.mostrar();
                break;
                
            case 5: // ESTADO
                cout << "\n--- ESTADO DE LA PILA ---" << endl;
                cout << "Capacidad máxima: " << miPila.getCapacidad() << endl;
                cout << "Elementos actuales: " << miPila.getTamaño() << endl;
                cout << "¿Está vacía? " << (miPila.isEmpty() ? "Sí" : "No") << endl;
                cout << "¿Está llena? " << (miPila.isFull() ? "Sí" : "No") << endl;
                cout << "\n" << endl;
                break;
                
            case 6: // SALIR
                cout << "\n" << endl;
                cout << "Saliendo del programa..." << endl;
                break;
                
            default:
                cout << "Opción no válida. Intente nuevamente." << endl;
        }
        
    } while (opcion != 6);
    
    return 0;
}