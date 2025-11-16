import random

# Clase del nodo
class Nodo:
    def __init__(self, valor):
        self.dato = valor
        self.siguiente = None
        self.anterior = None

# Clase de Lista Doblemente Enlazada
class ListaDoble:
    def __init__(self):
        self.cabeza = None
        self.cola = None
    
    # Método para insertar al final
    def insertar_final(self, valor):
        nuevo_nodo = Nodo(valor)
        
        if self.cabeza is None:
            # Lista vacía
            self.cabeza = nuevo_nodo
            self.cola = nuevo_nodo
        else:
            # Insertar al final
            self.cola.siguiente = nuevo_nodo
            nuevo_nodo.anterior = self.cola
            self.cola = nuevo_nodo
    
    # Método para mostrar la lista de inicio a fin
    def mostrar_adelante(self):
        if self.cabeza is None:
            print("La lista está vacía.")
            return
        
        actual = self.cabeza
        resultado = "Lista (adelante): "
        while actual is not None:
            resultado += str(actual.dato) + " "
            actual = actual.siguiente
        print(resultado)
    
    # Método para mostrar la lista de fin a inicio
    def mostrar_atras(self):
        if self.cola is None:
            print("La lista está vacía.")
            return
        
        actual = self.cola
        resultado = "Lista (atrás): "
        while actual is not None:
            resultado += str(actual.dato) + " "
            actual = actual.anterior
        print(resultado)
    
    # Método para generar lista con valores aleatorios
    def generar_lista_aleatoria(self, cantidad):
        for i in range(cantidad):
            valor = random.randint(1, 100)  # Números entre 1 y 100
            self.insertar_final(valor)

# Función principal
def main():
    lista = ListaDoble()
    
    print("=== LISTA DOBLEMENTE ENLAZADA CON VALORES ALEATORIOS ===")
    
    # Generar lista con 15 valores aleatorios
    lista.generar_lista_aleatoria(15)
    
    # Mostrar la lista en ambas direcciones
    lista.mostrar_adelante()
    lista.mostrar_atras()

# Ejecutar el programa
if __name__ == "__main__":
    main()