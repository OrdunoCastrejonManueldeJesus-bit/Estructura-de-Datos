#include <iostream>
#include <vector>
#include <random>
#include <ctime>

using namespace std;

void heapify(vector<int>& arr, int n, int i) {
    int largest = i;
    int left = 2 * i + 1;
    int right = 2 * i + 2;
    
    if (left < n && arr[left] > arr[largest]) {
        largest = left;
    }
    
    if (right < n && arr[right] > arr[largest]) {
        largest = right;
    }
    
    if (largest != i) {
        swap(arr[i], arr[largest]);
        heapify(arr, n, largest);
    }
}

void heapSort(vector<int>& arr) {
    int n = arr.size();
    
    for (int i = n / 2 - 1; i >= 0; i--) {
        heapify(arr, n, i);
    }
    
    for (int i = n - 1; i > 0; i--) {
        swap(arr[0], arr[i]);
        heapify(arr, i, 0);
    }
}

void printArray(const vector<int>& arr) {
    for (int i = 0; i < arr.size(); i++) {
        cout << arr[i] << " ";
    }
    cout << endl;
}

vector<int> generarArrayAleatorio(int tamaño, int min, int max) {
    vector<int> arr(tamaño);
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<> distrib(min, max);
    
    for (int i = 0; i < tamaño; i++) {
        arr[i] = distrib(gen);
    }
    return arr;
}

int main() {
    vector<int> arr = generarArrayAleatorio(15, 1, 100);
    
    cout << "Array original (15 valores aleatorios):" << endl;
    printArray(arr);
    
    heapSort(arr);
    
    cout << "\nArray ordenado con Heap Sort:" << endl;
    printArray(arr);
    
    return 0;
}