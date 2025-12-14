#ifndef BST_H
#define BST_H

#include <iostream>
#include <fstream>
#include <vector>
#include <queue>
#include <string>

// Estructura Nodo
struct Nodo {
    int key;
    Nodo* left;
    Nodo* right;
    
    // Constructor
    Nodo(int valor) : key(valor), left(nullptr), right(nullptr) {}
};

// Clase BST (Árbol Binario de Búsqueda)
class BST {
private:
    Nodo* root;
    
    // Métodos auxiliares privados (recursivos)
    Nodo* insertarRec(Nodo* nodo, int key);
    Nodo* buscarRec(Nodo* nodo, int key, std::vector<int>& ruta);
    Nodo* eliminarRec(Nodo* nodo, int key);
    void inorderRec(Nodo* nodo, std::vector<int>& resultado);
    void preorderRec(Nodo* nodo, std::vector<int>& resultado);
    void postorderRec(Nodo* nodo, std::vector<int>& resultado);
    int alturaRec(Nodo* nodo);
    int contarNodosRec(Nodo* nodo);
    Nodo* encontrarMinimo(Nodo* nodo);
    void destruirArbol(Nodo* nodo);
    void exportarInordenRec(Nodo* nodo, std::ofstream& archivo);
    
public:
    // Constructor y destructor
    BST();
    ~BST();
    
    // Métodos públicos
    void insertar(int key);
    bool buscar(int key, std::vector<int>& ruta);
    void eliminar(int key);
    std::vector<int> inorder();
    std::vector<int> preorder();
    std::vector<int> postorder();
    int altura();
    int size();
    void exportarInorden(const std::string& filename);
    void cargarDesdeArchivo(const std::string& filename);
    void guardarEnArchivo(const std::string& filename);
    bool estaVacio();
};

#endif