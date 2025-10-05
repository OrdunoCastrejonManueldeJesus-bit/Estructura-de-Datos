function heapSort(arr) {
    const n = arr.length;
    
    for (let i = Math.floor(n / 2) - 1; i >= 0; i--) {
        heapify(arr, n, i);
    }
    
    for (let i = n - 1; i > 0; i--) {
        const temp = arr[0];
        arr[0] = arr[i];
        arr[i] = temp;
        
        heapify(arr, i, 0);
    }
}

function heapify(arr, n, i) {
    let largest = i;
    const left = 2 * i + 1;
    const right = 2 * i + 2;
    
    if (left < n && arr[left] > arr[largest]) {
        largest = left;
    }
    
    if (right < n && arr[right] > arr[largest]) {
        largest = right;
    }
    
    if (largest !== i) {
        const swap = arr[i];
        arr[i] = arr[largest];
        arr[largest] = swap;
        
        heapify(arr, n, largest);
    }
}

function printArray(arr) {
    console.log(arr.join(" "));
}

function generarArrayAleatorio(tamaño, min, max) {
    const arr = new Array(tamaño);
    
    for (let i = 0; i < tamaño; i++) {
        arr[i] = Math.floor(Math.random() * (max - min + 1)) + min;
    }
    return arr;
}

// Ejecución del programa
const arr = generarArrayAleatorio(15, 1, 100);

console.log("Array original (15 valores aleatorios):");
printArray(arr);

heapSort(arr);

console.log("\nArray ordenado con Heap Sort:");
printArray(arr);