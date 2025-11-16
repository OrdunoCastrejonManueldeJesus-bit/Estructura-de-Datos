import random

# Listas enlazadas
class Nodo:
    def __init__(self, valor):
        self.valor = valor
        self.siguiente = None

def main():
    cabeza = None
    actual = None
    
    print("Generando 15 valores aleatorios")
    print(" ")
    
    # Crear 15 nodos con valores aleatorios
    for i in range(15):
        valor_aleatorio = 1 + random.randint(0, 99)  # 1-100
        nuevo_nodo = Nodo(valor_aleatorio)
        
        if cabeza is None:
            cabeza = nuevo_nodo
            actual = cabeza
        else:
            actual.siguiente = nuevo_nodo
            actual = nuevo_nodo
    
    # Mostrar la lista
    print("Lista Enlazada:")
    actual = cabeza
    contador = 1
    while actual is not None:
        print(f"Nodo {contador}: {actual.valor}")
        actual = actual.siguiente
        contador += 1

# Ejecutar el programa
if __name__ == "__main__":
    main()