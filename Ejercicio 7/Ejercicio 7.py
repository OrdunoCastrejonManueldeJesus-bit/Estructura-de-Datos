import random

def ordenamiento_por_seleccion(array):
    n = len(array)
    
    # repeat (numOfElements - 1) times
    for i in range(n - 1):
        # set the first unsorted element as the minimum
        indice_minimo = i
        
        # for each of the unsorted elements
        for j in range(i + 1, n):
            # if element < currentMinimum
            if array[j] < array[indice_minimo]:
                # set element as new minimum
                indice_minimo = j
        
        # swap minimum with first unsorted position
        if indice_minimo != i:
            array[i], array[indice_minimo] = array[indice_minimo], array[i]

def main():
    # Crear lista con 15 valores aleatorios
    array = []
    
    # Llenar la lista con valores aleatorios entre 1 y 100
    print("Array original:")
    for i in range(15):
        array.append(random.randint(1, 100))
        print(array[i], end=" ")
    print()
    
    # Aplicar ordenamiento por selección
    ordenamiento_por_seleccion(array)
    
    # Mostrar lista ordenada
    print("Array ordenado:")
    for num in array:
        print(num, end=" ")
    print()

if __name__ == "__main__":
    main()