function ordenamientoPorSeleccion(array) {
    const n = array.length;
    
    // repeat (numOfElements - 1) times
    for (let i = 0; i < n - 1; i++) {
        // set the first unsorted element as the minimum
        let indiceMinimo = i;
        
        // for each of the unsorted elements
        for (let j = i + 1; j < n; j++) {
            // if element < currentMinimum
            if (array[j] < array[indiceMinimo]) {
                // set element as new minimum
                indiceMinimo = j;
            }
        }
        
        // swap minimum with first unsorted position
        if (indiceMinimo !== i) {
            const temp = array[i];
            array[i] = array[indiceMinimo];
            array[indiceMinimo] = temp;
        }
    }
}

// Crear array con 15 valores aleatorios
const array = new Array(15);

// Llenar el array con valores aleatorios entre 1 y 100
console.log("Array original:");
for (let i = 0; i < array.length; i++) {
    array[i] = Math.floor(Math.random() * 100) + 1;
    process.stdout.write(array[i] + " ");
}
console.log();

// Aplicar ordenamiento por selección
ordenamientoPorSeleccion(array);

// Mostrar array ordenado
console.log("Array ordenado:");
for (const num of array) {
    process.stdout.write(num + " ");
}
console.log();