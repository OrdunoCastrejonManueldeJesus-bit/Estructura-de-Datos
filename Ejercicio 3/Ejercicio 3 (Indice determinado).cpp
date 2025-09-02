#include <iostream>
#include <vector>
#include <string>

int main() {
    std::vector<int> numeros = {10, 20, 30, 40, 50};
    int valorAInsertar = 75;
    int indice = 2;

    if (indice >= 0 && indice <= numeros.size()) {
        numeros.insert(numeros.begin() + indice, valorAInsertar);
    } else {
        std::cout << "Índice fuera de rango." << std::endl;
    }

    // Mostrar el resultado
    std::cout << "Lista actualizada: ";
    for (size_t i = 0; i < numeros.size(); i++) {
        std::cout << numeros[i];
        if (i < numeros.size() - 1) {
            std::cout << ", ";
        }
    }
    std::cout << std::endl;

    return 0;
}