import random

def merge_sort(array, left, right):
    """
    Método principal que implementa Merge Sort
    """
    if left < right:
        # Encuentra el punto medio
        middle = (left + right) // 2
        
        # Ordena la primera y segunda mitad
        merge_sort(array, left, middle)
        merge_sort(array, middle + 1, right)
        
        # Combina las mitades ordenadas
        merge(array, left, middle, right)

def merge(array, left, middle, right):
    """
    Método para combinar dos subarrays ordenados
    """
    # Tamaños de los subarrays temporales
    n1 = middle - left + 1
    n2 = right - middle
    
    # Arrays temporales
    left_array = [0] * n1
    right_array = [0] * n2
    
    # Copia datos a los arrays temporales
    for i in range(n1):
        left_array[i] = array[left + i]
    for j in range(n2):
        right_array[j] = array[middle + 1 + j]
    
    # Combina los arrays temporales
    i = j = 0
    k = left
    
    while i < n1 and j < n2:
        if left_array[i] <= right_array[j]:
            array[k] = left_array[i]
            i += 1
        else:
            array[k] = right_array[j]
            j += 1
        k += 1
    
    # Copia los elementos restantes de left_array (si hay)
    while i < n1:
        array[k] = left_array[i]
        i += 1
        k += 1
    
    # Copia los elementos restantes de right_array (si hay)
    while j < n2:
        array[k] = right_array[j]
        j += 1
        k += 1

def generar_array_aleatorio(tamaño, maximo):
    """
    Método para generar números aleatorios
    """
    array = [0] * tamaño
    
    for i in range(tamaño):
        array[i] = random.randint(1, maximo)  # Números entre 1 y maximo
    
    return array

def demostrar_merge_sort():
    """
    Método adicional para demostrar el proceso paso a paso
    """
    demo = [38, 27, 43, 3, 9, 82, 10]
    print("Ejemplo paso a paso con array pequeño:")
    print(f"Array inicial: {demo}")
    
    merge_sort(demo, 0, len(demo) - 1)
    
    print(f"Array final: {demo}")

def main():
    """
    Método principal
    """
    # Generar 15 números aleatorios entre 1 y 100
    numeros = generar_array_aleatorio(15, 100)
    
    print("=== ORDENAMIENTO MERGE SORT ===")
    print("Array original:")
    print(numeros)
    
    # Aplicar Merge Sort
    merge_sort(numeros, 0, len(numeros) - 1)
    
    print("\nArray ordenado:")
    print(numeros)
    
    # Mostrar paso a paso (opcional)
    print("\n=== DEMOSTRACION PASO A PASO ===")
    demostrar_merge_sort()

if __name__ == "__main__":
    main()