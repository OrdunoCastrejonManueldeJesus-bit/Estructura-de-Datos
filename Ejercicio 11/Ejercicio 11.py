import random

class HashSortSimplificado:
    
    @staticmethod
    def main():
        valores = HashSortSimplificado.generar_valores_aleatorios(15, 100)
        
        print("Valores originales:")
        print(HashSortSimplificado.list_to_string(valores))
        valores_ordenados = HashSortSimplificado.hash_sort_simplificado(valores)
        
        print("\nValores ordenados:")
        print(HashSortSimplificado.list_to_string(valores_ordenados))
    
    @staticmethod
    def list_to_string(lst):
        return "[" + ", ".join(str(x) for x in lst) + "]"

    @staticmethod
    def generar_valores_aleatorios(cantidad, max_val):
        valores = [0] * cantidad
        
        for i in range(cantidad):
            valores[i] = random.randint(0, max_val - 1)
        
        return valores

    @staticmethod
    def hash_sort_simplificado(valores):
        if len(valores) == 0:
            return []
        
        max_val = HashSortSimplificado.encontrar_maximo(valores)
        
        tabla_hash = [False] * (max_val + 1)
        
        for valor in valores:
            if 0 <= valor < len(tabla_hash):
                tabla_hash[valor] = True
        
        ordenados = []
        for i in range(len(tabla_hash)):
            if tabla_hash[i]:
                ordenados.append(i)
        
        return ordenados
    
    @staticmethod
    def encontrar_maximo(valores):
        max_val = valores[0]
        for i in range(1, len(valores)):
            if valores[i] > max_val:
                max_val = valores[i]
        return max_val
    
    @staticmethod
    def hash_sort_con_tree_set(valores):
        sorted_set = sorted(set(valores))
        
        return list(sorted_set)

# Ejecutar el programa
if __name__ == "__main__":
    HashSortSimplificado.main()