# Declarar e inicializar una matriz 3x3
matriz = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
]

print("Matriz 3x3 original:")
# Mostrar la matriz original
for i in range(3):
    fila = ""
    for j in range(3):
        fila += str(matriz[i][j]) + " "
    print(fila)

print("\nRecorrido por columnas:")
# Recorrido por columnas
for columna in range(3):
    contenido_columna = "Columna " + str(columna + 1) + ": "
    for fila in range(3):
        contenido_columna += str(matriz[fila][columna]) + " "
    print(contenido_columna)

print("\nRecorrido completo por columnas:")
# Recorrido completo columna por columna
for columna in range(3):
    for fila in range(3):
        print(f"Elemento [{fila}][{columna}] = {matriz[fila][columna]}")