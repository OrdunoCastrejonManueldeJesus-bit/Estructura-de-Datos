import random

# Clase del nodo
class Nodo:
    def __init__(self, v):
        self.valor = v
        self.siguiente = None
        self.anterior = None

# Clase de Lista Circular Doble
class ListaCircularDoble:
    def __init__(self):
        self.cabeza = None
        self.tamaño = 0
    
    # Insertar al final
    def insertar_final(self, valor):
        nuevo_nodo = Nodo(valor)
        
        if self.cabeza is None:
            self.cabeza = nuevo_nodo
            self.cabeza.siguiente = self.cabeza
            self.cabeza.anterior = self.cabeza
        else:
            ultimo = self.cabeza.anterior
            
            ultimo.siguiente = nuevo_nodo
            nuevo_nodo.anterior = ultimo
            nuevo_nodo.siguiente = self.cabeza
            self.cabeza.anterior = nuevo_nodo
        
        self.tamaño += 1
    
    # Mostrar lista en sentido horario
    def mostrar_horario(self):
        if self.cabeza is None:
            print("Lista vacía")
            return
        
        print("Lista en sentido horario: ", end="")
        actual = self.cabeza
        
        while True:
            print(actual.valor, end=" ")
            actual = actual.siguiente
            if actual == self.cabeza:
                break
        print()
    
    # Mostrar lista en sentido antihorario
    def mostrar_antihorario(self):
        if self.cabeza is None:
            print("Lista vacía")
            return
        
        print("Lista en sentido antihorario: ", end="")
        actual = self.cabeza.anterior
        
        while True:
            print(actual.valor, end=" ")
            actual = actual.anterior
            if actual == self.cabeza.anterior:
                break
        print()
    
    # Obtener tamaño
    def get_tamaño(self):
        return self.tamaño
    
    # Generar lista con valores aleatorios
    def generar_lista_aleatoria(self, cantidad):
        for _ in range(cantidad):
            valor = random.randint(1, 100)  # Valores entre 1 y 100
            self.insertar_final(valor)

# Función principal
def main():
    lista = ListaCircularDoble()
    
    print("=== LISTA ENLAZADA CIRCULAR DOBLE ===")
    print("Generando 15 valores aleatorios")
    print()
    
    # Generar lista con 15 valores aleatorios
    lista.generar_lista_aleatoria(15)
    
    # Mostrar resultados
    print(f"Tamaño de la lista: {lista.get_tamaño()}")
    
    lista.mostrar_horario()
    lista.mostrar_antihorario()

# Versión alternativa con formato de cadena
class ListaCircularDobleAlternativa:
    def __init__(self):
        self.cabeza = None
        self.tamaño = 0
    
    # Insertar al final (igual que antes)
    def insertar_final(self, valor):
        nuevo_nodo = Nodo(valor)
        
        if self.cabeza is None:
            self.cabeza = nuevo_nodo
            self.cabeza.siguiente = self.cabeza
            self.cabeza.anterior = self.cabeza
        else:
            ultimo = self.cabeza.anterior
            
            ultimo.siguiente = nuevo_nodo
            nuevo_nodo.anterior = ultimo
            nuevo_nodo.siguiente = self.cabeza
            self.cabeza.anterior = nuevo_nodo
        
        self.tamaño += 1
    
    # Mostrar lista en sentido horario (versión alternativa)
    def mostrar_horario(self):
        if self.cabeza is None:
            print("Lista vacía")
            return
        
        valores = []
        actual = self.cabeza
        
        while True:
            valores.append(str(actual.valor))
            actual = actual.siguiente
            if actual == self.cabeza:
                break
        
        print("Lista en sentido horario:", " ".join(valores))
    
    # Mostrar lista en sentido antihorario (versión alternativa)
    def mostrar_antihorario(self):
        if self.cabeza is None:
            print("Lista vacía")
            return
        
        valores = []
        actual = self.cabeza.anterior
        
        while True:
            valores.append(str(actual.valor))
            actual = actual.anterior
            if actual == self.cabeza.anterior:
                break
        
        print("Lista en sentido antihorario:", " ".join(valores))
    
    # Obtener tamaño
    def get_tamaño(self):
        return self.tamaño
    
    # Generar lista con valores aleatorios
    def generar_lista_aleatoria(self, cantidad):
        for _ in range(cantidad):
            valor = random.randint(1, 100)
            self.insertar_final(valor)

if __name__ == "__main__":
    main()