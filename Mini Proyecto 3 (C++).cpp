#include <iostream>
#include <vector>
#include <cstdlib>
#include <ctime>
#include <string>
#include <algorithm>
#include <limits>

using namespace std;

const int TAMANIO_TABLERO = 10;
const char AGUA = '~';
const char BARCO = 'B';
const char IMPACTO = 'X';
const char FALLO = 'O';

enum Dificultad { FACIL, NORMAL, DIFICIL };
enum Direccion { HORIZONTAL, VERTICAL };

class Tablero {
private:
    vector<vector<char>> celdas;
    vector<vector<bool>> barcosColocados;

public:
    Tablero() {
        celdas = vector<vector<char>>(TAMANIO_TABLERO, vector<char>(TAMANIO_TABLERO, AGUA));
        barcosColocados = vector<vector<bool>>(TAMANIO_TABLERO, vector<bool>(TAMANIO_TABLERO, false));
    }

    void mostrar(bool mostrarBarcos = false) {
        cout << "  ";
        for (int i = 0; i < TAMANIO_TABLERO; i++) {
            cout << i << " ";
        }
        cout << endl;

        for (int i = 0; i < TAMANIO_TABLERO; i++) {
            cout << static_cast<char>('A' + i) << " ";
            for (int j = 0; j < TAMANIO_TABLERO; j++) {
                if (mostrarBarcos && barcosColocados[i][j]) {
                    cout << BARCO << " ";
                } else {
                    cout << celdas[i][j] << " ";
                }
            }
            cout << endl;
        }
    }

    bool colocarBarco(int fila, int columna, int tamaño, Direccion dir) {
        if (dir == HORIZONTAL) {
            if (columna + tamaño > TAMANIO_TABLERO) return false;
            for (int i = 0; i < tamaño; i++) {
                if (barcosColocados[fila][columna + i]) return false;
            }
            for (int i = 0; i < tamaño; i++) {
                barcosColocados[fila][columna + i] = true;
            }
        } else {
            if (fila + tamaño > TAMANIO_TABLERO) return false;
            for (int i = 0; i < tamaño; i++) {
                if (barcosColocados[fila + i][columna]) return false;
            }
            for (int i = 0; i < tamaño; i++) {
                barcosColocados[fila + i][columna] = true;
            }
        }
        return true;
    }

    bool realizarDisparo(int fila, int columna) {
        if (fila < 0 || fila >= TAMANIO_TABLERO || columna < 0 || columna >= TAMANIO_TABLERO) {
            return false;
        }

        if (barcosColocados[fila][columna]) {
            celdas[fila][columna] = IMPACTO;
            return true;
        } else {
            celdas[fila][columna] = FALLO;
            return false;
        }
    }

    bool todosBarcosHundidos() {
        for (int i = 0; i < TAMANIO_TABLERO; i++) {
            for (int j = 0; j < TAMANIO_TABLERO; j++) {
                if (barcosColocados[i][j] && celdas[i][j] != IMPACTO) {
                    return false;
                }
            }
        }
        return true;
    }

    bool hayBarcoEn(int fila, int columna) {
        return barcosColocados[fila][columna];
    }

    char getCelda(int fila, int columna) {
        return celdas[fila][columna];
    }
};

class Juego {
private:
    Tablero tableroJugador1;
    Tablero tableroJugador2;
    vector<int> barcos = {5, 4, 3, 3, 2}; // Tamaños de los barcos

public:
    void colocarBarcos(int jugador) {
        cout << "\n=== JUGADOR " << jugador << " - COLOCACIÓN DE BARCOS ===" << endl;
        cout << "Tamaños de barcos: 5, 4, 3, 3, 2" << endl;

        Tablero& tableroActual = (jugador == 1) ? tableroJugador1 : tableroJugador2;

        for (int tamaño : barcos) {
            bool colocado = false;
            while (!colocado) {
                tableroActual.mostrar(true);
                cout << "Jugador " << jugador << ": Colocando barco de tamaño " << tamaño << endl;

                int fila, columna;
                char direccion;
                Direccion dir;

                cout << "Fila (A-J): ";
                char filaChar;
                cin >> filaChar;
                fila = toupper(filaChar) - 'A';

                cout << "Columna (0-9): ";
                cin >> columna;

                cout << "Dirección (H - Horizontal, V - Vertical): ";
                cin >> direccion;
                dir = (toupper(direccion) == 'H') ? HORIZONTAL : VERTICAL;

                if (fila < 0 || fila >= TAMANIO_TABLERO || columna < 0 || columna >= TAMANIO_TABLERO) {
                    cout << "Posición inválida. Intenta de nuevo." << endl;
                    continue;
                }

                colocado = tableroActual.colocarBarco(fila, columna, tamaño, dir);
                if (!colocado) {
                    cout << "No se puede colocar el barco ahí. Intenta de nuevo." << endl;
                }
            }
        }
        
        // Limpiar pantalla para que el siguiente jugador no vea la colocación
        cout << "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
        cout << "Jugador " << jugador << " ha terminado de colocar barcos. ";
        cout << "Presiona Enter para continuar...";
        cin.ignore();
        cin.get();
        cout << "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
    }

    void jugar() {
        // Ambos jugadores colocan sus barcos
        colocarBarcos(1);
        colocarBarcos(2);

        bool turnoJugador1 = true;

        while (!juegoTerminado()) {
            if (turnoJugador1) {
                turnoJugador(1, tableroJugador2);
            } else {
                turnoJugador(2, tableroJugador1);
            }
            turnoJugador1 = !turnoJugador1;
        }

        mostrarResultadoFinal();
    }

private:
    void turnoJugador(int jugador, Tablero& tableroOponente) {
        cout << "\n=== TURNO DEL JUGADOR " << jugador << " ===" << endl;
        
        // Mostrar el tablero propio del jugador
        cout << "Tu tablero:" << endl;
        Tablero& tableroPropio = (jugador == 1) ? tableroJugador1 : tableroJugador2;
        tableroPropio.mostrar(true);
        
        // Mostrar el tablero del oponente (solo impactos/fallos)
        cout << "\nTablero del oponente:" << endl;
        tableroOponente.mostrar();

        int fila, columna;
        bool disparoValido = false;

        while (!disparoValido) {
            cout << "Jugador " << jugador << ", dispara (ej: A 5): ";
            char filaChar;
            cin >> filaChar >> columna;
            fila = toupper(filaChar) - 'A';

            if (fila >= 0 && fila < TAMANIO_TABLERO && columna >= 0 && columna < TAMANIO_TABLERO) {
                if (tableroOponente.getCelda(fila, columna) == AGUA) {
                    disparoValido = true;
                } else {
                    cout << "Ya disparaste ahí. Elige otra posición." << endl;
                }
            } else {
                cout << "Posición inválida. Intenta de nuevo." << endl;
            }
        }

        bool impacto = tableroOponente.realizarDisparo(fila, columna);
        if (impacto) {
            cout << "¡IMPACTO!" << endl;
        } else {
            cout << "Agua..." << endl;
        }

        // Esperar antes de cambiar de turno
        cout << "Presiona Enter para continuar...";
        cin.ignore();
        cin.get();
        cout << "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
    }

    bool juegoTerminado() {
        return tableroJugador1.todosBarcosHundidos() || tableroJugador2.todosBarcosHundidos();
    }

    void mostrarResultadoFinal() {
        cout << "\n=== FIN DEL JUEGO ===" << endl;
        
        if (tableroJugador1.todosBarcosHundidos()) {
            cout << "¡JUGADOR 2 HA GANADO!" << endl;
        } else {
            cout << "¡JUGADOR 1 HA GANADO!" << endl;
        }

        cout << "\nTablero final del Jugador 1:" << endl;
        tableroJugador1.mostrar(true);
        
        cout << "\nTablero final del Jugador 2:" << endl;
        tableroJugador2.mostrar(true);
    }
};

int main() {
    srand(time(0));

    cout << "=== BATALLA NAVAL - 1 VS 1 ===" << endl;
    cout << "Modo de juego: Dos jugadores" << endl;

    Juego juego;
    juego.jugar();

    return 0;
}