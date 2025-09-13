import numpy as np
import os

class Conecta4:
    def __init__(self):
        self.filas = 6
        self.columnas = 7
        self.tablero = np.zeros((self.filas, self.columnas), dtype=int)
        self.jugador_actual = 1
        self.game_over = False
        self.ganador = 0
        
    def imprimir_tablero(self):
        """Imprime el tablero de forma legible"""
        os.system('cls' if os.name == 'nt' else 'clear')  # Limpiar pantalla
        
        print("\n  1   2   3   4   5   6   7")
        print("+---+---+---+---+---+---+---+")
        
        for fila in range(self.filas):
            print("|", end="")
            for columna in range(self.columnas):
                if self.tablero[fila][columna] == 0:
                    print("   |", end="")
                elif self.tablero[fila][columna] == 1:
                    print(" X |", end="")
                else:
                    print(" O |", end="")
            print("\n+---+---+---+---+---+---+---+")
    
    def movimiento_valido(self, columna):
        """Verifica si un movimiento es válido"""
        return 0 <= columna < self.columnas and self.tablero[0][columna] == 0
    
    def hacer_movimiento(self, columna):
        """Realiza un movimiento en la columna especificada"""
        if not self.movimiento_valido(columna):
            return False
            
        # Encontrar la primera fila vacía en la columna
        for fila in range(self.filas-1, -1, -1):
            if self.tablero[fila][columna] == 0:
                self.tablero[fila][columna] = self.jugador_actual
                return True
        return False
    
    def verificar_ganador(self):
        """Verifica si hay un ganador"""
        # Verificar horizontal
        for fila in range(self.filas):
            for col in range(self.columnas - 3):
                if (self.tablero[fila][col] == self.jugador_actual and
                    self.tablero[fila][col+1] == self.jugador_actual and
                    self.tablero[fila][col+2] == self.jugador_actual and
                    self.tablero[fila][col+3] == self.jugador_actual):
                    return True
        
        # Verificar vertical
        for fila in range(self.filas - 3):
            for col in range(self.columnas):
                if (self.tablero[fila][col] == self.jugador_actual and
                    self.tablero[fila+1][col] == self.jugador_actual and
                    self.tablero[fila+2][col] == self.jugador_actual and
                    self.tablero[fila+3][col] == self.jugador_actual):
                    return True
        
        # Verificar diagonal (ascendente)
        for fila in range(3, self.filas):
            for col in range(self.columnas - 3):
                if (self.tablero[fila][col] == self.jugador_actual and
                    self.tablero[fila-1][col+1] == self.jugador_actual and
                    self.tablero[fila-2][col+2] == self.jugador_actual and
                    self.tablero[fila-3][col+3] == self.jugador_actual):
                    return True
        
        # Verificar diagonal (descendente)
        for fila in range(self.filas - 3):
            for col in range(self.columnas - 3):
                if (self.tablero[fila][col] == self.jugador_actual and
                    self.tablero[fila+1][col+1] == self.jugador_actual and
                    self.tablero[fila+2][col+2] == self.jugador_actual and
                    self.tablero[fila+3][col+3] == self.jugador_actual):
                    return True
        
        return False
    
    def tablero_lleno(self):
        """Verifica si el tablero está lleno"""
        return np.all(self.tablero[0] != 0)
    
    def cambiar_jugador(self):
        """Cambia al siguiente jugador"""
        self.jugador_actual = 3 - self.jugador_actual  # Cambia entre 1 y 2
    
    def jugar(self):
        """Función principal del juego"""
        print("¡Bienvenido al Conecta 4!")
        print("Jugador 1: X  Jugador 2: N")
        
        while not self.game_over:
            self.imprimir_tablero()
            
            # Mostrar jugador actual
            jugador_str = "X (Jugador 1)" if self.jugador_actual == 1 else "O (Jugador 2)"
            print(f"\nTurno de {jugador_str}")
            
            try:
                # Obtener movimiento del jugador
                columna = int(input("Elige una columna (1-7): ")) - 1
                
                if columna == -1:  # Salir del juego
                    print("¡Hasta luego!")
                    return
                
                if self.hacer_movimiento(columna):
                    # Verificar si hay ganador
                    if self.verificar_ganador():
                        self.imprimir_tablero()
                        print(f"\n¡Felicidades! {jugador_str} ha ganado!")
                        self.game_over = True
                    # Verificar empate
                    elif self.tablero_lleno():
                        self.imprimir_tablero()
                        print("\n¡Empate! El tablero está lleno.")
                        self.game_over = True
                    else:
                        self.cambiar_jugador()
                else:
                    print("Movimiento inválido. Intenta de nuevo.")
                    
            except ValueError:
                print("Por favor, ingresa un número válido (1-7).")
            except KeyboardInterrupt:
                print("\n\n¡Juego terminado!")
                return

# Función para jugar de nuevo
def main():
    while True:
        juego = Conecta4()
        juego.jugar()
        
        jugar_de_nuevo = input("\n¿Quieres jugar de nuevo? (s/n): ").lower()
        if jugar_de_nuevo != 's':
            print("¡Gracias por jugar!")
            break

if __name__ == "__main__":
    main()