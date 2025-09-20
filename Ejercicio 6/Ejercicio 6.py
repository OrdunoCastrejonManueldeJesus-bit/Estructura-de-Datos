import random

def mostrar_array(array):
    for num in array:
        print(num, end=" ")
    print()

def main():
    array = [0] * 15
    
    # Generar números aleatorios
    for i in range(len(array)):
        array[i] = random.randint(1, 100)
    
    print("Array original:")
    mostrar_array(array)
    
    # Ordenamiento por inserción
    for i in range(1, len(array)):
        elemento_actual = array[i]
        j = i - 1
        
        while j >= 0 and array[j] > elemento_actual:
            array[j + 1] = array[j]
            j -= 1
        
        array[j + 1] = elemento_actual
    
    print("Array ordenado:")
    mostrar_array(array)

# Ejecutar el programa
if __name__ == "__main__":
    main()