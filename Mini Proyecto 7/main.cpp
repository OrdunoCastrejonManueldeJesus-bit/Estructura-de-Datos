#include "bst.h"
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

// Función para mostrar el menú de ayuda
void mostrarAyuda() {
    std::cout << "\n=== COMANDOS DISPONIBLES ===\n";
    std::cout << "insert <numero>     - Insertar un numero en el arbol\n";
    std::cout << "search <numero>     - Buscar un numero y mostrar su ruta\n";
    std::cout << "delete <numero>     - Eliminar un numero del arbol\n";
    std::cout << "inorder             - Mostrar recorrido inorder\n";
    std::cout << "preorder            - Mostrar recorrido preorder\n";
    std::cout << "postorder           - Mostrar recorrido postorder\n";
    std::cout << "height              - Mostrar altura del arbol\n";
    std::cout << "size                - Mostrar numero de nodos\n";
    std::cout << "export <archivo>    - Exportar recorrido inorder a archivo\n";
    std::cout << "load <archivo>      - Cargar numeros desde archivo\n";
    std::cout << "save <archivo>      - Guardar arbol en archivo\n";
    std::cout << "clear               - Vaciar el arbol\n";
    std::cout << "help                - Mostrar este mensaje\n";
    std::cout << "exit                - Salir del programa\n";
    std::cout << "============================\n";
}

// Función principal
int main() {
    BST arbol;
    std::string comando;
    std::string argumento;
    
    std::cout << "=== ARBOL BINARIO DE BUSQUEDA (BST) ===\n";
    std::cout << "Escribe 'help' para ver los comandos disponibles\n";
    
    while (true) {
        std::cout << "\n> ";
        
        // Leer entrada del usuario
        std::string entrada;
        std::getline(std::cin, entrada);
        
        // Separar comando y argumento
        std::stringstream ss(entrada);
        ss >> comando;
        ss >> argumento;
        
        // Procesar comando
        if (comando == "insert" || comando == "i") {
            if (argumento.empty()) {
                std::cout << "Uso: insert <numero>\n";
            } else {
                try {
                    int valor = std::stoi(argumento);
                    arbol.insertar(valor);
                    std::cout << "Numero " << valor << " insertado.\n";
                } catch (...) {
                    std::cout << "Error: Entrada no valida.\n";
                }
            }
        }
        else if (comando == "search" || comando == "s") {
            if (argumento.empty()) {
                std::cout << "Uso: search <numero>\n";
            } else {
                try {
                    int valor = std::stoi(argumento);
                    std::vector<int> ruta;
                    bool encontrado = arbol.buscar(valor, ruta);
                    
                    if (encontrado) {
                        std::cout << "Numero " << valor << " encontrado.\n";
                        std::cout << "Ruta: ";
                        for (size_t i = 0; i < ruta.size(); i++) {
                            std::cout << ruta[i];
                            if (i < ruta.size() - 1) {
                                std::cout << " -> ";
                            }
                        }
                        std::cout << std::endl;
                    } else {
                        std::cout << "Numero " << valor << " no encontrado.\n";
                    }
                } catch (...) {
                    std::cout << "Error: Entrada no valida.\n";
                }
            }
        }
        else if (comando == "delete" || comando == "d") {
            if (argumento.empty()) {
                std::cout << "Uso: delete <numero>\n";
            } else {
                try {
                    int valor = std::stoi(argumento);
                    arbol.eliminar(valor);
                    std::cout << "Numero " << valor << " eliminado.\n";
                } catch (...) {
                    std::cout << "Error: Entrada no valida.\n";
                }
            }
        }
        else if (comando == "inorder" || comando == "in") {
            std::vector<int> resultado = arbol.inorder();
            if (resultado.empty()) {
                std::cout << "El arbol esta vacio.\n";
            } else {
                std::cout << "Recorrido Inorder: ";
                for (int valor : resultado) {
                    std::cout << valor << " ";
                }
                std::cout << std::endl;
            }
        }
        else if (comando == "preorder" || comando == "pre") {
            std::vector<int> resultado = arbol.preorder();
            if (resultado.empty()) {
                std::cout << "El arbol esta vacio.\n";
            } else {
                std::cout << "Recorrido Preorder: ";
                for (int valor : resultado) {
                    std::cout << valor << " ";
                }
                std::cout << std::endl;
            }
        }
        else if (comando == "postorder" || comando == "post") {
            std::vector<int> resultado = arbol.postorder();
            if (resultado.empty()) {
                std::cout << "El arbol esta vacio.\n";
            } else {
                std::cout << "Recorrido Postorder: ";
                for (int valor : resultado) {
                    std::cout << valor << " ";
                }
                std::cout << std::endl;
            }
        }
        else if (comando == "height" || comando == "h") {
            int altura = arbol.altura();
            std::cout << "Altura del arbol: " << altura << std::endl;
        }
        else if (comando == "size" || comando == "sz") {
            int tamaño = arbol.size();
            std::cout << "Numero de nodos: " << tamaño << std::endl;
        }
        else if (comando == "export" || comando == "exp") {
            if (argumento.empty()) {
                std::cout << "Uso: export <nombre_archivo>\n";
            } else {
                arbol.exportarInorden(argumento);
            }
        }
        else if (comando == "load" || comando == "ld") {
            if (argumento.empty()) {
                std::cout << "Uso: load <nombre_archivo>\n";
            } else {
                arbol.cargarDesdeArchivo(argumento);
            }
        }
        else if (comando == "save" || comando == "sv") {
            if (argumento.empty()) {
                std::cout << "Uso: save <nombre_archivo>\n";
            } else {
                arbol.guardarEnArchivo(argumento);
            }
        }
        else if (comando == "clear" || comando == "c") {
            // Crear nuevo árbol (el destructor limpia el anterior)
            BST nuevoArbol;
            arbol = nuevoArbol;
            std::cout << "Arbol vaciado.\n";
        }
        else if (comando == "help" || comando == "?") {
            mostrarAyuda();
        }
        else if (comando == "exit" || comando == "quit" || comando == "q") {
            std::cout << "Saliendo del programa...\n";
            break;
        }
        else if (!comando.empty()) {
            std::cout << "Comando no reconocido. Escribe 'help' para ver comandos disponibles.\n";
        }
        
        // Limpiar para siguiente iteración
        comando.clear();
        argumento.clear();
    }
    
    return 0;
}