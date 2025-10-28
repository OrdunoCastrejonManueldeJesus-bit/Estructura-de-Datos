#include <iostream>
#include <vector>
#include <algorithm>
#include <cmath>
#include <cstdlib> 
#include <ctime>  

using namespace std;
const int ARRAY_SIZE = 15;
const int MIN_VAL = 1;
const int MAX_VAL = 1000;

// Función para imprimir el arreglo
void printArray(const vector<int>& arr) {
    for (int val : arr) {
        cout << val << " ";
    }
    cout << endl;
}

// 1. Obtener el valor máximo en el arreglo
int getMax(const vector<int>& arr) {
    int maxVal = arr[0];
    for (int i = 1; i < arr.size(); i++) {
        if (arr[i] > maxVal) {
            maxVal = arr[i];
        }
    }
    return maxVal;
}

// 2. Función de Counting Sort 
void countSort(vector<int>& arr, int exp) {
    int n = arr.size();
    vector<int> output(n);
    vector<int> count(10, 0);

    for (int i = 0; i < n; i++) {
        int digit = (arr[i] / exp) % 10;
        count[digit]++;
    }

    for (int i = 1; i < 10; i++) {
        count[i] += count[i - 1];
    }

    for (int i = n - 1; i >= 0; i--) {
        int digit = (arr[i] / exp) % 10;
        output[count[digit] - 1] = arr[i];
        count[digit]--;
    }
    for (int i = 0; i < n; i++) {
        arr[i] = output[i];
    }
}

// 3. Función principal de Radix Sort
void radixSort(vector<int>& arr) {
    int m = getMax(arr);

    for (int exp = 1; m / exp > 0; exp *= 10) {
        countSort(arr, exp);
        cout << " -> Arreglo después de la pasada con exp = " << exp << ": ";
        printArray(arr);
    }
}

int main() {
    srand(time(0));

    // 4. Generar el arreglo de 15 valores aleatorios entre 1 y 1000
    vector<int> data(ARRAY_SIZE);
    for (int i = 0; i < ARRAY_SIZE; i++) {
        // Genera números en el rango [1, 1000]
        data[i] = (rand() % (MAX_VAL - MIN_VAL + 1)) + MIN_VAL;
    }

    cout << "--- Radix Sort (Simplificado) ---" << endl;
    cout << "Valores a ordenar: " << ARRAY_SIZE << endl;
    cout << "Rango de valores: [" << MIN_VAL << ", " << MAX_VAL << "]" << endl;
    
    // Imprimir el arreglo no ordenado
    cout << "\nArreglo Original: ";
    printArray(data);

    // Llamar al algoritmo Radix Sort
    cout << "\nIniciando Radix Sort..." << endl;
    radixSort(data);

    // Imprimir el arreglo ordenado final
    cout << "\nArreglo Final Ordenado: ";
    printArray(data);
    cout << "-----------------------------------" << endl;

    return 0;
}