function bucketSort(arr, n) {
    let buckets = new Array(n);

    for (let i = 0; i < n; i++) {
        buckets[i] = [];
    }
    
    for (let i = 0; i < n; i++) {
        let bucketIndex = Math.floor(n * arr[i]);
        if (bucketIndex === n) bucketIndex = n - 1;
        buckets[bucketIndex].push(arr[i]);
    }
    
    for (let i = 0; i < n; i++) {
        buckets[i].sort((a, b) => a - b);
    }
    
    let index = 0;
    for (let i = 0; i < n; i++) {
        for (let value of buckets[i]) {
            arr[index++] = value;
        }
    }
}

// Generar número aleatorio entre 0 y 1
function generarAleatorio() {
    return Math.random();
}

// Imprimir el arreglo
function imprimirArray(arr, n) {
    let result = "";
    for (let i = 0; i < n; i++) {
        result += arr[i].toFixed(3) + " ";
    }
    console.log(result);
}

function main() {
    const TAMANO = 15;
    let arr = new Array(TAMANO);

    // Generar 15 números aleatorios entre 0 y 1
    console.log("Generando 15 numeros aleatorios entre 0 y 1");
    for (let i = 0; i < TAMANO; i++) {
        arr[i] = generarAleatorio();
    }

    // Mostrar array original
    console.log("\nArray original:");
    imprimirArray(arr, TAMANO);

    // Aplicar Bucket Sort
    bucketSort(arr, TAMANO);

    // Mostrar array ordenado
    console.log("\nArray ordenado con Bucket Sort:");
    imprimirArray(arr, TAMANO);
}

main();