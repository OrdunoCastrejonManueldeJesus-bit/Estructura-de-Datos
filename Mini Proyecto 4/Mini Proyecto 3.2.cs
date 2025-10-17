using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CazadorDeTesoros
{
    public class CazadorDeTesoros
    {
        // Constantes
        private const int ANCHO = 21;
        private const int ALTO = 21;
        private const int NIVELES = 3;
        
        // Códigos para los elementos del tablero
        public const int VACIO = 0;
        public const int PARED = 1;
        public const int TESORO = 2;
        public const int TRAMPA = 3;
        public const int SALIDA = 4;
        public const int PERSONAJE = 5;
        
        private int[,,] tablero;
        private int posX, posY, posZ; // Posición del personaje
        private bool[,,] descubierto; // Qué celdas se han revelado
        
        // Estadísticas del jugador
        private int vidas;
        private int energia;
        private int puntaje;
        private int movimientosSinChocar; // Contador para recuperar energía
        private List<int> inventario; // Inventario de tesoros (valores en puntos)
        private const int CAPACIDAD_INVENTARIO = 5;
        
        // Sistema de ranking
        private List<Jugador> ranking;
        private const string ARCHIVO_RANKING = "ranking.txt";
        private const int MAX_RANKING = 10;
        
        private Random random;
        
        public CazadorDeTesoros()
        {
            this.tablero = new int[ANCHO, ALTO, NIVELES];
            this.descubierto = new bool[ANCHO, ALTO, NIVELES];
            this.vidas = 3;
            this.energia = 4;
            this.puntaje = 0;
            this.movimientosSinChocar = 0;
            this.inventario = new List<int>();
            this.ranking = new List<Jugador>();
            this.random = new Random();
            
            CargarRanking();
            InicializarTablero();
        }
        
        // Clase para el ranking
        private class Jugador
        {
            public string Nombre { get; set; }
            public int Puntaje { get; set; }
            
            public Jugador(string nombre, int puntaje)
            {
                Nombre = nombre;
                Puntaje = puntaje;
            }
        }
        
        private void CargarRanking()
        {
            if (File.Exists(ARCHIVO_RANKING))
            {
                try
                {
                    string[] lineas = File.ReadAllLines(ARCHIVO_RANKING);
                    foreach (string linea in lineas)
                    {
                        string[] partes = linea.Split(':');
                        if (partes.Length == 2)
                        {
                            string nombre = partes[0].Trim();
                            int puntaje = int.Parse(partes[1].Trim());
                            ranking.Add(new Jugador(nombre, puntaje));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar ranking: {ex.Message}");
                }
            }
        }
        
        private void GuardarRanking()
        {
            try
            {
                var lineas = ranking.Select(j => $"{j.Nombre}:{j.Puntaje}");
                File.WriteAllLines(ARCHIVO_RANKING, lineas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar ranking: {ex.Message}");
            }
        }
        
        private void PreguntarGuardarPuntaje()
        {
            Console.WriteLine($"\n¿Deseas guardar tu puntaje de {puntaje} puntos en el ranking? (S/N):");
            string respuesta = Console.ReadLine().ToUpper();
            
            if (respuesta == "S" || respuesta == "SI")
            {
                AgregarAlRanking();
            }
            else
            {
                Console.WriteLine("Puntaje no guardado.");
            }
        }
        
        private void AgregarAlRanking()
        {
            if (puntaje > 0)
            {
                Console.WriteLine($"\nPuntaje: {puntaje}");
                
                string nombre = "";
                while (true)
                {
                    Console.Write("Ingresa tu nombre (máximo 3 palabras, ejemplo: M23): ");
                    nombre = Console.ReadLine().Trim();
                    
                    if (EsNombreValido(nombre))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nombre inválido. Debe tener máximo 3 palabras (solo letras y números).");
                    }
                }
                
                ranking.Add(new Jugador(nombre, puntaje));
                ranking = ranking.OrderByDescending(j => j.Puntaje).Take(MAX_RANKING).ToList();
                GuardarRanking();
                
                Console.WriteLine("\n¡Puntaje guardado exitosamente!");
                MostrarRanking();
            }
        }
        
        private bool EsNombreValido(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return false;
            
            string[] palabras = nombre.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (palabras.Length > 3) return false;
            
            foreach (string palabra in palabras)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(palabra, @"^[a-zA-Z0-9]+$"))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public void MostrarRanking()
        {
            Console.WriteLine("\n=== MEJORES PUNTAJES ===");
            if (ranking.Count == 0)
            {
                Console.WriteLine("No hay puntajes registrados.");
                return;
            }
            
            for (int i = 0; i < ranking.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ranking[i].Nombre} - {ranking[i].Puntaje} puntos");
            }
            Console.WriteLine();
        }
        
        public void InicializarTablero()
        {
            // Inicializar todo como vacío y oscuro
            for (int z = 0; z < NIVELES; z++)
            {
                for (int x = 0; x < ANCHO; x++)
                {
                    for (int y = 0; y < ALTO; y++)
                    {
                        tablero[x, y, z] = VACIO;
                        descubierto[x, y, z] = false;
                    }
                }
                GenerarNivel(z);
            }
            
            // Colocar personaje en el nivel 0, posición central
            posX = ANCHO / 2;
            posY = ALTO / 2;
            posZ = 0;
            tablero[posX, posY, posZ] = PERSONAJE;
            DescubrirAreaAlrededor(posX, posY, posZ);
        }
        
        private void GenerarNivel(int nivel)
        {
            // 1. Crear paredes alrededor
            for (int i = 0; i < ANCHO; i++)
            {
                for (int j = 0; j < ALTO; j++)
                {
                    if (i == 0 || i == ANCHO-1 || j == 0 || j == ALTO-1)
                    {
                        tablero[i, j, nivel] = PARED;
                    }
                }
            }
            
            // 2. Agregar paredes internas (mínimo 15 + nivel)
            int paredesInternas = 15 + nivel;
            for (int i = 0; i < paredesInternas; i++)
            {
                int x = random.Next(1, ANCHO-1);
                int y = random.Next(1, ALTO-1);
                if (tablero[x, y, nivel] == VACIO)
                {
                    tablero[x, y, nivel] = PARED;
                }
            }
            
            // 3. Colocar tesoros según el nivel
            int tesoros = 0;
            switch (nivel)
            {
                case 0: tesoros = 5; break;  // Nivel 1: 5 tesoros
                case 1: tesoros = 10; break; // Nivel 2: 10 tesoros
                case 2: tesoros = 15; break; // Nivel 3: 15 tesoros
            }
            
            for (int i = 0; i < tesoros; i++)
            {
                ColocarElementoAleatorio(TESORO, nivel);
            }
            
            // 4. Colocar trampas según el nivel
            int trampas = 0;
            switch (nivel)
            {
                case 0: trampas = 5; break;  // Nivel 1: 5 trampas
                case 1: trampas = 10; break; // Nivel 2: 10 trampas
                case 2: trampas = 15; break; // Nivel 3: 15 trampas
            }
            
            for (int i = 0; i < trampas; i++)
            {
                ColocarElementoAleatorio(TRAMPA, nivel);
            }
            
            // 5. Colocar salida en posición aleatoria
            ColocarElementoAleatorio(SALIDA, nivel);
        }
        
        private void ColocarElementoAleatorio(int elemento, int nivel)
        {
            int x, y;
            do
            {
                x = random.Next(1, ANCHO-1);
                y = random.Next(1, ALTO-1);
            } while (tablero[x, y, nivel] != VACIO);
            
            tablero[x, y, nivel] = elemento;
        }
        
        public void ImprimirTablero()
        {
            Console.Clear();
            Console.WriteLine("=== CAZADOR DE TESOROS ===");
            Console.WriteLine($"Nivel: {posZ + 1} | Vidas: {vidas} | Energía: {energia} | Puntaje: {puntaje}");
            Console.WriteLine($"Inventario: {inventario.Count}/{CAPACIDAD_INVENTARIO} tesoros");
            MostrarInventario();
            Console.WriteLine();
            
            for (int y = 0; y < ALTO; y++)
            {
                for (int x = 0; x < ANCHO; x++)
                {
                    char simbolo;
                    if (!descubierto[x, y, posZ])
                    {
                        simbolo = '?'; // Área oscura
                    }
                    else
                    {
                        switch (tablero[x, y, posZ])
                        {
                            case VACIO: simbolo = ' '; break;
                            case PARED: simbolo = '█'; break;
                            case TESORO: simbolo = 'T'; break;
                            case TRAMPA: simbolo = 'X'; break;
                            case SALIDA: simbolo = 'S'; break;
                            case PERSONAJE: simbolo = 'P'; break;
                            default: simbolo = '?'; break;
                        }
                    }
                    Console.Write(simbolo + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nControles: W (Arriba), S (Abajo), A (Izquierda), D (Derecha)");
            Console.WriteLine("I (Ver inventario), T (Tirar tesoro), R (Ver ranking), Q (Salir)");
        }
        
        private void MostrarInventario()
        {
            if (inventario.Count > 0)
            {
                Console.Write("Tesoros: ");
                for (int i = 0; i < inventario.Count; i++)
                {
                    Console.Write($"[{i + 1}:{inventario[i]}p] ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Inventario vacío");
            }
        }
        
        private void GestionarInventario()
        {
            Console.Clear();
            Console.WriteLine("=== INVENTARIO ===");
            MostrarInventario();
            Console.WriteLine($"\nCapacidad: {inventario.Count}/{CAPACIDAD_INVENTARIO}");
            
            if (inventario.Count > 0)
            {
                Console.WriteLine("\nPresiona el número del tesoro para tirarlo (1-5) o Enter para continuar:");
                string input = Console.ReadLine();
                
                if (int.TryParse(input, out int indice) && indice >= 1 && indice <= inventario.Count)
                {
                    int tesoroEliminado = inventario[indice - 1];
                    inventario.RemoveAt(indice - 1);
                    Console.WriteLine($"Tiraste un tesoro de {tesoroEliminado} puntos");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\nInventario vacío. Presiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
        
        private void DescubrirAreaAlrededor(int x, int y, int z)
        {
            // CAMBIO: Rango de visión en forma de cruz (posición actual + 4 direcciones)
            // Revelar posición actual
            descubierto[x, y, z] = true;
            
            // Revelar arriba
            if (y - 1 >= 0) descubierto[x, y - 1, z] = true;
            
            // Revelar abajo
            if (y + 1 < ALTO) descubierto[x, y + 1, z] = true;
            
            // Revelar izquierda
            if (x - 1 >= 0) descubierto[x - 1, y, z] = true;
            
            // Revelar derecha
            if (x + 1 < ANCHO) descubierto[x + 1, y, z] = true;
        }
        
        public bool MoverPersonaje(int dx, int dy)
        {
            int nuevoX = posX + dx;
            int nuevoY = posY + dy;
            
            // Verificar límites del tablero
            if (nuevoX < 0 || nuevoX >= ANCHO || nuevoY < 0 || nuevoY >= ALTO)
            {
                return false;
            }
            
            // Verificar si hay pared
            if (tablero[nuevoX, nuevoY, posZ] == PARED)
            {
                energia--;
                movimientosSinChocar = 0; // Reiniciar contador al chocar
                Console.WriteLine($"¡Chocaste con una pared! Energía: {energia}");
                if (energia <= 0)
                {
                    PerderVida();
                }
                return false;
            }
            
            // Movimiento exitoso - incrementar contador
            movimientosSinChocar++;
            
            // Verificar si se gana energía (cada 3 movimientos sin chocar)
            if (movimientosSinChocar >= 3 && energia < 4)
            {
                energia++;
                movimientosSinChocar = 0; // Reiniciar contador
                Console.WriteLine($"¡Recuperaste energía! Energía: {energia}");
            }
            
            // Mover personaje
            tablero[posX, posY, posZ] = VACIO;
            posX = nuevoX;
            posY = nuevoY;
            
            // Procesar la celda a la que se movió
            ProcesarCelda(nuevoX, nuevoY);
            
            tablero[posX, posY, posZ] = PERSONAJE;
            DescubrirAreaAlrededor(posX, posY, posZ);
            
            return true;
        }
        
        private void ProcesarCelda(int x, int y)
        {
            int celda = tablero[x, y, posZ];
            
            switch (celda)
            {
                case TESORO:
                    AbrirTesoro(x, y);
                    break;
                case TRAMPA:
                    ActivarTrampa(x, y);
                    break;
                case SALIDA:
                    UsarSalida();
                    break;
            }
        }
        
        private void AbrirTesoro(int x, int y)
        {
            Console.WriteLine("¡Encontraste un tesoro!");
            
            // Valor del tesoro basado en el nivel
            int puntos = 0;
            switch (posZ)
            {
                case 0: puntos = 100; break;   // Nivel 1: 100 puntos
                case 1: puntos = 500; break;   // Nivel 2: 500 puntos
                case 2: puntos = 1000; break;  // Nivel 3: 1000 puntos
            }
            
            if (inventario.Count < CAPACIDAD_INVENTARIO)
            {
                inventario.Add(puntos);
                Console.WriteLine($"¡Guardaste un tesoro de {puntos} puntos en tu inventario!");
                Console.WriteLine($"Inventario: {inventario.Count}/{CAPACIDAD_INVENTARIO}");
            }
            else
            {
                Console.WriteLine($"¡Inventario lleno! No puedes guardar este tesoro de {puntos} puntos.");
                Console.WriteLine("Usa 'T' para tirar un tesoro del inventario.");
            }
            
            tablero[x, y, posZ] = VACIO;
        }
        
        private void ActivarTrampa(int x, int y)
        {
            vidas--;
            Console.WriteLine($"¡Caíste en una trampa! Vidas: {vidas}");
            if (vidas <= 0)
            {
                GameOver();
            }
            tablero[x, y, posZ] = VACIO;
        }
        
        private void UsarSalida()
        {
            if (posZ < NIVELES - 1)
            {
                // Los tesoros se mantienen en el inventario para los siguientes niveles
                
                posZ++;
                Console.WriteLine($"¡Pasaste al nivel {posZ + 1}!");
                
                // Recompensa por completar nivel
                int recompensa = 250 + (posZ * 250);
                puntaje += recompensa;
                Console.WriteLine($"¡Recompensa por completar nivel: {recompensa} puntos!");
                
                // Reposicionar personaje en el nuevo nivel
                tablero[posX, posY, posZ-1] = VACIO;
                posX = ANCHO / 2;
                posY = ALTO / 2;
                tablero[posX, posY, posZ] = PERSONAJE;
                DescubrirAreaAlrededor(posX, posY, posZ);
                
                // Reiniciar contador de movimientos al cambiar de nivel
                movimientosSinChocar = 0;
                
                // Mostrar información sobre los tesoros que se conservan
                if (inventario.Count > 0)
                {
                    Console.WriteLine($"Conservas {inventario.Count} tesoros en tu inventario para este nivel.");
                }
            }
            else
            {
                // Al ganar el juego, convertir inventario a puntos
                if (inventario.Count > 0)
                {
                    int puntosInventario = inventario.Sum();
                    puntaje += puntosInventario;
                    Console.WriteLine($"¡Convertiste {inventario.Count} tesoros del inventario en {puntosInventario} puntos!");
                }
                GanarJuego();
            }
        }
        
        private void PerderVida()
        {
            vidas--;
            energia = 4; // Resetear energía
            movimientosSinChocar = 0; // Reiniciar contador
            Console.WriteLine($"¡Perdiste una vida! Vidas restantes: {vidas}");
            
            if (vidas <= 0)
            {
                // Al perder, convertir inventario a puntos
                if (inventario.Count > 0)
                {
                    int puntosInventario = inventario.Sum();
                    puntaje += puntosInventario;
                    Console.WriteLine($"¡Convertiste {inventario.Count} tesoros del inventario en {puntosInventario} puntos!");
                }
                GameOver();
            }
        }
        
        private void GameOver()
        {
            Console.WriteLine("\n");
            Console.WriteLine("==============================");
            Console.WriteLine("|                            |");
            Console.WriteLine("|         GAME OVER          |");
            Console.WriteLine($"|   Puntaje final: {puntaje} |");
            Console.WriteLine("|                            |");
            Console.WriteLine("==============================");
            
            PreguntarGuardarPuntaje();
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        private void GanarJuego()
        {
            Console.WriteLine("\n");
            Console.WriteLine("==============================");
            Console.WriteLine("|                            |");
            Console.WriteLine("|    ¡FELICIDADES!           |");
            Console.WriteLine("|   HAS GANADO EL JUEGO      |");
            Console.WriteLine($"|   Puntaje final: {puntaje} |");
            Console.WriteLine("|                            |");
            Console.WriteLine("==============================");
            
            PreguntarGuardarPuntaje();
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        // Getters para posibles expansiones
        public int GetVidas() { return vidas; }
        public int GetEnergia() { return energia; }
        public int GetPuntaje() { return puntaje; }
        public int GetNivelActual() { return posZ + 1; }

        public int GetElementoEn(int x, int y)
        {
            return tablero[x, y, posZ];
        }

        public bool EstaDescubierto(int x, int y)
        {
            return descubierto[x, y, posZ];
        }

        // Métodos públicos para obtener dimensiones
        public static int GetAncho() { return ANCHO; }
        public static int GetAlto() { return ALTO; }
        
        // Método principal del juego
        public void Jugar()
        {
            Console.WriteLine("¡Bienvenido a Cazador de Tesoros!");
            Console.WriteLine("\n");
            Console.WriteLine("====================Reglas de Cazador de Tesoros==============");
            Console.WriteLine("Encuentra tesoros (T), evita trampas (X) y busca la salida (S)");
            Console.WriteLine("Recuperas energía cada 3 movimientos sin chocar (máximo 4)");
            Console.WriteLine("Inventario: puedes guardar hasta 5 tesoros");
            Console.WriteLine("Los tesoros se conservan entre niveles 1 y 2");
            Console.WriteLine("Visión en cruz: ves tu posición y las 4 direcciones adyacentes");
            Console.WriteLine("===============================================================");
            Console.WriteLine("\n");
            Console.WriteLine("Presiona cualquier tecla para comenzar...");
            Console.ReadKey();
            
            bool jugando = true;
            
            while (jugando)
            {
                ImprimirTablero();
                
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                bool movimientoExitoso = false;
                
                switch (tecla.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        movimientoExitoso = MoverPersonaje(0, -1);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        movimientoExitoso = MoverPersonaje(0, 1);
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        movimientoExitoso = MoverPersonaje(-1, 0);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        movimientoExitoso = MoverPersonaje(1, 0);
                        break;
                    case ConsoleKey.I:
                        GestionarInventario();
                        break;
                    case ConsoleKey.T:
                        if (inventario.Count > 0)
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("¿Qué tesoro quieres tirar? (1-5):");
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out int indice) && indice >= 1 && indice <= inventario.Count)
                            {
                                int tesoroEliminado = inventario[indice - 1];
                                inventario.RemoveAt(indice - 1);
                                Console.WriteLine($"Tiraste un tesoro de {tesoroEliminado} puntos");
                                Console.WriteLine("\n");
                                Console.WriteLine("Presiona cualquier tecla para continuar...");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("Inventario vacío. Presiona cualquier tecla para continuar...");
                            Console.ReadKey();
                        }
                        break;
                    case ConsoleKey.R:
                        MostrarRanking();
                        Console.WriteLine("\n");
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.Q:
                        jugando = false;
                        Console.WriteLine("\n");
                        Console.WriteLine("¡Gracias por jugar!");
                        break;
                }
                
                if (movimientoExitoso)
                {
                    // Pequeña pausa para que el jugador pueda ver lo que pasó
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CazadorDeTesoros juego = new CazadorDeTesoros();
            juego.Jugar();
        }
    }
}