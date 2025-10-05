class HashSortSimplificado {
    
    static main() {
        const valores = this.generarValoresAleatorios(15, 100);
        
        console.log("Valores originales:");
        console.log(this.arrayToString(valores));
        const valoresOrdenados = this.hashSortSimplificado(valores);
        
        console.log("\nValores ordenados:");
        console.log(this.arrayToString(valoresOrdenados));
    }
    
    static arrayToString(arr) {
        return "[" + arr.join(", ") + "]";
    }

    static generarValoresAleatorios(cantidad, max) {
        const valores = new Array(cantidad);
        
        for (let i = 0; i < cantidad; i++) {
            valores[i] = Math.floor(Math.random() * max);
        }
        
        return valores;
    }

    static hashSortSimplificado(valores) {
        if (valores.length === 0) {
            return [];
        }
        
        const max = this.encontrarMaximo(valores);
        
        const tablaHash = new Array(max + 1).fill(false);
        
        for (const valor of valores) {
            if (valor >= 0 && valor < tablaHash.length) {
                tablaHash[valor] = true;
            }
        }
        
        const ordenados = [];
        for (let i = 0; i < tablaHash.length; i++) {
            if (tablaHash[i]) {
                ordenados.push(i);
            }
        }
        
        return ordenados;
    }
    
    static encontrarMaximo(valores) {
        let max = valores[0];
        for (let i = 1; i < valores.length; i++) {
            if (valores[i] > max) {
                max = valores[i];
            }
        }
        return max;
    }
    
    static hashSortConTreeSet(valores) {
        const set = new Set();
        
        for (const valor of valores) {
            set.add(valor);
        }
        
        const resultado = Array.from(set);
        resultado.sort((a, b) => a - b);
        
        return resultado;
    }
}

// Ejecutar el programa
HashSortSimplificado.main();