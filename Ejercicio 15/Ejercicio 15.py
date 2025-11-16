import random

# Clase del nodo
class Nodo:
    def __init__(self, val):
        self.valor = val
        self.siguiente = None

# Clase Lista Circular
class ListaCircular:
    def __init__(self):
        self.cabeza = None
        self.cola = None
    
    # Insertar al final de la lista
    def insertar(self, valor):
        nuevo_nodo = Nodo(valor)
        
        if self.cabeza is None:
            # Lista vacía
            self.cabeza = nuevo_nodo
            self.cola = nuevo_nodo
            nuevo_nodo.siguiente = self.cabeza
        else:
            # Insertar al final
            self.cola.siguiente = nuevo_nodo
            self.cola = nuevo_nodo
            self.cola.siguiente = self.cabeza
    
    # Mostrar la lista circular
    def mostrar(self):
        if self.cabeza is None:
            print("Lista vacía")
            return
        
        actual = self.cabeza
        contador = 0
        output = "Lista Circular: "
        
        while True:
            output += str(actual.valor)
            actual = actual.siguiente
            contador += 1
            
            if actual != self.cabeza:
                output += " -> "
            
            # Salto de línea cada 5 elementos para mejor visualización
            if contador % 5 == 0 and actual != self.cabeza:
                output += "\n               "
            
            if actual == self.cabeza:
                break
        
        output += " -> (vuelve al inicio)"
        print(output)
    
    # Verificar si la lista está vacía
    def esta_vacia(self):
        return self.cabeza is None
    
    # Obtener tamaño de la lista
    def obtener_tamano(self):
        if self.cabeza is None:
            return 0
        
        contador = 0
        actual = self.cabeza
        
        while True:
            contador += 1
            actual = actual.siguiente
            if actual == self.cabeza:
                break
        
        return contador

# Función para generar número aleatorio entre 1 y 100
def generar_aleatorio():
    return random.randint(1, 100)

# Función principal
def main():
    # Crear lista circular
    lista = ListaCircular()
    
    print("=== LISTA ENLAZADA CIRCULAR CON 15 VALORES ALEATORIOS ===")
    print("Generando valores aleatorios...")
    print()
    
    # Insertar 15 valores aleatorios
    for i in range(15):
        valor = generar_aleatorio()
        lista.insertar(valor)
        print(f"Insertado: {valor}")
    
    print()
    
    # Mostrar la lista completa
    lista.mostrar()

# Ejecutar el programa
if __name__ == "__main__":
    main()