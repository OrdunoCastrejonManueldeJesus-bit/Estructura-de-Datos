#include <iostream>
#include <vector>
#include <random>
#include <set>
#include <algorithm>

class HashSortSimplificado {
    
public:
    static void main() {
        std::vector<int> valores = generarValoresAleatorios(15, 100);
        
        std::cout << "Valores originales:" << std::endl;
        imprimirVector(valores);
        std::vector<int> valoresOrdenados = hashSortSimplificado(valores);
        
        std::cout << "\nValores ordenados:" << std::endl;
        imprimirVector(valoresOrdenados);
    }
    
    static void imprimirVector(const std::vector<int>& vec) {
        std::cout << "[";
        for (size_t i = 0; i < vec.size(); i++) {
            std::cout << vec[i];
            if (i < vec.size() - 1) {
                std::cout << ", ";
            }
        }
        std::cout << "]" << std::endl;
    }

    static std::vector<int> generarValoresAleatorios(int cantidad, int max) {
        std::vector<int> valores(cantidad);
        std::random_device rd;
        std::mt19937 gen(rd());
        std::uniform_int_distribution<> dis(0, max - 1);
        
        for (int i = 0; i < cantidad; i++) {
            valores[i] = dis(gen);
        }
        
        return valores;
    }

    static std::vector<int> hashSortSimplificado(const std::vector<int>& valores) {
        if (valores.empty()) {
            return std::vector<int>();
        }
        
        int max = encontrarMaximo(valores);
        
        std::vector<bool> tablaHash(max + 1, false);
        
        for (int valor : valores) {
            if (valor >= 0 && valor < static_cast<int>(tablaHash.size())) {
                tablaHash[valor] = true;
            }
        }
        
        std::vector<int> ordenados;
        for (size_t i = 0; i < tablaHash.size(); i++) {
            if (tablaHash[i]) {
                ordenados.push_back(i);
            }
        }
        
        return ordenados;
    }
    
    static int encontrarMaximo(const std::vector<int>& valores) {
        int max = valores[0];
        for (size_t i = 1; i < valores.size(); i++) {
            if (valores[i] > max) {
                max = valores[i];
            }
        }
        return max;
    }
    
    static std::vector<int> hashSortConTreeSet(const std::vector<int>& valores) {
        std::set<int> set;
        
        for (int valor : valores) {
            set.insert(valor);
        }
        
        std::vector<int> resultado(set.size());
        int index = 0;
        for (int valor : set) {
            resultado[index++] = valor;
        }
        
        return resultado;
    }
};

int main() {
    HashSortSimplificado::main();
    return 0;
}