#include <iostream>
#include <limits> // Para numeric_limits

using namespace std;

// Definimos el tamaño máximo de la pila.
const int MAX_SIZE = 5;

// Arreglo que almacena los elementos de la pila.
int stack[MAX_SIZE];

// Variable 'top' que indica el índice del elemento superior de la pila.
// Inicializamos 'top' a -1 para indicar que la pila está vacía.
int top = -1;

/**
 * @brief Verifica si la pila está completamente llena.
 * @return true si la pila está llena, false en caso contrario.
 */
bool isFull() {
    // Si 'top' es igual al último índice del arreglo (MAX_SIZE - 1), la pila está llena.
    return top == MAX_SIZE - 1;
}

/**
 * @brief Verifica si la pila está vacía.
 * @return true si la pila está vacía, false en caso contrario.
 */
bool isEmpty() {
    // Si 'top' es igual a -1, la pila está vacía.
    return top == -1;
}

/**
 * @brief Inserta un elemento en la cima de la pila (operación PUSH).
 * @param item El valor a insertar.
 */
void push(int item) {
    if (isFull()) {
        cout << "ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar " << item << "." << endl;
    } else {
        // 1. Incrementamos 'top' para apuntar a la siguiente posición libre.
        top = top + 1;
        // 2. Insertamos el elemento.
        stack[top] = item;
        cout << "PUSH: Elemento " << item << " insertado en la pila." << endl;
    }
}

/**
 * @brief Elimina y devuelve el elemento de la cima de la pila (operación POP).
 * @return El elemento eliminado, o un valor de error si la pila está vacía.
 */
int pop() {
    if (isEmpty()) {
        cout << "ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar." << endl;
        // Devolvemos el valor entero más bajo posible para indicar un error.
        return numeric_limits<int>::min(); 
    } else {
        // 1. Recuperamos el elemento de la cima.
        int popped_item = stack[top];
        // 2. Decrementamos 'top' para "eliminar" el elemento (deja de ser visible).
        top = top - 1;
        cout << "POP: Elemento " << popped_item << " eliminado de la pila." << endl;
        return popped_item;
    }
}

/**
 * @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK).
 * @return El elemento de la cima, o un valor de error si la pila está vacía.
 */
int peek() {
    if (isEmpty()) {
        cout << "ERROR: La pila está vacía. No hay elementos para ver (Peek)." << endl;
        // Devolvemos el valor entero más bajo posible para indicar un error.
        return numeric_limits<int>::min();
    } else {
        // Devolvemos el elemento en la posición 'top' sin modificar 'top'.
        return stack[top];
    }
}

/**
 * @brief Función principal para demostrar el uso de la pila.
 */
int main() {
    cout << "--- Demostración de la Pila (Stack) ---" << endl;
    
    // Inserción de elementos (PUSH)
    push(10); // top = 0
    push(20); // top = 1
    push(30); // top = 2
    
    // Verificamos si está vacía e imprimimos el elemento superior
    cout << "\n" << endl;
    cout << "--- Estado Actual ---" << endl;
    if (isEmpty()) {
        cout << "La pila está vacía." << endl;
    } else {
        cout << "La pila NO está vacía." << endl;
        cout << "Elemento superior (Peek): " << peek() << endl; // Debería ser 30
    }
    cout << "Índice de 'top' actual: " << top << endl; // Debería ser 2

    // Eliminación de elementos (POP)
    cout << "\n" << endl;
    cout << "--- Operaciones POP ---" << endl;
    pop(); // Elimina 30, top = 1
    pop(); // Elimina 20, top = 0
    
    // Verificamos el estado después de las eliminaciones
    cout << "\n" << endl;
    cout << "--- Estado Final ---" << endl;
    cout << "¿Está la pila vacía? (0=No, 1=Sí): " << isEmpty() << endl; // Debería ser 0 (todavía queda 10)
    
    pop(); // Elimina 10, top = -1
    
    cout << "¿Está la pila vacía? (0=No, 1=Sí): " << isEmpty() << endl; // Debería ser 1
    pop(); // Intento de pop en pila vacía (Underflow)

    return 0;
}