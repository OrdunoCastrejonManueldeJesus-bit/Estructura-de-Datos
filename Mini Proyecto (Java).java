import java.util.Scanner;

public class GatitoMejorado {
    private char[][] tablero;
    private char jugadorActual;
    private boolean juegoTerminado;
    private Scanner scanner;
    private int movimientos;

    public GatitoMejorado() {
        tablero = new char[3][3];
        jugadorActual = 'X';
        juegoTerminado = false;
        movimientos = 0;
        scanner = new Scanner(System.in);
        inicializarTablero();
    }

    private void inicializarTablero() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                tablero[i][j] = '-';
            }
        }
    }

    public void jugar() {
        System.out.println("¡Bienvenido al juego del Gatito!");
        System.out.println("=====================================");
        System.out.println("Jugador 1: X");
        System.out.println("Jugador 2: O");
        System.out.println("=====================================\n");

        while (!juegoTerminado) {
            mostrarTablero();
            realizarMovimiento();
            movimientos++;
            verificarEstadoJuego();
            if (!juegoTerminado) {
                cambiarJugador();
            }
        }

        preguntarRevancha();
    }

    private void mostrarTablero() {
        System.out.println("\n  1 2 3");
        for (int i = 0; i < 3; i++) {
            System.out.print((i + 1) + " ");
            for (int j = 0; j < 3; j++) {
                System.out.print(tablero[i][j] + " ");
            }
            System.out.println();
        }
        System.out.println();
    }

    private void realizarMovimiento() {
        boolean movimientoValido = false;
        
        while (!movimientoValido) {
            try {
                System.out.println("Turno del jugador " + (jugadorActual == 'X' ? "1 (X)" : "2 (O)"));
                System.out.print("Ingresa fila (1-3): ");
                int fila = Integer.parseInt(scanner.nextLine()) - 1;
                System.out.print("Ingresa columna (1-3): ");
                int columna = Integer.parseInt(scanner.nextLine()) - 1;

                if (fila >= 0 && fila < 3 && columna >= 0 && columna < 3) {
                    if (tablero[fila][columna] == '-') {
                        tablero[fila][columna] = jugadorActual;
                        movimientoValido = true;
                    } else {
                        System.out.println("Esa casilla ya está ocupada! Intenta de nuevo.\n");
                    }
                } else {
                    System.out.println("¡Posición inválida! Usa números del 1 al 3.\n");
                }
            } catch (NumberFormatException e) {
                System.out.println("¡Entrada inválida! Por favor ingresa números.\n");
            }
        }
    }

    private void verificarEstadoJuego() {
        // Verificar victoria
        if (hayGanador()) {
            juegoTerminado = true;
            mostrarTablero();
            System.out.println(" ¡Felicidades! El jugador " + 
                (jugadorActual == 'X' ? "1 (X)" : "2 (O)") + " gana! ");
            return;
        }

        // Verificar empate
        if (movimientos == 9) {
            juegoTerminado = true;
            mostrarTablero();
            System.out.println(" ¡Empate! El tablero está lleno sin ganador.");
        }
    }

    private boolean hayGanador() {
        // Verificar filas y columnas
        for (int i = 0; i < 3; i++) {
            // Filas
            if (tablero[i][0] != '-' && 
                tablero[i][0] == tablero[i][1] && 
                tablero[i][1] == tablero[i][2]) {
                return true;
            }
            // Columnas
            if (tablero[0][i] != '-' && 
                tablero[0][i] == tablero[1][i] && 
                tablero[1][i] == tablero[2][i]) {
                return true;
            }
        }

        // Verificar diagonales
        if (tablero[0][0] != '-' && 
            tablero[0][0] == tablero[1][1] && 
            tablero[1][1] == tablero[2][2]) {
            return true;
        }

        if (tablero[0][2] != '-' && 
            tablero[0][2] == tablero[1][1] && 
            tablero[1][1] == tablero[2][0]) {
            return true;
        }

        return false;
    }

    private void cambiarJugador() {
        jugadorActual = (jugadorActual == 'X') ? 'O' : 'X';
    }

    private void preguntarRevancha() {
        System.out.print("\n¿Quieres jugar de nuevo? (s/n): ");
        String respuesta = scanner.nextLine().toLowerCase();
        
        if (respuesta.equals("s") || respuesta.equals("si")) {
            reiniciarJuego();
            jugar();
        } else {
            System.out.println("¡Gracias por jugar!");
        }
    }

    private void reiniciarJuego() {
        inicializarTablero();
        jugadorActual = 'X';
        juegoTerminado = false;
        movimientos = 0;
    }

    public static void main(String[] args) {
        GatitoMejorado juego = new GatitoMejorado();
        juego.jugar();
    }
}