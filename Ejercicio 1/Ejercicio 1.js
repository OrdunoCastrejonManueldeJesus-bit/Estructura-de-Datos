// Función principal del programa
function ejecutarPrograma() {
    
    // Declaración e inicialización
    let valoresNumericos = new Array(6);
    
    // Asignación de valores iniciales
    valoresNumericos[0] = 10;
    valoresNumericos[1] = 20;
    valoresNumericos[2] = 30;
    valoresNumericos[3] = 40;
    valoresNumericos[4] = 50;
    valoresNumericos[5] = 60;
    
    // Modificación de un elemento
    valoresNumericos[2] = 100;
    
    console.log("Contenido del arreglo:");
    for (let indice = 0; indice < valoresNumericos.length; indice++) {
        console.log(`valoresNumericos[${indice}] = ${valoresNumericos[indice]}`);
    }
    
    // Operación matemática
    let resultadoSuma = valoresNumericos[0] + valoresNumericos[2];
    console.log(`\nResultado de valores numericos + valores numericos = ${resultadoSuma}`);
}

ejecutarPrograma();