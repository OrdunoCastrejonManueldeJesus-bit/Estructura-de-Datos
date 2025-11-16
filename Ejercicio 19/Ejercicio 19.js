// Clase Nodo para la lista enlazada
class Nodo {
    constructor(valor) {
        this.dato = valor;
        this.siguiente = null;
    }
}

// Clase Pila implementada con lista enlazada
class Pila {
    /**
     * @brief Constructor de la pila
     * @param cap Capacidad máxima de la pila
     */
    constructor(cap) {
        this.top = null;      // Referencia al tope de la pila
        this.capacidad = cap; // Capacidad máxima de la pila
        this.tamaño = 0;      // Tamaño actual de la pila
        
        console.log("\n");
        console.log(`Pila creada con capacidad: ${this.capacidad}`);
    }

    /**
     * @brief Verifica si la pila está completamente llena
     * @return true si la pila está llena, false en caso contrario
     */
    isFull() {
        return this.tamaño === this.capacidad;
    }

    /**
     * @brief Verifica si la pila está vacía
     * @return true si la pila está vacía, false en caso contrario
     */
    isEmpty() {
        return this.top === null;
    }

    /**
     * @brief Inserta un elemento en la cima de la pila (operación PUSH)
     * @param valor El valor a insertar
     */
    push(valor) {
        if (this.isFull()) {
            console.log(`ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar '${valor}'.`);
        } else {
            // Crear nuevo nodo
            const nuevoNodo = new Nodo(valor);
            
            // Insertar al inicio (tope de la pila)
            nuevoNodo.siguiente = this.top;
            this.top = nuevoNodo;
            this.tamaño++;
            
            console.log(`PUSH: Elemento '${valor}' insertado en la pila.`);
        }
    }

    /**
     * @brief Elimina y devuelve el elemento de la cima de la pila (operación POP)
     * @return El elemento eliminado, o cadena vacía si la pila está vacía
     */
    pop() {
        if (this.isEmpty()) {
            console.log("ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar.");
            return ""; // Cadena vacía para indicar error
        } else {
            // Recuperar el elemento del tope
            const valorEliminado = this.top.dato;
            
            // Eliminar el nodo del tope
            const temp = this.top;
            this.top = this.top.siguiente;
            // En JavaScript el garbage collector se encarga de liberar la memoria
            this.tamaño--;
            
            console.log(`POP: Elemento '${valorEliminado}' eliminado de la pila.`);
            return valorEliminado;
        }
    }

    /**
     * @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK)
     * @return El elemento de la cima, o cadena vacía si la pila está vacía
     */
    peek() {
        if (this.isEmpty()) {
            console.log("ERROR: La pila está vacía. No hay elementos para ver (Peek).");
            return ""; // Cadena vacía para indicar error
        } else {
            return this.top.dato;
        }
    }

    /**
     * @brief Muestra todos los elementos de la pila
     */
    mostrar() {
        if (this.isEmpty()) {
            console.log("La pila está vacía.");
        } else {
            console.log("Elementos en la pila (desde el tope):");
            let actual = this.top;
            let posicion = 1;
            
            while (actual !== null) {
                console.log(`${posicion}. ${actual.dato}`);
                actual = actual.siguiente;
                posicion++;
            }
        }
    }

    /**
     * @brief Obtiene el tamaño actual de la pila
     * @return Número de elementos en la pila
     */
    getTamaño() {
        return this.tamaño;
    }

    /**
     * @brief Obtiene la capacidad máxima de la pila
     * @return Capacidad máxima
     */
    getCapacidad() {
        return this.capacidad;
    }
}

// Para simular la entrada del usuario en el navegador
const readline = require('readline');

/**
 * @brief Función para mostrar el menú de opciones
 */
function mostrarMenu() {
    console.log("\n---- MENU DE OPERACIONES ---");
    console.log("1. PUSH (Insertar elemento)");
    console.log("2. POP (Eliminar elemento)");
    console.log("3. PEEK (Ver elemento superior)");
    console.log("4. MOSTRAR (Mostrar todos los elementos)");
    console.log("5. ESTADO (Ver estado de la pila)");
    console.log("6. SALIR");
    console.log("-----------------------------");
    console.log("\n");
}

/**
 * @brief Función principal
 */
async function main() {
    const rl = readline.createInterface({
        input: process.stdin,
        output: process.stdout
    });

    // Función auxiliar para preguntar al usuario
    const pregunta = (query) => new Promise((resolve) => rl.question(query, resolve));

    try {
        console.log("--- Pila con Lista Enlazada Simple ---");
        const capacidadInput = await pregunta("Ingrese la capacidad de la pila: ");
        const capacidad = parseInt(capacidadInput);
        
        // Crear la pila
        const miPila = new Pila(capacidad);
        
        let opcion;
        
        do {
            mostrarMenu();
            const opcionInput = await pregunta("Seleccione una opción: ");
            opcion = parseInt(opcionInput);
            
            switch (opcion) {
                case 1: // PUSH
                    console.log("\n");
                    const valor = await pregunta("Ingrese el valor a insertar: ");
                    miPila.push(valor);
                    console.log("\n");
                    break;
                    
                case 2: // POP
                    console.log("\n");
                    miPila.pop();
                    break;
                    
                case 3: // PEEK
                    console.log("\n");
                    const tope = miPila.peek();
                    if (tope !== null && tope !== "") {
                        console.log(`Elemento en el tope: ${tope}`);
                    }
                    console.log("\n");
                    break;
                    
                case 4: // MOSTRAR
                    console.log("\n");
                    miPila.mostrar();
                    break;
                    
                case 5: // ESTADO
                    console.log("\n--- ESTADO DE LA PILA ---");
                    console.log(`Capacidad máxima: ${miPila.getCapacidad()}`);
                    console.log(`Elementos actuales: ${miPila.getTamaño()}`);
                    console.log(`¿Está vacía? ${miPila.isEmpty() ? "Sí" : "No"}`);
                    console.log(`¿Está llena? ${miPila.isFull() ? "Sí" : "No"}`);
                    console.log("\n");
                    break;
                    
                case 6: // SALIR
                    console.log("\n");
                    console.log("Saliendo del programa...");
                    break;
                    
                default:
                    console.log("Opción no válida. Intente nuevamente.");
                    break;
            }
            
        } while (opcion !== 6);
        
    } finally {
        rl.close();
    }
}

// Ejecutar el programa
if (typeof window === 'undefined') {
    // Entorno Node.js
    main().catch(console.error);
} 