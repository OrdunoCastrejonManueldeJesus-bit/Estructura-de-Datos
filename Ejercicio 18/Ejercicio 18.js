// Definimos el tamaño máximo de la pila.
const MAX_SIZE = 5;

// Arreglo que almacena los elementos de la pila.
let stack = new Array(MAX_SIZE);

// Variable 'top' que indica el índice del elemento superior de la pila.
// Inicializamos 'top' a -1 para indicar que la pila está vacía.
let top = -1;

/**
 * @brief Verifica si la pila está completamente llena.
 * @return true si la pila está llena, false en caso contrario.
 */
function isFull() {
    // Si 'top' es igual al último índice del arreglo (MAX_SIZE - 1), la pila está llena.
    return top === MAX_SIZE - 1;
}

/**
 * @brief Verifica si la pila está vacía.
 * @return true si la pila está vacía, false en caso contrario.
 */
function isEmpty() {
    // Si 'top' es igual a -1, la pila está vacía.
    return top === -1;
}

/**
 * @brief Inserta un elemento en la cima de la pila (operación PUSH).
 * @param item El valor a insertar.
 */
function push(item) {
    if (isFull()) {
        console.log(`ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar ${item}.`);
    } else {
        // 1. Incrementamos 'top' para apuntar a la siguiente posición libre.
        top = top + 1;
        // 2. Insertamos el elemento.
        stack[top] = item;
        console.log(`PUSH: Elemento ${item} insertado en la pila.`);
    }
}

/**
 * @brief Elimina y devuelve el elemento de la cima de la pila (operación POP).
 * @return El elemento eliminado, o un valor de error si la pila está vacía.
 */
function pop() {
    if (isEmpty()) {
        console.log("ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar.");
        // Devolvemos el valor entero más bajo posible para indicar un error.
        return Number.MIN_SAFE_INTEGER;
    } else {
        // 1. Recuperamos el elemento de la cima.
        const popped_item = stack[top];
        // 2. Decrementamos 'top' para "eliminar" el elemento (deja de ser visible).
        top = top - 1;
        console.log(`POP: Elemento ${popped_item} eliminado de la pila.`);
        return popped_item;
    }
}

/**
 * @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK).
 * @return El elemento de la cima, o un valor de error si la pila está vacía.
 */
function peek() {
    if (isEmpty()) {
        console.log("ERROR: La pila está vacía. No hay elementos para ver (Peek).");
        // Devolvemos el valor entero más bajo posible para indicar un error.
        return Number.MIN_SAFE_INTEGER;
    } else {
        // Devolvemos el elemento en la posición 'top' sin modificar 'top'.
        return stack[top];
    }
}

/**
 * @brief Función principal para demostrar el uso de la pila.
 */
function main() {
    console.log("--- Demostración de la Pila (Stack) ---");
    
    // Inserción de elementos (PUSH)
    push(10); // top = 0
    push(20); // top = 1
    push(30); // top = 2
    
    // Verificamos si está vacía e imprimimos el elemento superior
    console.log("\n");
    console.log("--- Estado Actual ---");
    if (isEmpty()) {
        console.log("La pila está vacía.");
    } else {
        console.log("La pila NO está vacía.");
        console.log(`Elemento superior (Peek): ${peek()}`); // Debería ser 30
    }
    console.log(`Índice de 'top' actual: ${top}`); // Debería ser 2

    // Eliminación de elementos (POP)
    console.log("\n");
    console.log("--- Operaciones POP ---");
    pop(); // Elimina 30, top = 1
    pop(); // Elimina 20, top = 0
    
    // Verificamos el estado después de las eliminaciones
    console.log("\n");
    console.log("--- Estado Final ---");
    console.log(`¿Está la pila vacía? (False=No, True=Sí): ${isEmpty()}`); // Debería ser False (todavía queda 10)
    
    pop(); // Elimina 10, top = -1
    
    console.log(`¿Está la pila vacía? (False=No, True=Sí): ${isEmpty()}`); // Debería ser True
    pop(); // Intento de pop en pila vacía (Underflow)
}

// Ejecutar la función principal
main();