#include <iostream>
#include <vector>
#include <random>
#include <ctime>

using namespace std;

// Método para combinar dos subarrays ordenados
void merge(vector<int>& array, int left, int middle, int right) {
    // Tamaños de los subarrays temporales
    int n1 = middle - left + 1;
    int n2 = right - middle;
    
    // Arrays temporales
    vector<int> leftArray(n1);
    vector<int> rightArray(n2);
    
    // Copia datos a los arrays temporales
    for (int i = 0; i < n1; i++) {
        leftArray[i] = array[left + i];
    }
    for (int j = 0; j < n2; j++) {
        rightArray[j] = array[middle + 1 + j];
    }
    
    // Combina los arrays temporales
    int i = 0, j = 0;
    int k = left;
    
    while (i < n1 && j < n2) {
        if (leftArray[i] <= rightArray[j]) {
            array[k] = leftArray[i];
            i++;
        } else {
            array[k] = rightArray[j];
            j++;
        }
        k++;
    }
    
    // Copia los elementos restantes de leftArray (si hay)
    while (i < n1) {
        array[k] = leftArray[i];
        i++;
        k++;
    }
    
    // Copia los elementos restantes de rightArray (si hay)
    while (j < n2) {
        array[k] = rightArray[j];
        j++;
        k++;
    }
}

// Método principal que implementa Merge Sort
void mergeSort(vector<int>& array, int left, int right) {
    if (left < right) {
        // Encuentra el punto medio
        int middle = (left + right) / 2;
        
        // Ordena la primera y segunda mitad
        mergeSort(array, left, middle);
        mergeSort(array, middle + 1, right);
        
        // Combina las mitades ordenadas
        merge(array, left, middle, right);
    }
}

// Método para generar números aleatorios
vector<int> generarArrayAleatorio(int tamaño, int maximo) {
    vector<int> array(tamaño);
    
    // Semilla para números aleatorios
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<> dis(1, maximo);
    
    for (int i = 0; i < tamaño; i++) {
        array[i] = dis(gen); // Números entre 1 y maximo
    }
    
    return array;
}

// Función para imprimir un vector
void imprimirVector(const vector<int>& vec) {
    cout << "[";
    for (size_t i = 0; i < vec.size(); i++) {
        cout << vec[i];
        if (i < vec.size() - 1) {
            cout << ", ";
        }
    }
    cout << "]";
}

// Método adicional para demostrar el proceso paso a paso
void demostrarMergeSort() {
    vector<int> demo = {38, 27, 43, 3, 9, 82, 10};
    cout << "Ejemplo paso a paso con array pequeño:" << endl;
    cout << "Array inicial: ";
    imprimirVector(demo);
    cout << endl;
    
    mergeSort(demo, 0, demo.size() - 1);
    
    cout << "Array final: ";
    imprimirVector(demo);
    cout << endl;
}

// Método principal
int main() {
    // Generar 15 números aleatorios entre 1 y 100
    vector<int> numeros = generarArrayAleatorio(15, 100);
    
    cout << "=== ORDENAMIENTO MERGE SORT ===" << endl;
    cout << "Array original:" << endl;
    imprimirVector(numeros);
    cout << endl;
    
    // Aplicar Merge Sort
    mergeSort(numeros, 0, numeros.size() - 1);
    
    cout << "\nArray ordenado:" << endl;
    imprimirVector(numeros);
    cout << endl;
    
    // Mostrar paso a paso (opcional)
    cout << "\n=== DEMOSTRACION PASO A PASO ===" << endl;
    demostrarMergeSort();
    
    return 0;
}