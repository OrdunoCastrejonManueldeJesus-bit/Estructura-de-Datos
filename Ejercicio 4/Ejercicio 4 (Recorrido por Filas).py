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

print("\nRecorrido por filas:")
# Recorrido por filas
for fila in range(3):
    contenido_fila = "Fila " + str(fila + 1) + ": "
    for columna in range(3):
        contenido_fila += str(matriz[fila][columna]) + " "
    print(contenido_fila)

print("\nRecorrido completo por filas:")
# Recorrido completo fila por fila
for fila in range(3):
    for columna in range(3):
        print(f"Elemento [{fila}][{columna}] = {matriz[fila][columna]}")