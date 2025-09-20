function main() {
    let array = new Array(15);
    
    // Generar números aleatorios
    for (let i = 0; i < array.length; i++) {
        array[i] = Math.floor(Math.random() * 100) + 1;
    }
    
    console.log("Array original:");
    mostrarArray(array);
    
    // Ordenamiento por inserción
    for (let i = 1; i < array.length; i++) {
        let elementoActual = array[i];
        let j = i - 1;
        
        while (j >= 0 && array[j] > elementoActual) {
            array[j + 1] = array[j];
            j--;
        }
        array[j + 1] = elementoActual;
    }
    
    console.log("Array ordenado:");
    mostrarArray(array);
}

function mostrarArray(array) {
    let resultado = "";
    for (let num of array) {
        resultado += num + " ";
    }
    console.log(resultado);
}

// Ejecutar el programa
main();