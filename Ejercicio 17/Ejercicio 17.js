// Clase del nodo
class Nodo {
    constructor(v) {
        this.valor = v;
        this.siguiente = null;
        this.anterior = null;
    }
}

// Clase de Lista Circular Doble
class ListaCircularDoble {
    constructor() {
        this.cabeza = null;
        this.tamaño = 0;
    }
    
    // Insertar al final
    insertarFinal(valor) {
        const nuevoNodo = new Nodo(valor);
        
        if (this.cabeza === null) {
            this.cabeza = nuevoNodo;
            this.cabeza.siguiente = this.cabeza;
            this.cabeza.anterior = this.cabeza;
        } else {
            const ultimo = this.cabeza.anterior;
            
            ultimo.siguiente = nuevoNodo;
            nuevoNodo.anterior = ultimo;
            nuevoNodo.siguiente = this.cabeza;
            this.cabeza.anterior = nuevoNodo;
        }
        
        this.tamaño++;
    }
    
    // Mostrar lista en sentido horario
    mostrarHorario() {
        if (this.cabeza === null) {
            console.log("Lista vacía");
            return;
        }
        
        process.stdout.write("Lista en sentido horario: ");
        let actual = this.cabeza;
        
        do {
            process.stdout.write(actual.valor + " ");
            actual = actual.siguiente;
        } while (actual !== this.cabeza);
        
        console.log();
    }
    
    // Mostrar lista en sentido antihorario
    mostrarAntihorario() {
        if (this.cabeza === null) {
            console.log("Lista vacía");
            return;
        }
        
        process.stdout.write("Lista en sentido antihorario: ");
        let actual = this.cabeza.anterior;
        
        do {
            process.stdout.write(actual.valor + " ");
            actual = actual.anterior;
        } while (actual !== this.cabeza.anterior);
        
        console.log();
    }
    
    // Obtener tamaño
    getTamaño() {
        return this.tamaño;
    }
    
    // Generar lista con valores aleatorios
    generarListaAleatoria(cantidad) {
        for (let i = 0; i < cantidad; i++) {
            const valor = Math.floor(Math.random() * 100) + 1; // Valores entre 1 y 100
            this.insertarFinal(valor);
        }
    }
}

// Función principal
function main() {
    const lista = new ListaCircularDoble();
    
    console.log("=== LISTA ENLAZADA CIRCULAR DOBLE ===");
    console.log("Generando 15 valores aleatorios");
    console.log();
    
    // Generar lista con 15 valores aleatorios
    lista.generarListaAleatoria(15);
    
    // Mostrar resultados
    console.log("Tamaño de la lista: " + lista.getTamaño());
    
    lista.mostrarHorario();
    lista.mostrarAntihorario();
}

// Versión alternativa con console.log para navegadores
class ListaCircularDobleBrowser {
    constructor() {
        this.cabeza = null;
        this.tamaño = 0;
    }
    
    // Insertar al final (igual que antes)
    insertarFinal(valor) {
        const nuevoNodo = new Nodo(valor);
        
        if (this.cabeza === null) {
            this.cabeza = nuevoNodo;
            this.cabeza.siguiente = this.cabeza;
            this.cabeza.anterior = this.cabeza;
        } else {
            const ultimo = this.cabeza.anterior;
            
            ultimo.siguiente = nuevoNodo;
            nuevoNodo.anterior = ultimo;
            nuevoNodo.siguiente = this.cabeza;
            this.cabeza.anterior = nuevoNodo;
        }
        
        this.tamaño++;
    }
    
    // Mostrar lista en sentido horario (versión para navegador)
    mostrarHorario() {
        if (this.cabeza === null) {
            console.log("Lista vacía");
            return;
        }
        
        let resultado = "Lista en sentido horario: ";
        let actual = this.cabeza;
        
        do {
            resultado += actual.valor + " ";
            actual = actual.siguiente;
        } while (actual !== this.cabeza);
        
        console.log(resultado);
    }
    
    // Mostrar lista en sentido antihorario (versión para navegador)
    mostrarAntihorario() {
        if (this.cabeza === null) {
            console.log("Lista vacía");
            return;
        }
        
        let resultado = "Lista en sentido antihorario: ";
        let actual = this.cabeza.anterior;
        
        do {
            resultado += actual.valor + " ";
            actual = actual.anterior;
        } while (actual !== this.cabeza.anterior);
        
        console.log(resultado);
    }
    
    // Obtener tamaño
    getTamaño() {
        return this.tamaño;
    }
    
    // Generar lista con valores aleatorios
    generarListaAleatoria(cantidad) {
        for (let i = 0; i < cantidad; i++) {
            const valor = Math.floor(Math.random() * 100) + 1;
            this.insertarFinal(valor);
        }
    }
}
main();

