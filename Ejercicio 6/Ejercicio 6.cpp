#include <iostream>
#include <cstdlib>
#include <ctime>
using namespace std;

void mostrarArray(int array[], int tamaño) {
    for (int i = 0; i < tamaño; i++) {
        cout << array[i] << " ";
    }
    cout << endl;
}

int main() {
    const int TAMANO = 15;
    int array[TAMANO];
    
    // Semilla para números aleatorios
    srand(time(0));
    
    // Generar números aleatorios
    for (int i = 0; i < TAMANO; i++) {
        array[i] = rand() % 100 + 1;
    }
    
    cout << "Array original:" << endl;
    mostrarArray(array, TAMANO);
    
    // Ordenamiento por inserción
    for (int i = 1; i < TAMANO; i++) {
        int elementoActual = array[i];
        int j = i - 1;
        
        while (j >= 0 && array[j] > elementoActual) {
            array[j + 1] = array[j];
            j--;
        }
        array[j + 1] = elementoActual;
    }
    
    cout << "Array ordenado:" << endl;
    mostrarArray(array, TAMANO);
    
    return 0;
}