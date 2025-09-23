#include <iostream>
#include <cstdlib>
#include <ctime>
using namespace std;

void ordenamientoPorSeleccion(int array[], int n) {
    // repeat (numOfElements - 1) times
    for (int i = 0; i < n - 1; i++) {
        // set the first unsorted element as the minimum
        int indiceMinimo = i;
        
        // for each of the unsorted elements
        for (int j = i + 1; j < n; j++) {
            // if element < currentMinimum
            if (array[j] < array[indiceMinimo]) {
                // set element as new minimum
                indiceMinimo = j;
            }
        }
        
        // swap minimum with first unsorted position
        if (indiceMinimo != i) {
            int temp = array[i];
            array[i] = array[indiceMinimo];
            array[indiceMinimo] = temp;
        }
    }
}

int main() {
    // Semilla para números aleatorios
    srand(time(0));
    
    // Crear array con 15 valores aleatorios
    const int TAMANO = 15;
    int array[TAMANO];
    
    // Llenar el array con valores aleatorios entre 1 y 100
    cout << "Array original:" << endl;
    for (int i = 0; i < TAMANO; i++) {
        array[i] = rand() % 100 + 1;
        cout << array[i] << " ";
    }
    cout << endl;
    
    // Aplicar ordenamiento por selección
    ordenamientoPorSeleccion(array, TAMANO);
    
    // Mostrar array ordenado
    cout << "Array ordenado:" << endl;
    for (int i = 0; i < TAMANO; i++) {
        cout << array[i] << " ";
    }
    cout << endl;
    
    return 0;
}