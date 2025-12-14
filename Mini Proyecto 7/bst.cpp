#include "bst.h"

// Constructor
BST::BST() : root(nullptr) {}

// Destructor
BST::~BST() {
    destruirArbol(root);
}

// Destruir árbol (liberar memoria)
void BST::destruirArbol(Nodo* nodo) {
    if (nodo != nullptr) {
        destruirArbol(nodo->left);
        destruirArbol(nodo->right);
        delete nodo;
    }
}

// Insertar clave (método público)
void BST::insertar(int key) {
    root = insertarRec(root, key);
}

// Insertar recursivo
Nodo* BST::insertarRec(Nodo* nodo, int key) {
    // Caso base: árbol vacío o llegamos a hoja
    if (nodo == nullptr) {
        return new Nodo(key);
    }
    
    // Insertar en subárbol izquierdo o derecho
    if (key < nodo->key) {
        nodo->left = insertarRec(nodo->left, key);
    } else if (key > nodo->key) {
        nodo->right = insertarRec(nodo->right, key);
    }
    // Si la clave ya existe, no hacemos nada
    
    return nodo;
}

// Buscar clave (método público)
bool BST::buscar(int key, std::vector<int>& ruta) {
    ruta.clear();
    Nodo* resultado = buscarRec(root, key, ruta);
    return resultado != nullptr;
}

// Buscar recursivo
Nodo* BST::buscarRec(Nodo* nodo, int key, std::vector<int>& ruta) {
    // Caso base: nodo nulo o encontramos la clave
    if (nodo == nullptr) {
        return nullptr;
    }
    
    // Agregar nodo actual a la ruta
    ruta.push_back(nodo->key);
    
    if (nodo->key == key) {
        return nodo;
    }
    
    // Buscar en subárbol izquierdo o derecho
    if (key < nodo->key) {
        return buscarRec(nodo->left, key, ruta);
    } else {
        return buscarRec(nodo->right, key, ruta);
    }
}

// Encontrar mínimo (para eliminación)
Nodo* BST::encontrarMinimo(Nodo* nodo) {
    while (nodo && nodo->left != nullptr) {
        nodo = nodo->left;
    }
    return nodo;
}

// Eliminar clave (método público)
void BST::eliminar(int key) {
    root = eliminarRec(root, key);
}

// Eliminar recursivo
Nodo* BST::eliminarRec(Nodo* nodo, int key) {
    // Caso base: árbol vacío
    if (nodo == nullptr) {
        return nullptr;
    }
    
    // Buscar el nodo a eliminar
    if (key < nodo->key) {
        nodo->left = eliminarRec(nodo->left, key);
    } else if (key > nodo->key) {
        nodo->right = eliminarRec(nodo->right, key);
    } else {
        // Encontramos el nodo a eliminar
        
        // Caso 1: Nodo hoja o con un solo hijo
        if (nodo->left == nullptr) {
            Nodo* temp = nodo->right;
            delete nodo;
            return temp;
        } else if (nodo->right == nullptr) {
            Nodo* temp = nodo->left;
            delete nodo;
            return temp;
        }
        
        // Caso 2: Nodo con dos hijos
        // Encontrar el sucesor inorder (mínimo en subárbol derecho)
        Nodo* temp = encontrarMinimo(nodo->right);
        
        // Copiar el valor del sucesor
        nodo->key = temp->key;
        
        // Eliminar el sucesor
        nodo->right = eliminarRec(nodo->right, temp->key);
    }
    
    return nodo;
}

// Recorrido inorder (método público)
std::vector<int> BST::inorder() {
    std::vector<int> resultado;
    inorderRec(root, resultado);
    return resultado;
}

// Recorrido inorder recursivo
void BST::inorderRec(Nodo* nodo, std::vector<int>& resultado) {
    if (nodo != nullptr) {
        inorderRec(nodo->left, resultado);
        resultado.push_back(nodo->key);
        inorderRec(nodo->right, resultado);
    }
}

// Recorrido preorder (método público)
std::vector<int> BST::preorder() {
    std::vector<int> resultado;
    preorderRec(root, resultado);
    return resultado;
}

// Recorrido preorder recursivo
void BST::preorderRec(Nodo* nodo, std::vector<int>& resultado) {
    if (nodo != nullptr) {
        resultado.push_back(nodo->key);
        preorderRec(nodo->left, resultado);
        preorderRec(nodo->right, resultado);
    }
}

// Recorrido postorder (método público)
std::vector<int> BST::postorder() {
    std::vector<int> resultado;
    postorderRec(root, resultado);
    return resultado;
}

// Recorrido postorder recursivo
void BST::postorderRec(Nodo* nodo, std::vector<int>& resultado) {
    if (nodo != nullptr) {
        postorderRec(nodo->left, resultado);
        postorderRec(nodo->right, resultado);
        resultado.push_back(nodo->key);
    }
}

// Altura del árbol (método público)
int BST::altura() {
    return alturaRec(root);
}

// Altura recursiva
int BST::alturaRec(Nodo* nodo) {
    if (nodo == nullptr) {
        return 0;
    }
    
    int alturaIzq = alturaRec(nodo->left);
    int alturaDer = alturaRec(nodo->right);
    
    return std::max(alturaIzq, alturaDer) + 1;
}

// Número de nodos (método público)
int BST::size() {
    return contarNodosRec(root);
}

// Contar nodos recursivo
int BST::contarNodosRec(Nodo* nodo) {
    if (nodo == nullptr) {
        return 0;
    }
    
    return contarNodosRec(nodo->left) + contarNodosRec(nodo->right) + 1;
}

// Exportar recorrido inorder a archivo
void BST::exportarInorden(const std::string& filename) {
    std::ofstream archivo(filename);
    if (archivo.is_open()) {
        exportarInordenRec(root, archivo);
        archivo.close();
        std::cout << "Recorrido inorder exportado a " << filename << std::endl;
    } else {
        std::cout << "Error al abrir el archivo " << filename << std::endl;
    }
}

// Exportar inorder recursivo
void BST::exportarInordenRec(Nodo* nodo, std::ofstream& archivo) {
    if (nodo != nullptr) {
        exportarInordenRec(nodo->left, archivo);
        archivo << nodo->key << " ";
        exportarInordenRec(nodo->right, archivo);
    }
}

// Cargar árbol desde archivo
void BST::cargarDesdeArchivo(const std::string& filename) {
    std::ifstream archivo(filename);
    if (archivo.is_open()) {
        int valor;
        while (archivo >> valor) {
            insertar(valor);
        }
        archivo.close();
        std::cout << "Arbol cargado desde " << filename << std::endl;
    } else {
        std::cout << "No se pudo abrir el archivo " << filename << std::endl;
    }
}

// Guardar árbol en archivo
void BST::guardarEnArchivo(const std::string& filename) {
    std::ofstream archivo(filename);
    if (archivo.is_open()) {
        std::vector<int> elementos = inorder();
        for (int valor : elementos) {
            archivo << valor << " ";
        }
        archivo.close();
        std::cout << "Arbol guardado en " << filename << std::endl;
    } else {
        std::cout << "Error al abrir el archivo " << filename << std::endl;
    }
}

// Verificar si el árbol está vacío
bool BST::estaVacio() {
    return root == nullptr;
}