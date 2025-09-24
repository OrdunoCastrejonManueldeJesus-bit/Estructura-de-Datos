class QuickSortSimplificado {
    
    static main() {
        // Generar 15 valores aleatorios
        const array = this.generarArrayAleatorio(15);
        
        console.log("Array original:");
        this.imprimirArray(array);
        
        // Ordenar con Quick Sort simplificado
        this.quickSort(array, 0, array.length - 1);
        
        console.log("\nArray ordenado:");
        this.imprimirArray(array);
    }
    
    // Generar array con valores aleatorios entre 1 y 100
    static generarArrayAleatorio(tamaño) {
        const array = new Array(tamaño);
        
        for (let i = 0; i < tamaño; i++) {
            array[i] = Math.floor(Math.random() * 100) + 1; // Valores entre 1 y 100
        }
        
        return array;
    }
    
    // Algoritmo Quick Sort simplificado
    static quickSort(array, izquierda, derecha) {
        if (izquierda < derecha) {
            const indicePivote = this.particion(array, izquierda, derecha);
            
            // Ordenar recursivamente las dos particiones
            this.quickSort(array, izquierda, indicePivote - 1);
            this.quickSort(array, indicePivote + 1, derecha);
        }
    }
    
    // Función de partición según la descripción
    static particion(array, izquierda, derecha) {
        // El primer elemento como pivote
        const pivote = array[izquierda];
        const indicePivote = izquierda;
        let storeIndex = izquierda + 1;
        
        for (let i = izquierda + 1; i <= derecha; i++) {
            let debeIntercambiar = false;
            
            // Verificar si debe intercambiar según las condiciones
            if (array[i] < pivote) {
                debeIntercambiar = true;
            } else if (array[i] === pivote) {
                // 50% de probabilidad cuando son iguales
                if (Math.random() < 0.5) {
                    debeIntercambiar = true;
                }
            }
            
            if (debeIntercambiar) {
                this.intercambiar(array, i, storeIndex);
                storeIndex++;
            }
        }
        
        // Colocar el pivote en su posición final
        this.intercambiar(array, indicePivote, storeIndex - 1);
        
        return storeIndex - 1;
    }
    
    // Función auxiliar para intercambiar elementos
    static intercambiar(array, i, j) {
        const temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    
    // Función para imprimir el array
    static imprimirArray(array) {
        let resultado = "";
        for (let i = 0; i < array.length; i++) {
            resultado += array[i];
            if (i < array.length - 1) {
                resultado += ", ";
            }
        }
        console.log(resultado);
    }
}

// Ejecutar el programa
QuickSortSimplificado.main();