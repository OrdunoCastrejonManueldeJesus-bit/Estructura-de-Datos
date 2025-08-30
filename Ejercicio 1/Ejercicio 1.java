public class Main {
    public static void ejecutarPrograma() {
        
        // Declaración e inicialización
        int[] valoresNumericos = new int[6];
        
        // Asignación de valores iniciales
        valoresNumericos[0] = 10;
        valoresNumericos[1] = 20;
        valoresNumericos[2] = 30;
        valoresNumericos[3] = 40;
        valoresNumericos[4] = 50;
        valoresNumericos[5] = 60;
        
        // Modificación de un elemento
        valoresNumericos[2] = 100;
        
        System.out.println("Contenido del arreglo:");
        for (int indice = 0; indice < valoresNumericos.length; indice++) {
            System.out.println("valoresNumericos[" + indice + "] = " + valoresNumericos[indice]);
        }
        
        // Operación matemática
        int resultadoSuma = valoresNumericos[0] + valoresNumericos[2];
        System.out.println("\nResultado de valores numericos + valores numericos = " + resultadoSuma);
    }
    
    public static void main(String[] args) {
        ejecutarPrograma();
    }
}