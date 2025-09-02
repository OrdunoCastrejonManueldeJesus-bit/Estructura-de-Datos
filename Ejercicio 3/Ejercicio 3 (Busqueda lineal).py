arreglo = [12, 23, 57, 45, 34, 67, 89, 98]
valorBuscado = 34

indices = []
for index, elemento in enumerate(arreglo):
    if elemento == valorBuscado:
        indices.append(index)

if len(indices) > 0:
    print("Valor encontrado en los índices:", ", ".join(map(str, indices)))
else:
    print("Valor no encontrado en el arreglo.")