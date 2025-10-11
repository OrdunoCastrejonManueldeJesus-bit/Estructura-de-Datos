#include <iostream>
#include <vector>
#include <algorithm>
#include <cstdlib>
#include <ctime>
#include <iomanip>

using namespace std;

// Función para implementar Bucket Sort
void bucketSort(float arr[], int n) {
    vector<float> buckets[n];
    
    for (int i = 0; i < n; i++) {
        int bucketIndex = n * arr[i];
        buckets[bucketIndex].push_back(arr[i]);
    }
    
    for (int i = 0; i < n; i++) {
        sort(buckets[i].begin(), buckets[i].end());
    }
    
    int index = 0;
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < buckets[i].size(); j++) {
            arr[index++] = buckets[i][j];
        }
    }
}

float generarAleatorio() {
    return static_cast<float>(rand() % 1000) / 1000.0f;
}

void imprimirArray(float arr[], int n) {
    for (int i = 0; i < n; i++) {
        cout << fixed << setprecision(3) << arr[i] << " ";
    }
    cout << endl;
}

int main() {
    srand(time(0));
    
    const int TAMANO = 15;
    float arr[TAMANO];
    
    // Generar 15 números aleatorios entre 0 y 1
    cout << "Generando 15 numeros aleatorios entre 0 y 1" << endl;
    for (int i = 0; i < TAMANO; i++) {
        arr[i] = generarAleatorio();
    }
    
    // Mostrar array original
    cout << "\nArray original:" << endl;
    imprimirArray(arr, TAMANO);
    
    // Aplicar Bucket Sort
    bucketSort(arr, TAMANO);
    
    // Mostrar array ordenado
    cout << "\nArray ordenado con Bucket Sort:" << endl;
    imprimirArray(arr, TAMANO);
    
    return 0;
}