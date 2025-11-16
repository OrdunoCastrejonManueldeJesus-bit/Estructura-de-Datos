// Clase del nodo
class Nodo {
    constructor(val) {
        this.valor = val;
        this.siguiente = null;
    }
}

// Clase Lista Circular
class ListaCircular {
    constructor() {
        this.cabeza = null;
        this.cola = null;
    }
    
    // Insertar al final de la lista
    insertar(valor) {
        const nuevoNodo = new Nodo(valor);
        
        if (this.cabeza === null) {
            // Lista vacía
            this.cabeza = nuevoNodo;
            this.cola = nuevoNodo;
            nuevoNodo.siguiente = this.cabeza;
        } else {
            // Insertar al final
            this.cola.siguiente = nuevoNodo;
            this.cola = nuevoNodo;
            this.cola.siguiente = this.cabeza;
        }
    }
    
    // Mostrar la lista circular
    mostrar() {
        if (this.cabeza === null) {
            console.log("Lista vacía");
            return;
        }
        
        let actual = this.cabeza;
        let contador = 0;
        let output = "Lista Circular: ";
        
        do {
            output += actual.valor;
            actual = actual.siguiente;
            contador++;
            
            if (actual !== this.cabeza) {
                output += " -> ";
            }
            
            // Salto de línea cada 5 elementos para mejor visualización
            if (contador % 5 === 0 && actual !== this.cabeza) {
                output += "\n               ";
            }
        } while (actual !== this.cabeza);
        
        output += " -> (vuelve al inicio)";
        console.log(output);
    }
    
    // Verificar si la lista está vacía
    estaVacia() {
        return this.cabeza === null;
    }
    
    // Obtener tamaño de la lista
    obtenerTamano() {
        if (this.cabeza === null) return 0;
        
        let contador = 0;
        let actual = this.cabeza;
        
        do {
            contador++;
            actual = actual.siguiente;
        } while (actual !== this.cabeza);
        
        return contador;
    }
}

// Función para generar número aleatorio entre 1 y 100
function generarAleatorio() {
    return Math.floor(Math.random() * 100) + 1;
}

// Función principal
function main() {
    // Crear lista circular
    const lista = new ListaCircular();
    
    console.log("=== LISTA ENLAZADA CIRCULAR CON 15 VALORES ALEATORIOS ===");
    console.log("Generando valores aleatorios...");
    console.log();
    
    // Insertar 15 valores aleatorios
    for (let i = 0; i < 15; i++) {
        const valor = generarAleatorio();
        lista.insertar(valor);
        console.log("Insertado: " + valor);
    }
    
    console.log();
    
    // Mostrar la lista completa
    lista.mostrar();
}
main();