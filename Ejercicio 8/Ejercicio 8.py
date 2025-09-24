import random

class QuickSortSimplificado:
    
    @staticmethod
    def main():
        # Generar 15 valores aleatorios
        array = QuickSortSimplificado.generar_array_aleatorio(15)
        
        print("Array original:")
        QuickSortSimplificado.imprimir_array(array)
        
        # Ordenar con Quick Sort simplificado
        QuickSortSimplificado.quick_sort(array, 0, len(array) - 1)
        
        print("\nArray ordenado:")
        QuickSortSimplificado.imprimir_array(array)
    
    # Generar array con valores aleatorios entre 1 y 100
    @staticmethod
    def generar_array_aleatorio(tamaño):
        array = [0] * tamaño
        
        for i in range(tamaño):
            array[i] = random.randint(1, 100)  # Valores entre 1 y 100
        
        return array
    
    # Algoritmo Quick Sort simplificado
    @staticmethod
    def quick_sort(array, izquierda, derecha):
        if izquierda < derecha:
            indice_pivote = QuickSortSimplificado.particion(array, izquierda, derecha)
            
            # Ordenar recursivamente las dos particiones
            QuickSortSimplificado.quick_sort(array, izquierda, indice_pivote - 1)
            QuickSortSimplificado.quick_sort(array, indice_pivote + 1, derecha)
    
    # Función de partición según la descripción
    @staticmethod
    def particion(array, izquierda, derecha):
        # El primer elemento como pivote
        pivote = array[izquierda]
        indice_pivote = izquierda
        store_index = izquierda + 1
        
        for i in range(izquierda + 1, derecha + 1):
            debe_intercambiar = False
            
            # Verificar si debe intercambiar según las condiciones
            if array[i] < pivote:
                debe_intercambiar = True
            elif array[i] == pivote:
                # 50% de probabilidad cuando son iguales
                if random.random() < 0.5:
                    debe_intercambiar = True
            
            if debe_intercambiar:
                QuickSortSimplificado.intercambiar(array, i, store_index)
                store_index += 1
        
        # Colocar el pivote en su posición final
        QuickSortSimplificado.intercambiar(array, indice_pivote, store_index - 1)
        
        return store_index - 1
    
    # Función auxiliar para intercambiar elementos
    @staticmethod
    def intercambiar(array, i, j):
        temp = array[i]
        array[i] = array[j]
        array[j] = temp
    
    # Función para imprimir el array
    @staticmethod
    def imprimir_array(array):
        for i in range(len(array)):
            print(array[i], end="")
            if i < len(array) - 1:
                print(", ", end="")
        print()

# Ejecutar el programa
if __name__ == "__main__":
    QuickSortSimplificado.main()