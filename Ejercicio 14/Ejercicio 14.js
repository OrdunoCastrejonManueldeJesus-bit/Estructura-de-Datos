//Listas enlazadas
class Nodo {
    constructor(valor) {
        this.valor = valor;
        this.siguiente = null;
    }
}

function main() {
    let cabeza = null;
    let actual = null;
    
    console.log("Generando 15 valores aleatorios");
    console.log(" ");
    
    // Crear 15 nodos con valores aleatorios
    for (let i = 0; i < 15; i++) {
        const valorAleatorio = 1 + Math.floor(Math.random() * 100); // 1-100
        const nuevoNodo = new Nodo(valorAleatorio);
        
        if (cabeza === null) {
            cabeza = nuevoNodo;
            actual = cabeza;
        } else {
            actual.siguiente = nuevoNodo;
            actual = nuevoNodo;
        }
    }
    
    // Mostrar la lista
    console.log("Lista Enlazada:");
    actual = cabeza;
    let contador = 1;
    while (actual !== null) {
        console.log(`Nodo ${contador}: ${actual.valor}`);
        actual = actual.siguiente;
        contador++;
    }
}
main();