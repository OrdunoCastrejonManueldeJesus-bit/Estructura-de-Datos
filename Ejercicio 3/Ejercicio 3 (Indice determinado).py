numeros = [10, 20, 30, 40, 50]
valorAInsertar = 75
indice = 2

if 0 <= indice <= len(numeros):
    numeros.insert(indice, valorAInsertar)
else:
    print("Índice fuera de rango.")

# Mostrar el resultado (forma más simple)
print("Lista actualizada:", numeros)