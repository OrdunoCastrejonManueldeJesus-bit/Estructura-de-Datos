import random

def bubble_sort(arr):
    n = len(arr)
    swapped = False
    swap_counter = 0
    
    while True:
        swapped = False
        for i in range(1, n):
            if arr[i - 1] > arr[i]:
                # Intercambiar elementos
                arr[i - 1], arr[i] = arr[i], arr[i - 1]
                swapped = True
                swap_counter += 1
        # Reducir el rango en cada iteración ya que el último elemento está en su posición correcta
        n -= 1
        if not swapped:
            break
    
    return swap_counter

def main():
    # Crear un array de 15 elementos
    array = [0] * 15
    
    # Generar 15 valores aleatorios entre 1 y 100
    print("Array original (valores aleatorios):")
    for i in range(len(array)):
        array[i] = random.randint(1, 100)  # Números entre 1 y 100
        print(array[i], end=" ")
    print()
    
    # Aplicar el algoritmo Bubble Sort
    swap_counter = bubble_sort(array)
    
    # Mostrar el array ordenado
    print("\nArray ordenado:")
    for i in range(len(array)):
        print(array[i], end=" ")
    
    print(f"\n\nTotal de intercambios realizados: {swap_counter}")

if __name__ == "__main__":
    main()