const ARRAY_SIZE = 15;
const MIN_VAL = 1;
const MAX_VAL = 1000;

// Función para imprimir el arreglo
function printArray(arr) {
    console.log(arr.join(" "));
}

// 1. Obtener el valor máximo en el arreglo
function getMax(arr) {
    let maxVal = arr[0];
    for (let i = 1; i < arr.length; i++) {
        if (arr[i] > maxVal) {
            maxVal = arr[i];
        }
    }
    return maxVal;
}

// 2. Función de Counting Sort 
function countSort(arr, exp) {
    const n = arr.length;
    const output = new Array(n).fill(0);
    const count = new Array(10).fill(0);

    for (let i = 0; i < n; i++) {
        const digit = Math.floor(arr[i] / exp) % 10;
        count[digit]++;
    }

    for (let i = 1; i < 10; i++) {
        count[i] += count[i - 1];
    }

    for (let i = n - 1; i >= 0; i--) {
        const digit = Math.floor(arr[i] / exp) % 10;
        output[count[digit] - 1] = arr[i];
        count[digit]--;
    }

    for (let i = 0; i < n; i++) {
        arr[i] = output[i];
    }
}

// 3. Función principal de Radix Sort
function radixSort(arr) {
    const m = getMax(arr);

    for (let exp = 1; Math.floor(m / exp) > 0; exp *= 10) {
        countSort(arr, exp);
        process.stdout.write(` -> Arreglo después de la pasada con exp = ${exp}: `);
        printArray(arr);
    }
}

// Función principal
function main() {
    // 4. Generar el arreglo de 15 valores aleatorios entre 1 y 1000
    const data = [];
    for (let i = 0; i < ARRAY_SIZE; i++) {
        // Genera números en el rango [1, 1000]
        data.push(Math.floor(Math.random() * (MAX_VAL - MIN_VAL + 1)) + MIN_VAL);
    }

    console.log("--- Radix Sort (Simplificado) ---");
    console.log("Valores a ordenar: " + ARRAY_SIZE);
    console.log("Rango de valores: [" + MIN_VAL + ", " + MAX_VAL + "]");
    
    // Imprimir el arreglo no ordenado
    process.stdout.write("\nArreglo Original: ");
    printArray(data);

    // Llamar al algoritmo Radix Sort
    console.log("\nIniciando Radix Sort...");
    radixSort(data);

    // Imprimir el arreglo ordenado final
    process.stdout.write("\nArreglo Final Ordenado: ");
    printArray(data);
    console.log("-----------------------------------");
}
main();