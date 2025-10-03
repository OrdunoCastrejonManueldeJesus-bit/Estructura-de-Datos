import java.util.Random;
import java.util.Scanner;

public class ArregloTridimensional{
    private int[][][] arreglo;
    private int filas = 3;
    private int columnas = 3;
    private int profundidad = 3;

    public ArregloTridimensional() {
        arreglo = new int[filas][columnas][profundidad];
        inicializarArreglo();
    }

    private void inicializarArreglo() {
        Random random = new Random();
        for (int i = 0; i < filas; i++) {
            for (int j = 0; j < columnas; j++) {
                for (int k = 0; k < profundidad; k++) {
                    arreglo[i][j][k] = random.nextInt(10);
                }
            }
        }
    }

    public int contarNumerosPares() {
        int contador = 0;
        for (int i = 0; i < filas; i++) {
            for (int j = 0; j < columnas; j++) {
                for (int k = 0; k < profundidad; k++) {
                    if (arreglo[i][j][k] % 2 == 0) {
                        contador++;
                    }
                }
            }
        }
        return contador;
    }    

    public int[] sumarColumnas() {
        int[] sumaColumnas = new int[columnas];
        
        for (int j = 0; j < columnas; j++) {
            int suma = 0;
            for (int i = 0; i < filas; i++) {
                for (int k = 0; k < profundidad; k++) {
                    suma += arreglo[i][j][k];
                }
            }
            sumaColumnas[j] = suma;
        }
        return sumaColumnas;
    }

    public void buscarValor(int valor) {
        boolean encontrado = false;
        System.out.println("Buscando el valor: " + valor);
        
        for (int i = 0; i < filas; i++) {
            for (int j = 0; j < columnas; j++) {
                for (int k = 0; k < profundidad; k++) {
                    if (arreglo[i][j][k] == valor) {
                        System.out.println("Valor encontrado en posicion: [" + i + "][" + j + "][" + k + "]");
                        encontrado = true;
                    }
                }
            }
        }
        
        if (!encontrado) {
            System.out.println("El valor " + valor + " no se encontro en el arreglo.");
        }
    }

    public void mostrarArreglo() {
        System.out.println(" ");
        System.out.println("=== ARREGLO TRIDIMENSIONAL===");
        for (int i = 0; i < filas; i++) {
            System.out.println("Capa " + i + ":");
            for (int j = 0; j < columnas; j++) {
                System.out.print("  Fila " + j + ": ");
                for (int k = 0; k < profundidad; k++) {
                    System.out.print(arreglo[i][j][k] + " ");
                }
                System.out.println();
            }
            System.out.println();
        }
    }

    private static int leerEntero(Scanner scanner, String mensaje) {
        while (true) {
            try {
                System.out.print(mensaje);
                String input = scanner.nextLine().trim();
                
                if (input.isEmpty()) {
                    System.out.println("Error: No puede ingresar una entrada vacia. Intente nuevamente.");
                    continue;
                }
                
                return Integer.parseInt(input);
                
            } catch (NumberFormatException e) {
                System.out.println("Error: Por favor ingrese solo numeros enteros. Intente nuevamente.");
            }
        }
    }
    
    private static int leerOpcionMenu(Scanner scanner) {
        while (true) {
            try {
                System.out.print("Seleccione una opcion: ");
                String input = scanner.nextLine().trim();
                
                if (input.isEmpty()) {
                    System.out.println("Error: No puede ingresar una entrada vacia. Intente nuevamente.");
                    continue;
                }
                
                int opcion = Integer.parseInt(input);
                
                if (opcion >= 1 && opcion <= 5) {
                    return opcion;
                } else {
                    System.out.println("Error: Por favor ingrese una opcion entre 1 y 5. Intente nuevamente.");
                }
                
            } catch (NumberFormatException e) {
                System.out.println("Error: Por favor ingrese solo numeros. Intente nuevamente.");
            }
        }
    }

    public static void main(String[] args) {
        ArregloTridimensional programa = new ArregloTridimensional();
        Scanner scanner = new Scanner(System.in);
        
        int opcion;
        
        do {
            System.out.println("=== MENU PRINCIPAL ===");
            System.out.println("1. Mostrar arreglo");
            System.out.println("2. Conteo de numeros pares");
            System.out.println("3. Suma de columnas");
            System.out.println("4. Buscar valor");
            System.out.println("5. Salir");
            System.out.println(" ");
            
            opcion = leerOpcionMenu(scanner);
            
            switch (opcion) {
                case 1:
                    programa.mostrarArreglo();
                    break;
                    
                case 2:
                    int cantidadPares = programa.contarNumerosPares();
                    System.out.println(" ");
                    System.out.println("Cantidad de numeros pares: " + cantidadPares);
                    System.out.println("Total de elementos: " + (programa.filas * programa.columnas * programa.profundidad));
                    System.out.println("Porcentaje de pares: " + 
                        String.format("%.2f", cantidadPares * 100.0 / (programa.filas * programa.columnas * programa.profundidad)) + "%");
                    break;
                    
                case 3:
                    int[] sumaColumnas = programa.sumarColumnas();
                    System.out.println(" ");
                    System.out.println("=== SUMA DE COLUMNAS ===");
                    for (int j = 0; j < programa.columnas; j++) {
                        System.out.println("Columna " + j + ": " + sumaColumnas[j]);
                    }
                    break;
                    
                case 4:
                    System.out.println(" ");
                    int valor = leerEntero(scanner, "Ingrese el valor a buscar: ");
                    programa.buscarValor(valor);
                    break;
                    
                case 5:
                    System.out.println("Hasta luego");
                    break;
                    
                default:
                    System.out.println("Opción no válida. Intente nuevamente.");
            }
            System.out.println();
            
        } while (opcion != 5);
        
        scanner.close();
    }
}