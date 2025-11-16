# Clase Nodo para la lista enlazada
class Nodo:
    def __init__(self, valor):
        self.dato = valor
        self.siguiente = None

# Clase Pila implementada con lista enlazada
class Pila:
    """
    @brief Constructor de la pila
    @param cap Capacidad máxima de la pila
    """
    def __init__(self, cap):
        self.top = None       # Referencia al tope de la pila
        self.capacidad = cap  # Capacidad máxima de la pila
        self.tamaño = 0       # Tamaño actual de la pila
        
        print("\n")
        print(f"Pila creada con capacidad: {self.capacidad}")

    """
    @brief Verifica si la pila está completamente llena
    @return true si la pila está llena, false en caso contrario
    """
    def is_full(self):
        return self.tamaño == self.capacidad

    """
    @brief Verifica si la pila está vacía
    @return true si la pila está vacía, false en caso contrario
    """
    def is_empty(self):
        return self.top is None

    """
    @brief Inserta un elemento en la cima de la pila (operación PUSH)
    @param valor El valor a insertar
    """
    def push(self, valor):
        if self.is_full():
            print(f"ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar '{valor}'.")
        else:
            # Crear nuevo nodo
            nuevo_nodo = Nodo(valor)
            
            # Insertar al inicio (tope de la pila)
            nuevo_nodo.siguiente = self.top
            self.top = nuevo_nodo
            self.tamaño += 1
            
            print(f"PUSH: Elemento '{valor}' insertado en la pila.")

    """
    @brief Elimina y devuelve el elemento de la cima de la pila (operación POP)
    @return El elemento eliminado, o cadena vacía si la pila está vacía
    """
    def pop(self):
        if self.is_empty():
            print("ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar.")
            return ""  # Cadena vacía para indicar error
        else:
            # Recuperar el elemento del tope
            valor_eliminado = self.top.dato
            
            # Eliminar el nodo del tope
            temp = self.top
            self.top = self.top.siguiente
            # En Python el garbage collector se encarga de liberar la memoria
            self.tamaño -= 1
            
            print(f"POP: Elemento '{valor_eliminado}' eliminado de la pila.")
            return valor_eliminado

    """
    @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK)
    @return El elemento de la cima, o cadena vacía si la pila está vacía
    """
    def peek(self):
        if self.is_empty():
            print("ERROR: La pila está vacía. No hay elementos para ver (Peek).")
            return ""  # Cadena vacía para indicar error
        else:
            return self.top.dato

    """
    @brief Muestra todos los elementos de la pila
    """
    def mostrar(self):
        if self.is_empty():
            print("La pila está vacía.")
        else:
            print("Elementos en la pila (desde el tope):")
            actual = self.top
            posicion = 1
            
            while actual is not None:
                print(f"{posicion}. {actual.dato}")
                actual = actual.siguiente
                posicion += 1

    """
    @brief Obtiene el tamaño actual de la pila
    @return Número de elementos en la pila
    """
    def get_tamaño(self):
        return self.tamaño

    """
    @brief Obtiene la capacidad máxima de la pila
    @return Capacidad máxima
    """
    def get_capacidad(self):
        return self.capacidad

"""
@brief Función para mostrar el menú de opciones
"""
def mostrar_menu():
    print("\n---- MENU DE OPERACIONES ---")
    print("1. PUSH (Insertar elemento)")
    print("2. POP (Eliminar elemento)")
    print("3. PEEK (Ver elemento superior)")
    print("4. MOSTRAR (Mostrar todos los elementos)")
    print("5. ESTADO (Ver estado de la pila)")
    print("6. SALIR")
    print("-----------------------------")
    print("\n")

"""
@brief Función principal
"""
def main():
    print("--- Pila con Lista Enlazada Simple ---")
    capacidad_input = input("Ingrese la capacidad de la pila: ")
    capacidad = int(capacidad_input)
    
    # Crear la pila
    mi_pila = Pila(capacidad)
    
    while True:
        mostrar_menu()
        opcion_input = input("Seleccione una opción: ")
        opcion = int(opcion_input)
        
        if opcion == 1:  # PUSH
            print("\n")
            valor = input("Ingrese el valor a insertar: ")
            mi_pila.push(valor)
            print("\n")
            
        elif opcion == 2:  # POP
            print("\n")
            mi_pila.pop()
            
        elif opcion == 3:  # PEEK
            print("\n")
            tope = mi_pila.peek()
            if tope is not None and tope != "":
                print(f"Elemento en el tope: {tope}")
            print("\n")
            
        elif opcion == 4:  # MOSTRAR
            print("\n")
            mi_pila.mostrar()
            
        elif opcion == 5:  # ESTADO
            print("\n--- ESTADO DE LA PILA ---")
            print(f"Capacidad máxima: {mi_pila.get_capacidad()}")
            print(f"Elementos actuales: {mi_pila.get_tamaño()}")
            print(f"¿Está vacía? {'Sí' if mi_pila.is_empty() else 'No'}")
            print(f"¿Está llena? {'Sí' if mi_pila.is_full() else 'No'}")
            print("\n")
            
        elif opcion == 6:  # SALIR
            print("\n")
            print("Saliendo del programa...")
            break
            
        else:
            print("Opción no válida. Intente nuevamente.")

# Ejecutar el programa
if __name__ == "__main__":
    main()