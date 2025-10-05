import random

def heap_sort(arr):
    n = len(arr)
    
    # Construir el heap máximo
    for i in range(n // 2 - 1, -1, -1):
        heapify(arr, n, i)
    
    # Extraer elementos uno por uno del heap
    for i in range(n - 1, 0, -1):
        arr[0], arr[i] = arr[i], arr[0]  # Intercambiar
        heapify(arr, i, 0)

def heapify(arr, n, i):
    largest = i
    left = 2 * i + 1
    right = 2 * i + 2
    
    if left < n and arr[left] > arr[largest]:
        largest = left
    
    if right < n and arr[right] > arr[largest]:
        largest = right
    
    if largest != i:
        arr[i], arr[largest] = arr[largest], arr[i]  # Intercambiar
        heapify(arr, n, largest)

def print_array(arr):
    print(" ".join(map(str, arr)))

def generar_array_aleatorio(tamaño, min_val, max_val):
    return [random.randint(min_val, max_val) for _ in range(tamaño)]

def main():
    arr = generar_array_aleatorio(15, 1, 100)
    
    print("Array original (15 valores aleatorios):")
    print_array(arr)
    
    heap_sort(arr)
    
    print("\nArray ordenado con Heap Sort:")
    print_array(arr)

if __name__ == "__main__":
    main()