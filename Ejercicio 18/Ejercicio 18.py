# Definimos el tamaño máximo de la pila.
MAX_SIZE = 5

# Lista que almacena los elementos de la pila.
stack = [None] * MAX_SIZE

# Variable 'top' que indica el índice del elemento superior de la pila.
# Inicializamos 'top' a -1 para indicar que la pila está vacía.
top = -1

def is_full():
    """
    @brief Verifica si la pila está completamente llena.
    @return True si la pila está llena, False en caso contrario.
    """
    # Si 'top' es igual al último índice del arreglo (MAX_SIZE - 1), la pila está llena.
    return top == MAX_SIZE - 1

def is_empty():
    """
    @brief Verifica si la pila está vacía.
    @return True si la pila está vacía, False en caso contrario.
    """
    # Si 'top' es igual a -1, la pila está vacía.
    return top == -1

def push(item):
    """
    @brief Inserta un elemento en la cima de la pila (operación PUSH).
    @param item El valor a insertar.
    """
    global top
    if is_full():
        print(f"ERROR: Desbordamiento de pila (Stack Overflow). No se puede insertar {item}.")
    else:
        # 1. Incrementamos 'top' para apuntar a la siguiente posición libre.
        top = top + 1
        # 2. Insertamos el elemento.
        stack[top] = item
        print(f"PUSH: Elemento {item} insertado en la pila.")

def pop():
    """
    @brief Elimina y devuelve el elemento de la cima de la pila (operación POP).
    @return El elemento eliminado, o un valor de error si la pila está vacía.
    """
    global top
    if is_empty():
        print("ERROR: Desbordamiento negativo de pila (Stack Underflow). No hay elementos para eliminar.")
        # Devolvemos el valor entero más bajo posible para indicar un error.
        return float('-inf')
    else:
        # 1. Recuperamos el elemento de la cima.
        popped_item = stack[top]
        # 2. Decrementamos 'top' para "eliminar" el elemento (deja de ser visible).
        top = top - 1
        print(f"POP: Elemento {popped_item} eliminado de la pila.")
        return popped_item

def peek():
    """
    @brief Devuelve el elemento de la cima sin eliminarlo (operación PEEK).
    @return El elemento de la cima, o un valor de error si la pila está vacía.
    """
    if is_empty():
        print("ERROR: La pila está vacía. No hay elementos para ver (Peek).")
        # Devolvemos el valor entero más bajo posible para indicar un error.
        return float('-inf')
    else:
        # Devolvemos el elemento en la posición 'top' sin modificar 'top'.
        return stack[top]

def main():
    """
    @brief Función principal para demostrar el uso de la pila.
    """
    print("--- Demostración de la Pila (Stack) ---")
    
    # Inserción de elementos (PUSH)
    push(10)  # top = 0
    push(20)  # top = 1
    push(30)  # top = 2
    
    # Verificamos si está vacía e imprimimos el elemento superior
    print("\n")
    print("--- Estado Actual ---")
    if is_empty():
        print("La pila está vacía.")
    else:
        print("La pila NO está vacía.")
        print(f"Elemento superior (Peek): {peek()}")  # Debería ser 30
    print(f"Índice de 'top' actual: {top}")  # Debería ser 2

    # Eliminación de elementos (POP)
    print("\n")
    print("--- Operaciones POP ---")
    pop()  # Elimina 30, top = 1
    pop()  # Elimina 20, top = 0
    
    # Verificamos el estado después de las eliminaciones
    print("\n")
    print("--- Estado Final ---")
    print(f"¿Está la pila vacía? (False=No, True=Sí): {is_empty()}")  # Debería ser False (todavía queda 10)
    
    pop()  # Elimina 10, top = -1
    
    print(f"¿Está la pila vacía? (False=No, True=Sí): {is_empty()}")  # Debería ser True
    pop()  # Intento de pop en pila vacía (Underflow)

# Ejecutar la función principal
if __name__ == "__main__":
    main()