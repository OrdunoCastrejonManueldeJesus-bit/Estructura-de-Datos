// Método principal que implementa Merge Sort
function mergeSort(array, left, right) {
    if (left < right) {
        // Encuentra el punto medio
        const middle = Math.floor((left + right) / 2);
        
        // Ordena la primera y segunda mitad
        mergeSort(array, left, middle);
        mergeSort(array, middle + 1, right);
        
        // Combina las mitades ordenadas
        merge(array, left, middle, right);
    }
}

// Método para combinar dos subarrays ordenados
function merge(array, left, middle, right) {
    // Tamaños de los subarrays temporales
    const n1 = middle - left + 1;
    const n2 = right - middle;
    
    // Arrays temporales
    const leftArray = new Array(n1);
    const rightArray = new Array(n2);
    
    // Copia datos a los arrays temporales
    for (let i = 0; i < n1; i++) {
        leftArray[i] = array[left + i];
    }
    for (let j = 0; j < n2; j++) {
        rightArray[j] = array[middle + 1 + j];
    }
    
    // Combina los arrays temporales
    let i = 0, j = 0;
    let k = left;
    
    while (i < n1 && j < n2) {
        if (leftArray[i] <= rightArray[j]) {
            array[k] = leftArray[i];
            i++;
        } else {
            array[k] = rightArray[j];
            j++;
        }
        k++;
    }
    
    // Copia los elementos restantes de leftArray (si hay)
    while (i < n1) {
        array[k] = leftArray[i];
        i++;
        k++;
    }
    
    // Copia los elementos restantes de rightArray (si hay)
    while (j < n2) {
        array[k] = rightArray[j];
        j++;
        k++;
    }
}

// Método para generar números aleatorios
function generarArrayAleatorio(tamaño, maximo) {
    const array = new Array(tamaño);
    
    for (let i = 0; i < tamaño; i++) {
        array[i] = Math.floor(Math.random() * maximo) + 1; // Números entre 1 y maximo
    }
    
    return array;
}

// Método principal
function main() {
    // Generar 15 números aleatorios entre 1 y 100
    const numeros = generarArrayAleatorio(15, 100);
    
    console.log("=== ORDENAMIENTO MERGE SORT ===");
    console.log("Array original:");
    console.log(JSON.stringify(numeros));
    
    // Aplicar Merge Sort
    mergeSort(numeros, 0, numeros.length - 1);
    
    console.log("\nArray ordenado:");
    console.log(JSON.stringify(numeros));
    
    // Mostrar paso a paso (opcional)
    console.log("\n=== DEMOSTRACION PASO A PASO ===");
    demostrarMergeSort();
}

// Método adicional para demostrar el proceso paso a paso
function demostrarMergeSort() {
    const demo = [38, 27, 43, 3, 9, 82, 10];
    console.log("Ejemplo paso a paso con array pequeño:");
    console.log("Array inicial: " + JSON.stringify(demo));
    
    mergeSort(demo, 0, demo.length - 1);
    
    console.log("Array final: " + JSON.stringify(demo));
}

main();