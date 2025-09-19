#include <iostream>
#include <cstdlib>
#include <ctime>

using namespace std;

int bubbleSort(int arr[], int size) {
    int n = size;
    bool swapped;
    int swapCounter = 0;
    
    do {
        swapped = false;
        for (int i = 1; i < n; i++) {
            if (arr[i - 1] > arr[i]) {
                // Intercambiar elementos
                int temp = arr[i - 1];
                arr[i - 1] = arr[i];
                arr[i] = temp;
                
                swapped = true;
                swapCounter++;
            }
        }
        // Reducir el rango en cada iteración ya que el último elemento está en su posición correcta
        n--;
    } while (swapped);
    
    return swapCounter;
}

int main() {
    // Semilla para números aleatorios
    srand(time(0));
    
    // Crear un array de 15 elementos
    const int SIZE = 15;
    int array[SIZE];
    
    // Generar 15 valores aleatorios entre 1 y 100
    cout << "Array original (valores aleatorios):" << endl;
    for (int i = 0; i < SIZE; i++) {
        array[i] = rand() % 100 + 1; // Números entre 1 y 100
        cout << array[i] << " ";
    }
    cout << endl;
    
    // Aplicar el algoritmo Bubble Sort
    int swapCounter = bubbleSort(array, SIZE);
    
    // Mostrar el array ordenado
    cout << "\nArray ordenado:" << endl;
    for (int i = 0; i < SIZE; i++) {
        cout << array[i] << " ";
    }
    
    cout << "\n\nTotal de intercambios realizados: " << swapCounter << endl;
    
    return 0;
}