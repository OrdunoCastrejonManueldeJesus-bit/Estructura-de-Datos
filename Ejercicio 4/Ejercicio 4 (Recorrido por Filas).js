// Declarar e inicializar una matriz 3x3
const matriz = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
];

console.log("Matriz 3x3 original:");
// Mostrar la matriz original
for (let i = 0; i < 3; i++) {
    let fila = "";
    for (let j = 0; j < 3; j++) {
        fila += matriz[i][j] + " ";
    }
    console.log(fila);
}

console.log("\nRecorrido por filas:");
// Recorrido por filas
for (let fila = 0; fila < 3; fila++) {
    let contenidoFila = "Fila " + (fila + 1) + ": ";
    for (let columna = 0; columna < 3; columna++) {
        contenidoFila += matriz[fila][columna] + " ";
    }
    console.log(contenidoFila);
}

console.log("\nRecorrido completo por filas:");
// Recorrido completo fila por fila
for (let fila = 0; fila < 3; fila++) {
    for (let columna = 0; columna < 3; columna++) {
        console.log("Elemento [" + fila + "][" + columna + "] = " + matriz[fila][columna]);
    }
}