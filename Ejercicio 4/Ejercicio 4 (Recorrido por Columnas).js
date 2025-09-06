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

console.log("\nRecorrido por columnas:");
// Recorrido por columnas
for (let columna = 0; columna < 3; columna++) {
    let contenidoColumna = "Columna " + (columna + 1) + ": ";
    for (let fila = 0; fila < 3; fila++) {
        contenidoColumna += matriz[fila][columna] + " ";
    }
    console.log(contenidoColumna);
}

console.log("\nRecorrido completo por columnas:");
// Recorrido completo columna por columna
for (let columna = 0; columna < 3; columna++) {
    for (let fila = 0; fila < 3; fila++) {
        console.log("Elemento [" + fila + "][" + columna + "] = " + matriz[fila][columna]);
    }
}