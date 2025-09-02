#include <iostream>
#include <vector>
#include <string>

int main() {
    int arreglo[] = {12, 23, 57, 45, 34, 67, 89, 98};
    int valorBuscado = 23;
    int tamaño = sizeof(arreglo) / sizeof(arreglo[0]);
    
    std::vector<int> indices;
    
    for (int index = 0; index < tamaño; index++) {
        if (arreglo[index] == valorBuscado) {
            indices.push_back(index);
        }
    }
    
    if (indices.size() > 0) {
        std::cout << "Valor encontrado en los índices: ";
        for (size_t i = 0; i < indices.size(); i++) {
            std::cout << indices[i];
            if (i < indices.size() - 1) {
                std::cout << ", ";
            }
        }
        std::cout << std::endl;
    } else {
        std::cout << "Valor no encontrado en el arreglo." << std::endl;
    }
    
    return 0;
}