#include <iostream>
using namespace std;

int main() {
    // Declarar e inicializar una matriz 3x3
    int matriz[3][3] = {
        {1, 2, 3},
        {4, 5, 6},
        {7, 8, 9}
    };
    
    cout << "Matriz 3x3 original:" << endl;
    // Mostrar la matriz original
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            cout << matriz[i][j] << " ";
        }
        cout << endl;
    }
    
    cout << "\nRecorrido por filas:" << endl;
    // Recorrido por filas
    for (int fila = 0; fila < 3; fila++) {
        cout << "Fila " << fila + 1 << ": ";
        for (int columna = 0; columna < 3; columna++) {
            cout << matriz[fila][columna] << " ";
        }
        cout << endl;
    }
    
    cout << "\nRecorrido completo por filas:" << endl;
    // Recorrido completo fila por fila
    for (int fila = 0; fila < 3; fila++) {
        for (int columna = 0; columna < 3; columna++) {
            cout << "Elemento [" << fila << "][" << columna << "] = " 
                 << matriz[fila][columna] << endl;
        }
    }
    
    return 0;
}