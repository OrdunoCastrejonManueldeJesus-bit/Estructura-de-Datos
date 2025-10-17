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
        private List<Tesoro> inventario; // Inventario de tesoros (ahora guarda objetos Tesoro)
        private const int CAPACIDAD_INVENTARIO = 5;
        
        // Sistema de ranking y guardado
        private List<Jugador> ranking;
        private const string ARCHIVO_RANKING = "ranking.txt";
        private const string ARCHIVO_PARTIDA = "partida_guardada.txt";
        private const int MAX_RANKING = 10;
        
        private Random random;

        // Clase para tesoros con nivel de origen
        private class Tesoro
        {
            public int ValorBase { get; set; }
            public int NivelOrigen { get; set; }
            
            public Tesoro(int valorBase, int nivelOrigen)
            {
                ValorBase = valorBase;
                NivelOrigen = nivelOrigen;
            }

            // Calcular valor actual basado en el nivel actual
            public int CalcularValor(int nivelActual)
            {
                int nivelesAvanzados = nivelActual - NivelOrigen;
                if (nivelesAvanzados <= 0) return ValorBase;
                
                // Tesoros del nivel 1: 100 -> 200 -> 400
                if (ValorBase == 100)
                {
                    if (nivelActual == 1) return 200;  // Nivel 2
                    if (nivelActual == 2) return 400;  // Nivel 3
                }
                // Tesoros del nivel 2: 500 -> 1000
                else if (ValorBase == 500)
                {
                    if (nivelActual == 2) return 1000; // Nivel 3
                }
                // Tesoros del nivel 3: 1000 (no cambian)
                return ValorBase;
            }
        }
        
        public CazadorDeTesoros()
        {
            this.tablero = new int[ANCHO, ALTO, NIVELES];
            this.descubierto = new bool[ANCHO, ALTO, NIVELES];
            this.vidas = 3;
            this.energia = 4;
            this.puntaje = 0;
            this.movimientosSinChocar = 0;
            this.inventario = new List<Tesoro>();
            this.ranking = new List<Jugador>();
            this.random = new Random();
            
            CargarRanking();
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
        
        // Sistema de guardado y carga de partidas
        public void GuardarPartida()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ARCHIVO_PARTIDA))
                {
                    // Guardar estadísticas del jugador
                    writer.WriteLine($"{vidas},{energia},{puntaje},{movimientosSinChocar},{posX},{posY},{posZ}");
                    
                    // Guardar inventario (valorBase,nivelOrigen)
                    var inventarioData = inventario.Select(t => $"{t.ValorBase},{t.NivelOrigen}");
                    writer.WriteLine(string.Join(";", inventarioData));
                    
                    // Guardar tablero
                    for (int z = 0; z < NIVELES; z++)
                    {
                        for (int y = 0; y < ALTO; y++)
                        {
                            for (int x = 0; x < ANCHO; x++)
                            {
                                writer.Write(tablero[x, y, z]);
                            }
                            writer.WriteLine();
                        }
                        writer.WriteLine("---"); // Separador entre niveles
                    }
                    
                    // Guardar descubierto
                    for (int z = 0; z < NIVELES; z++)
                    {
                        for (int y = 0; y < ALTO; y++)
                        {
                            for (int x = 0; x < ANCHO; x++)
                            {
                                writer.Write(descubierto[x, y, z] ? '1' : '0');
                            }
                            writer.WriteLine();
                        }
                        writer.WriteLine("---"); // Separador entre niveles
                    }
                }
                Console.WriteLine("¡Partida guardada exitosamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar partida: {ex.Message}");
            }
        }
        
        public bool CargarPartida()
        {
            if (!File.Exists(ARCHIVO_PARTIDA))
            {
                return false;
            }
            
            try
            {
                string[] lineas = File.ReadAllLines(ARCHIVO_PARTIDA);
                int lineaIndex = 0;
                
                // Cargar estadísticas del jugador
                string[] stats = lineas[lineaIndex++].Split(',');
                vidas = int.Parse(stats[0]);
                energia = int.Parse(stats[1]);
                puntaje = int.Parse(stats[2]);
                movimientosSinChocar = int.Parse(stats[3]);
                posX = int.Parse(stats[4]);
                posY = int.Parse(stats[5]);
                posZ = int.Parse(stats[6]);
                
                // Cargar inventario
                inventario.Clear();
                if (!string.IsNullOrEmpty(lineas[lineaIndex]))
                {
                    string[] tesorosData = lineas[lineaIndex].Split(';');
                    foreach (string tesoroData in tesorosData)
                    {
                        string[] partes = tesoroData.Split(',');
                        if (partes.Length == 2)
                        {
                            int valorBase = int.Parse(partes[0]);
                            int nivelOrigen = int.Parse(partes[1]);
                            inventario.Add(new Tesoro(valorBase, nivelOrigen));
                        }
                    }
                }
                lineaIndex++;
                
                // Cargar tablero
                for (int z = 0; z < NIVELES; z++)
                {
                    for (int y = 0; y < ALTO; y++)
                    {
                        string linea = lineas[lineaIndex++];
                        for (int x = 0; x < ANCHO; x++)
                        {
                            tablero[x, y, z] = int.Parse(linea[x].ToString());
                        }
                    }
                    lineaIndex++; // Saltar separador
                }
                
                // Cargar descubierto
                for (int z = 0; z < NIVELES; z++)
                {
                    for (int y = 0; y < ALTO; y++)
                    {
                        string linea = lineas[lineaIndex++];
                        for (int x = 0; x < ANCHO; x++)
                        {
                            descubierto[x, y, z] = linea[x] == '1';
                        }
                    }
                    if (lineaIndex < lineas.Length) lineaIndex++; // Saltar separador si existe
                }
                Console.WriteLine("\n");
                Console.WriteLine("¡Partida cargada exitosamente!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar partida: {ex.Message}");
                return false;
            }
        }
        
        private void PreguntarGuardarPuntaje()
        {
            Console.WriteLine($"¿Deseas guardar tu puntaje de {puntaje} puntos en el ranking? (S/N):");
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
                Console.WriteLine("\n");
                Console.WriteLine($"Puntaje: {puntaje}");
                
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
                
                Console.WriteLine("\n");
                Console.WriteLine("¡Puntaje guardado exitosamente!");
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
            Console.WriteLine("\n");
            Console.WriteLine("=== MEJORES PUNTAJES ===");
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
                GenerarLaberintoConPasillos(z);
                ColocarElementos(z);
            }
            
            // Colocar personaje en el nivel 0, posición central
            posX = ANCHO / 2;
            posY = ALTO / 2;
            posZ = 0;
            tablero[posX, posY, posZ] = PERSONAJE;
            DescubrirAreaAlrededor(posX, posY, posZ);
        }
        
        private void GenerarLaberintoConPasillos(int nivel)
        {
            // Crear estructura básica con pasillos principales
            for (int x = 0; x < ANCHO; x++)
            {
                for (int y = 0; y < ALTO; y++)
                {
                    // Crear bordes
                    if (x == 0 || x == ANCHO - 1 || y == 0 || y == ALTO - 1)
                    {
                        tablero[x, y, nivel] = PARED;
                    }
                    else
                    {
                        // Crear pasillos principales en forma de cuadrícula
                        bool esPasilloHorizontal = (y % 4 == 2) && (x % 4 != 0);
                        bool esPasilloVertical = (x % 4 == 2) && (y % 4 != 0);
                        bool esInterseccion = (x % 4 == 2) && (y % 4 == 2);
                        
                        if (esPasilloHorizontal || esPasilloVertical || esInterseccion)
                        {
                            tablero[x, y, nivel] = VACIO;
                        }
                        else
                        {
                            // Aleatorizar algunas paredes internas para hacerlo más orgánico
                            if (random.Next(100) < 70) // 70% de probabilidad de ser pared
                            {
                                tablero[x, y, nivel] = PARED;
                            }
                            else
                            {
                                tablero[x, y, nivel] = VACIO;
                            }
                        }
                    }
                }
            }
            
            // Conectar áreas desconectadas
            ConectarAreasDesconectadas(nivel);
            
            // Asegurar que haya un camino desde el centro
            AsegurarConexionDesdeCentro(nivel);
        }
        
        private void ConectarAreasDesconectadas(int nivel)
        {
            // Conectar áreas aleatorias para asegurar que el laberinto sea transitable
            for (int i = 0; i < 30; i++) // Intentar 30 conexiones
            {
                int x = random.Next(2, ANCHO - 2);
                int y = random.Next(2, ALTO - 2);
                
                if (tablero[x, y, nivel] == PARED)
                {
                    // Verificar si conectar esta pared crearía un pasillo útil
                    int vaciosAlrededor = 0;
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if ((dx == 0 || dy == 0) && !(dx == 0 && dy == 0))
                            {
                                int nx = x + dx;
                                int ny = y + dy;
                                if (nx >= 0 && nx < ANCHO && ny >= 0 && ny < ALTO && 
                                    tablero[nx, ny, nivel] == VACIO)
                                {
                                    vaciosAlrededor++;
                                }
                            }
                        }
                    }
                    
                    // Si hay al menos 2 espacios vacíos adyacentes, convertir en pasillo
                    if (vaciosAlrededor >= 2)
                    {
                        tablero[x, y, nivel] = VACIO;
                    }
                }
            }
        }
        
        private void AsegurarConexionDesdeCentro(int nivel)
        {
            int centroX = ANCHO / 2;
            int centroY = ALTO / 2;
            
            // Crear caminos desde el centro en las 4 direcciones principales
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };
            
            for (int i = 0; i < 4; i++)
            {
                int x = centroX;
                int y = centroY;
                
                // Crear camino en esta dirección por 5 casillas
                for (int j = 0; j < 5; j++)
                {
                    x += dx[i];
                    y += dy[i];
                    
                    if (x > 0 && x < ANCHO - 1 && y > 0 && y < ALTO - 1)
                    {
                        tablero[x, y, nivel] = VACIO;
                        
                        // También limpiar algunas casillas adyacentes para hacerlo más ancho
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                if (random.Next(100) < 40) // 40% de probabilidad
                                {
                                    int nx = x + k;
                                    int ny = y + l;
                                    if (nx > 0 && nx < ANCHO - 1 && ny > 0 && ny < ALTO - 1)
                                    {
                                        tablero[nx, ny, nivel] = VACIO;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private void ColocarElementos(int nivel)
        {
            // Colocar tesoros según el nivel
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
            
            // Colocar trampas según el nivel
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
            
            // Colocar salida en posición aleatoria
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
            Console.WriteLine("\n");
            Console.WriteLine("Controles: W (Arriba), S (Abajo), A (Izquierda), D (Derecha)");
            Console.WriteLine("I (Inventario), T (Tirar tesoro), G (Guardar), C (Cargar), R (Ranking), Q (Salir)");
        }
        
        private void MostrarInventario()
        {
            if (inventario.Count > 0)
            {
                Console.Write("Tesoros: ");
                for (int i = 0; i < inventario.Count; i++)
                {
                    int valorActual = inventario[i].CalcularValor(posZ);
                    Console.Write($"[{i + 1}:{valorActual}p(N{inventario[i].NivelOrigen + 1})] ");
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
            Console.WriteLine("\n");
            Console.WriteLine($"Capacidad: {inventario.Count}/{CAPACIDAD_INVENTARIO}");
            
            if (inventario.Count > 0)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Presiona el número del tesoro para tirarlo (1-5) o Enter para continuar:");
                string input = Console.ReadLine();
                
                if (int.TryParse(input, out int indice) && indice >= 1 && indice <= inventario.Count)
                {
                    Tesoro tesoroEliminado = inventario[indice - 1];
                    inventario.RemoveAt(indice - 1);
                    // CAMBIO: Al tirar un tesoro, NO se suma al puntaje
                    int valorActual = tesoroEliminado.CalcularValor(posZ);
                    Console.WriteLine($"Tiraste un tesoro del nivel {tesoroEliminado.NivelOrigen + 1} que vale {valorActual} puntos.");
                    Console.WriteLine("El puntaje se mantiene igual.");
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
        }
        
        private void DescubrirAreaAlrededor(int x, int y, int z)
        {
            // Rango de visión en forma de cruz
            descubierto[x, y, z] = true;
            
            if (y - 1 >= 0) descubierto[x, y - 1, z] = true;
            if (y + 1 < ALTO) descubierto[x, y + 1, z] = true;
            if (x - 1 >= 0) descubierto[x - 1, y, z] = true;
            if (x + 1 < ANCHO) descubierto[x + 1, y, z] = true;
        }
        
        public bool MoverPersonaje(int dx, int dy)
        {
            int nuevoX = posX + dx;
            int nuevoY = posY + dy;
            
            if (nuevoX < 0 || nuevoX >= ANCHO || nuevoY < 0 || nuevoY >= ALTO)
            {
                return false;
            }
            
            if (tablero[nuevoX, nuevoY, posZ] == PARED)
            {
                energia--;
                movimientosSinChocar = 0;
                Console.WriteLine($"¡Chocaste con una pared! Energía: {energia}");
                if (energia <= 0)
                {
                    PerderVida();
                }
                return false;
            }
            
            movimientosSinChocar++;
            
            if (movimientosSinChocar >= 3 && energia < 4)
            {
                energia++;
                movimientosSinChocar = 0;
                Console.WriteLine($"¡Recuperaste energía! Energía: {energia}");
            }
            
            tablero[posX, posY, posZ] = VACIO;
            posX = nuevoX;
            posY = nuevoY;
            
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
            
            int valorBase = 0;
            switch (posZ)
            {
                case 0: valorBase = 100; break;  // Nivel 1: 100 puntos base
                case 1: valorBase = 500; break;  // Nivel 2: 500 puntos base
                case 2: valorBase = 1000; break; // Nivel 3: 1000 puntos base
            }
            
            // Sumar puntos inmediatamente al encontrar tesoro
            puntaje += valorBase;
            Console.WriteLine($"¡Obtuviste {valorBase} puntos!");
            
            // También guardar en inventario si hay espacio
            if (inventario.Count < CAPACIDAD_INVENTARIO)
            {
                Tesoro nuevoTesoro = new Tesoro(valorBase, posZ);
                inventario.Add(nuevoTesoro);
                Console.WriteLine($"¡Y guardaste un tesoro del nivel {posZ + 1} en tu inventario!");
                Console.WriteLine($"Inventario: {inventario.Count}/{CAPACIDAD_INVENTARIO}");
            }
            else
            {
                Console.WriteLine("Inventario lleno. No puedes guardar más tesoros.");
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
                posZ++;
                Console.WriteLine($"¡Pasaste al nivel {posZ + 1}!");
                
                // CAMBIO: Recompensa fija de 100 puntos por pasar nivel
                int recompensa = 100;
                puntaje += recompensa;
                Console.WriteLine($"¡Recompensa por completar nivel: {recompensa} puntos!");
                
                tablero[posX, posY, posZ-1] = VACIO;
                posX = ANCHO / 2;
                posY = ALTO / 2;
                tablero[posX, posY, posZ] = PERSONAJE;
                DescubrirAreaAlrededor(posX, posY, posZ);
                
                movimientosSinChocar = 0;
                
                if (inventario.Count > 0)
                {
                    Console.WriteLine($"Conservas {inventario.Count} tesoros en tu inventario para este nivel.");
                    Console.WriteLine("¡Los tesoros han aumentado de valor!");
                }
            }
            else
            {
                // Al ganar, acumular puntos del inventario restante con valor actualizado
                if (inventario.Count > 0)
                {
                    int puntosInventario = 0;
                    foreach (var tesoro in inventario)
                    {
                        puntosInventario += tesoro.CalcularValor(posZ);
                    }
                    puntaje += puntosInventario;
                    Console.WriteLine($"¡Convertiste {inventario.Count} tesoros del inventario en {puntosInventario} puntos!");
                }
                GanarJuego();
            }
        }
        
        private void PerderVida()
        {
            vidas--;
            energia = 4;
            movimientosSinChocar = 0;
            Console.WriteLine($"¡Perdiste una vida! Vidas restantes: {vidas}");
            
            if (vidas <= 0)
            {
                // Al perder, acumular puntos del inventario restante con valor actualizado
                if (inventario.Count > 0)
                {
                    int puntosInventario = 0;
                    foreach (var tesoro in inventario)
                    {
                        puntosInventario += tesoro.CalcularValor(posZ);
                    }
                    puntaje += puntosInventario;
                    Console.WriteLine($"¡Convertiste {inventario.Count} tesoros del inventario en {puntosInventario} puntos!");
                }
                GameOver();
            }
        }
        
        private void GameOver()
        {
            // CAMBIO: Asegurar que el puntaje sea múltiplo de 100
            puntaje = (puntaje / 100) * 100;
            
            Console.WriteLine("\n");
            Console.WriteLine("==============================");
            Console.WriteLine("|                            |");
            Console.WriteLine("|         GAME OVER          |");
            Console.WriteLine($"|   Puntaje final: {puntaje} |");
            Console.WriteLine("|                            |");
            Console.WriteLine("==============================");
            
            PreguntarGuardarPuntaje();
            Console.WriteLine("\n");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        private void GanarJuego()
        {
            // CAMBIO: Asegurar que el puntaje sea múltiplo de 100
            puntaje = (puntaje / 100) * 100;
            
            Console.WriteLine("\n");
            Console.WriteLine("==============================");
            Console.WriteLine("|                            |");
            Console.WriteLine("|    ¡FELICIDADES!           |");
            Console.WriteLine("|   HAS GANADO EL JUEGO      |");
            Console.WriteLine($"|   Puntaje final: {puntaje} |");
            Console.WriteLine("|                            |");
            Console.WriteLine("==============================");
            
            PreguntarGuardarPuntaje();
            Console.WriteLine("\n");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        // Método principal del juego
        public void Jugar()
        {
            Console.WriteLine("¡Bienvenido a Cazador de Tesoros!");
            Console.WriteLine("\n");
            Console.WriteLine("====================Reglas de Cazador de Tesoros==============");
            Console.WriteLine("Encuentra tesoros (T), evita trampas (X) y busca la salida (S)");
            Console.WriteLine("Recuperas energía cada 3 movimientos sin chocar (máximo 4)");
            Console.WriteLine("Inventario: puedes guardar hasta 5 tesoros");
            Console.WriteLine("Valor de los Tesoros:");
            Console.WriteLine("- Nivel 1: 100 puntos, Nivel 2: 500, Nivel 3: 1000");
            Console.WriteLine("- Recompensa por nivel: 100 puntos");
            Console.WriteLine("===============================================================");
            Console.WriteLine("\n");
            Console.WriteLine("¿Deseas cargar una partida guardada? (S/N):");
            string respuesta = Console.ReadLine().ToUpper();
            
            if ((respuesta == "S" || respuesta == "SI") && CargarPartida())
            {   
                Console.WriteLine("\n");
                Console.WriteLine("Partida cargada. Presiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                InicializarTablero();
                Console.WriteLine("\n");
                Console.WriteLine("Nueva partida iniciada. Presiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
            
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
                                Tesoro tesoroEliminado = inventario[indice - 1];
                                inventario.RemoveAt(indice - 1);
                                // CAMBIO: Al tirar tesoro, NO se suma al puntaje
                                int valorActual = tesoroEliminado.CalcularValor(posZ);
                                Console.WriteLine($"Tiraste un tesoro del nivel {tesoroEliminado.NivelOrigen + 1} que vale {valorActual} puntos.");
                                Console.WriteLine("El puntaje se mantiene igual.");
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
                    case ConsoleKey.G:
                        GuardarPartida();
                        Console.WriteLine("\n");
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.C:
                        if (CargarPartida())
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("Presiona cualquier tecla para continuar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("No hay partida guardada. Presiona cualquier tecla para continuar...");
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