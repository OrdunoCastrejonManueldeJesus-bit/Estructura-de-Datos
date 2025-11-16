// Clase del nodo
class Nodo {
    constructor(valor) {
        this.dato = valor;
        this.siguiente = null;
        this.anterior = null;
    }
}

// Clase de Lista Doblemente Enlazada
class ListaDoble {
    constructor() {
        this.cabeza = null;
        this.cola = null;
    }
    
    // Método para insertar al final
    insertarFinal(valor) {
        const nuevoNodo = new Nodo(valor);
        
        if (this.cabeza === null) {
            // Lista vacía
            this.cabeza = nuevoNodo;
            this.cola = nuevoNodo;
        } else {
            // Insertar al final
            this.cola.siguiente = nuevoNodo;
            nuevoNodo.anterior = this.cola;
            this.cola = nuevoNodo;
        }
    }
    
    // Método para mostrar la lista de inicio a fin
    mostrarAdelante() {
        if (this.cabeza === null) {
            console.log("La lista está vacía.");
            return;
        }
        
        let actual = this.cabeza;
        let resultado = "Lista (adelante): ";
        while (actual !== null) {
            resultado += actual.dato + " ";
            actual = actual.siguiente;
        }
        console.log(resultado);
    }
    
    // Método para mostrar la lista de fin a inicio
    mostrarAtras() {
        if (this.cola === null) {
            console.log("La lista está vacía.");
            return;
        }
        
        let actual = this.cola;
        let resultado = "Lista (atrás): ";
        while (actual !== null) {
            resultado += actual.dato + " ";
            actual = actual.anterior;
        }
        console.log(resultado);
    }
    
    // Método para generar lista con valores aleatorios
    generarListaAleatoria(cantidad) {
        for (let i = 0; i < cantidad; i++) {
            const valor = Math.floor(Math.random() * 100) + 1; // Números entre 1 y 100
            this.insertarFinal(valor);
        }
    }
}

// Función principal
function main() {
    const lista = new ListaDoble();
    
    console.log("=== LISTA DOBLEMENTE ENLAZADA CON VALORES ALEATORIOS ===");
    
    // Generar lista con 15 valores aleatorios
    lista.generarListaAleatoria(15);
    
    // Mostrar la lista en ambas direcciones
    lista.mostrarAdelante();
    lista.mostrarAtras();
}

// Ejecutar el programa
main();