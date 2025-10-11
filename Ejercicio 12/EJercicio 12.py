import random

def bucket_sort(arr, n):
    # Crear lista de buckets
    buckets = [[] for _ in range(n)]
    
    # Distribuir los elementos en los buckets
    for i in range(n):
        bucket_index = int(n * arr[i])
        buckets[bucket_index].append(arr[i])
    
    # Ordenar cada bucket individualmente
    for i in range(n):
        buckets[i].sort()
    
    # Combinar todos los buckets en la lista original
    index = 0
    for i in range(n):
        for j in range(len(buckets[i])):
            arr[index] = buckets[i][j]
            index += 1

def generar_aleatorio():
    return random.randint(0, 999) / 1000.0

def imprimir_array(arr, n):
    for i in range(n):
        print(f"{arr[i]:.3f}", end=" ")
    print()

def main():
    random.seed()
    
    TAMANO = 15
    arr = [0.0] * TAMANO
    
    # Generar 15 números aleatorios entre 0 y 1
    print("Generando 15 numeros aleatorios entre 0 y 1")
    for i in range(TAMANO):
        arr[i] = generar_aleatorio()
    
    # Mostrar array original
    print("\nArray original:")
    imprimir_array(arr, TAMANO)
    
    # Aplicar Bucket Sort
    bucket_sort(arr, TAMANO)
    
    # Mostrar array ordenado
    print("\nArray ordenado con Bucket Sort:")
    imprimir_array(arr, TAMANO)

if __name__ == "__main__":
    main()