import random

ARRAY_SIZE = 15
MIN_VAL = 1
MAX_VAL = 1000

# Función para imprimir el arreglo
def print_array(arr):
    print(' '.join(str(val) for val in arr))

# 1. Obtener el valor máximo en el arreglo
def get_max(arr):
    max_val = arr[0]
    for i in range(1, len(arr)):
        if arr[i] > max_val:
            max_val = arr[i]
    return max_val

# 2. Función de Counting Sort 
def count_sort(arr, exp):
    n = len(arr)
    output = [0] * n
    count = [0] * 10

    for i in range(n):
        digit = (arr[i] // exp) % 10
        count[digit] += 1

    for i in range(1, 10):
        count[i] += count[i - 1]

    for i in range(n - 1, -1, -1):
        digit = (arr[i] // exp) % 10
        output[count[digit] - 1] = arr[i]
        count[digit] -= 1

    for i in range(n):
        arr[i] = output[i]

# 3. Función principal de Radix Sort
def radix_sort(arr):
    m = get_max(arr)

    exp = 1
    while m // exp > 0:
        count_sort(arr, exp)
        print(f" -> Arreglo después de la pasada con exp = {exp}: ", end="")
        print_array(arr)
        exp *= 10

# Función principal
def main():
    # 4. Generar el arreglo de 15 valores aleatorios entre 1 y 1000
    data = []
    for i in range(ARRAY_SIZE):
        # Genera números en el rango [1, 1000]
        data.append(random.randint(MIN_VAL, MAX_VAL))

    print("--- Radix Sort (Simplificado) ---")
    print(f"Valores a ordenar: {ARRAY_SIZE}")
    print(f"Rango de valores: [{MIN_VAL}, {MAX_VAL}]")
    
    # Imprimir el arreglo no ordenado
    print("\nArreglo Original: ", end="")
    print_array(data)

    # Llamar al algoritmo Radix Sort
    print("\nIniciando Radix Sort...")
    radix_sort(data)

    # Imprimir el arreglo ordenado final
    print("\nArreglo Final Ordenado: ", end="")
    print_array(data)
    print("-----------------------------------")

# Ejecutar el programa
if __name__ == "__main__":
    main()