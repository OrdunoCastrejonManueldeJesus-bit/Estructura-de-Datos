#include <iostream>
#include <vector>
#include <cstdlib>
#include <ctime>
#include <random>

using namespace std;

class QuickSortSimplificado {
    
public:
    static void main() {
        // Generar 15 valores aleatorios
        vector<int> array = generarArrayAleatorio(15);
        
        cout << "Array original:" << endl;
        imprimirArray(array);
        
        // Ordenar con Quick Sort simplificado
        quickSort(array, 0, array.size() - 1);
        
        cout << "\nArray ordenado:" << endl;
        imprimirArray(array);
    }
    
    // Generar array con valores aleatorios entre 1 y 100
    static vector<int> generarArrayAleatorio(int tamaño) {
        vector<int> array(tamaño);
        random_device rd;
        mt19937 gen(rd());
        uniform_int_distribution<> dis(1, 100);
        
        for (int i = 0; i < tamaño; i++) {
            array[i] = dis(gen); // Valores entre 1 y 100
        }
        
        return array;
    }
    
    // Algoritmo Quick Sort simplificado
    static void quickSort(vector<int>& array, int izquierda, int derecha) {
        if (izquierda < derecha) {
            int indicePivote = particion(array, izquierda, derecha);
            
            // Ordenar recursivamente las dos particiones
            quickSort(array, izquierda, indicePivote - 1);
            quickSort(array, indicePivote + 1, derecha);
        }
    }
    
    // Función de partición según la descripción
    static int particion(vector<int>& array, int izquierda, int derecha) {
        // El primer elemento como pivote
        int pivote = array[izquierda];
        int indicePivote = izquierda;
        int storeIndex = izquierda + 1;
        
        random_device rd;
        mt19937 gen(rd());
        uniform_real_distribution<> dis(0.0, 1.0);
        
        for (int i = izquierda + 1; i <= derecha; i++) {
            bool debeIntercambiar = false;
            
            // Verificar si debe intercambiar según las condiciones
            if (array[i] < pivote) {
                debeIntercambiar = true;
            } else if (array[i] == pivote) {
                // 50% de probabilidad cuando son iguales
                if (dis(gen) < 0.5) {
                    debeIntercambiar = true;
                }
            }
            
            if (debeIntercambiar) {
                intercambiar(array, i, storeIndex);
                storeIndex++;
            }
        }
        
        // Colocar el pivote en su posición final
        intercambiar(array, indicePivote, storeIndex - 1);
        
        return storeIndex - 1;
    }
    
    // Función auxiliar para intercambiar elementos
    static void intercambiar(vector<int>& array, int i, int j) {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    
    // Función para imprimir el array
    static void imprimirArray(const vector<int>& array) {
        for (size_t i = 0; i < array.size(); i++) {
            cout << array[i];
            if (i < array.size() - 1) {
                cout << ", ";
            }
        }
        cout << endl;
    }
};

int main() {
    // Inicializar semilla para números aleatorios
    srand(time(nullptr));
    
    QuickSortSimplificado::main();
    return 0;
}