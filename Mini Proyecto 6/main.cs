using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Linq;
class SnakeGame
{
    private static int width = 40;
    private static int height = 20;
    private static int score = 0;
    private static int level = 1;
    private static int speed = 5;
    private static bool gameOver = false;
    private static Random random = new Random();
   
    // Direcciones
    private static ConsoleKey currentDirection = ConsoleKey.RightArrow;
    private static ConsoleKey nextDirection = ConsoleKey.RightArrow;
   
    // Snake
    private static List<Position> snake = new List<Position>();
    private static Position food = new Position();
    private static List<Position> traps = new List<Position>();
    private static bool foodActive = false;
   
    // Ranking
    private static List<RankingEntry> ranking = new List<RankingEntry>();
    private const string RankingFileName = "snake_ranking.txt";
   
    struct Position
    {
        public int X;
        public int Y;
       
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
       
        public static bool operator ==(Position a, Position b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
       
        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }
    }
   
    class RankingEntry
    {
        public string Name { get; set; }
        public string RankingCode { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
       
        public override string ToString()
        {
            return $"Nombre: {Name} - Puntaje: {Score} - Nivel: {Level} - {Date:dd/MM/yyyy}";
        }
    }
    static void Main()
    {
        Console.CursorVisible = false;
        LoadRanking();
        ShowMainMenu();
    }
   
    static void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SNAKE GAME ===");
            Console.WriteLine("| 1. Jugar       |");
            Console.WriteLine("| 2. Ver Ranking |");
            Console.WriteLine("| 3. Salir       |");
            Console.WriteLine("==================");
            Console.WriteLine("\n");
            Console.Write("Selecciona una opcion: ");
           
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    InitializeGame();
                    StartGame();
                    break;
                case ConsoleKey.D2:
                    ShowRanking();
                    break;
                case ConsoleKey.D3:
                    return;
            }
        }
    }
   
    static void StartGame()
    {
        Thread inputThread = new Thread(ReadInput);
        inputThread.Start();
       
        while (!gameOver)
        {
            UpdateDirection();
            MoveSnake();
            CheckCollisions();
            if (gameOver) break;
            GenerateItems();
            DrawGame();
            Thread.Sleep(1000 / speed);
        }
       
        inputThread.Join();
        GameOver();
    }
   
    static void InitializeGame()
    {
        snake.Clear();
        traps.Clear();
        int startX = width / 4;
        int startY = height / 2;
       
        for (int i = 0; i < 5; i++)
        {
            snake.Add(new Position(startX - i, startY));
        }
       
        speed = 5;
        level = 1;
        score = 0;
        currentDirection = ConsoleKey.RightArrow;
        nextDirection = ConsoleKey.RightArrow;
        foodActive = false;
        gameOver = false;
       
        // Generar trampas iniciales
        for (int i = 0; i < 1; i++)
        {
            GenerateTrap();
        }
    }
   
    static void ReadInput()
    {
        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
               
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentDirection != ConsoleKey.DownArrow)
                            nextDirection = ConsoleKey.UpArrow;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentDirection != ConsoleKey.UpArrow)
                            nextDirection = ConsoleKey.DownArrow;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (currentDirection != ConsoleKey.RightArrow)
                            nextDirection = ConsoleKey.LeftArrow;
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentDirection != ConsoleKey.LeftArrow)
                            nextDirection = ConsoleKey.RightArrow;
                        break;
                    case ConsoleKey.Escape:
                        gameOver = true;
                        break;
                }
            }
            Thread.Sleep(50);
        }
    }
   
    static void UpdateDirection()
    {
        currentDirection = nextDirection;
    }
   
    static void MoveSnake()
    {
        Position newHead = snake[0];
       
        switch (currentDirection)
        {
            case ConsoleKey.UpArrow:
                newHead.Y--;
                break;
            case ConsoleKey.DownArrow:
                newHead.Y++;
                break;
            case ConsoleKey.LeftArrow:
                newHead.X--;
                break;
            case ConsoleKey.RightArrow:
                newHead.X++;
                break;
        }
       
        // Wrap around borders
        if (newHead.X < 0) newHead.X = width - 1;
        if (newHead.X >= width) newHead.X = 0;
        if (newHead.Y < 0) newHead.Y = height - 1;
        if (newHead.Y >= height) newHead.Y = 0;
       
        snake.Insert(0, newHead);
       
        // Comer comida
        if (foodActive && newHead == food)
        {
            score += 100; // +100 puntos por comida
            speed++; // Incrementar velocidad
            foodActive = false;
           
            // Generar una trampa nueva después de comer
            GenerateTrap();
           
            // Verificar si pasa al siguiente nivel
            if (snake.Count >= 15)
            {
                level++;
                // Reiniciar snake a tamaño 5
                while (snake.Count > 5)
                {
                    snake.RemoveAt(snake.Count - 1);
                }
            }
        }
        else
        {
            // Verificar si pisa una trampa
            bool hitTrap = false;
            for (int i = traps.Count - 1; i >= 0; i--)
            {
                if (newHead == traps[i])
                {
                    score = Math.Max(0, score - 50); // -50 puntos por trampa
                    speed = Math.Max(1, speed - 1);
                    traps.RemoveAt(i);
                    hitTrap = true;
                   
                    // Remover segmentos de la cola (mantener mínimo 0)
                    int segmentsToRemove = 2;
                    for (int j = 0; j < segmentsToRemove; j++)
                    {
                        if (snake.Count > 0)
                        {
                            snake.RemoveAt(snake.Count - 1);
                        }
                    }
                    break;
                }
            }
           
            // Si no comió comida ni pisó trampa, remover cola normal
            if (!hitTrap)
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }
    }
   
    static void CheckCollisions()
    {
        if (snake.Count <= 0)
        {
            gameOver = true;
            return;
        }
       
        Position head = snake[0];
       
        // Colisión consigo mismo
        for (int i = 1; i < snake.Count; i++)
        {
            if (head == snake[i])
            {
                gameOver = true;
                return;
            }
        }
    }
   
    static void GenerateItems()
    {
        if (!foodActive)
        {
            do
            {
                food = new Position(random.Next(0, width), random.Next(0, height));
            } while (IsPositionOccupied(food));
            foodActive = true;
        }
    }
   
    static void GenerateTrap()
    {
        Position newTrap;
        int attempts = 0;
        do
        {
            newTrap = new Position(random.Next(0, width), random.Next(0, height));
            attempts++;
        } while (IsPositionOccupied(newTrap) && attempts < 50);
       
        if (attempts < 50)
        {
            traps.Add(newTrap);
        }
    }
   
    static bool IsPositionOccupied(Position pos)
    {
        // Verificar snake
        foreach (var segment in snake)
        {
            if (segment == pos)
                return true;
        }
       
        // Verificar comida
        if (foodActive && food == pos)
            return true;
           
        // Verificar trampas
        foreach (var trap in traps)
        {
            if (trap == pos)
                return true;
        }
       
        return false;
    }
   
    static void DrawGame()
    {
        Console.Clear();
       
        // Dibujar bordes superiores
        for (int i = 0; i < width + 2; i++)
            Console.Write("_");
        Console.WriteLine();
       
        bool[,] buffer = new bool[width, height];
       
        // Limpiar buffer
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                buffer[x, y] = false;
       
        // Marcar snake en buffer
        foreach (var segment in snake)
        {
            if (segment.X >= 0 && segment.X < width && segment.Y >= 0 && segment.Y < height)
                buffer[segment.X, segment.Y] = true;
        }
       
        // Dibujar juego usando buffer
        for (int y = 0; y < height; y++)
        {
            Console.Write("|");
            for (int x = 0; x < width; x++)
            {
                Position currentPos = new Position(x, y);
               
                if (snake.Count > 0 && snake[0] == currentPos)
                {
                    Console.Write("O"); // Cabeza
                }
                else if (buffer[x, y])
                {
                    Console.Write("N"); // Cuerpo (5 en total: O + NNNN)
                }
                else if (foodActive && food == currentPos)
                {
                    Console.Write("+"); // Comida
                }
                else if (traps.Any(t => t == currentPos))
                {
                    Console.Write("-"); // Trampa
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine("|");
        }
       
        // Dibujar bordes inferiores
        for (int i = 0; i < width + 2; i++)
            Console.Write("_");
        Console.WriteLine();
       
        // Mostrar información
        Console.WriteLine($"| Nivel: {level} | Puntaje: {score} | Velocidad: {speed} | Tamaño: {snake.Count} | Trampas: {traps.Count} |");
        Console.WriteLine("| Flechas: Mover | ESC: Salir | +: Comida | -: Trampa |");
    }
   
    static void GameOver()
    {
        string rankingCode = $"M{level}{score % 100:D2}";
       
        Console.Clear();
        Console.WriteLine("=== GAME OVER ===");
        Console.WriteLine($"Puntaje Final: {score}");
        Console.WriteLine($"Nivel Alcanzado: {level}");
        Console.WriteLine($"Tamaño Final: {snake.Count}");
        Console.WriteLine($"Código de Ranking: {rankingCode}");
       
        // Preguntar si guardar ranking
        Console.Write("\n¿Deseas guardar tu puntuación en el ranking? (s/n): ");
        var key = Console.ReadKey(true);
       
        if (key.Key == ConsoleKey.S || key.KeyChar == 's' || key.KeyChar == 'S')
        {
            Console.Write("\nIngresa tu nombre (máx. 3 letras): ");
            string name = Console.ReadLine();
           
            if (string.IsNullOrWhiteSpace(name))
                name = "JUG";
           
            name = name.Length > 3 ? name.Substring(0, 3).ToUpper() : name.ToUpper();
           
            // Agregar al ranking
            ranking.Add(new RankingEntry
            {
                Name = name,
                RankingCode = rankingCode,
                Score = score,
                Level = level,
                Date = DateTime.Now
            });
           
            // Guardar ranking
            SaveRanking();
            Console.WriteLine($"\n¡Puntuación guardada! {name} - {rankingCode}");
        }
       
        Console.WriteLine("\nPresiona R para reiniciar, M para menú principal o cualquier tecla para salir");
       
        key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.R)
        {
            InitializeGame();
            StartGame();
        }
        else if (key.Key == ConsoleKey.M)
        {
            ShowMainMenu();
        }
    }
   
    static void LoadRanking()
    {
        ranking = new List<RankingEntry>();
       
        try
        {
            if (File.Exists(RankingFileName))
            {
                var lines = File.ReadAllLines(RankingFileName);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 5)
                    {
                        ranking.Add(new RankingEntry
                        {
                            Name = parts[0],
                            RankingCode = parts[1],
                            Score = int.Parse(parts[2]),
                            Level = int.Parse(parts[3]),
                            Date = DateTime.Parse(parts[4])
                        });
                    }
                }
               
                // Ordenar por puntaje descendente
                ranking = ranking.OrderByDescending(r => r.Score)
                                .ThenByDescending(r => r.Level)
                                .ToList();
            }
        }
        catch (Exception ex)
        {
            // Silenciar error para OnlineGDB
        }
    }
   
    static void SaveRanking()
    {
        try
        {
            var lines = new List<string>();
            foreach (var entry in ranking.OrderByDescending(r => r.Score)
                                       .ThenByDescending(r => r.Level))
            {
                lines.Add($"{entry.Name}|{entry.RankingCode}|{entry.Score}|{entry.Level}|{entry.Date:yyyy-MM-dd HH:mm:ss}");
            }
            File.WriteAllLines(RankingFileName, lines);
        }
        catch (Exception ex)
        {
            // Silenciar error para OnlineGDB
        }
    }
   
    static void ShowRanking()
    {
        Console.Clear();
        Console.WriteLine("=== RANKING ===");
       
        if (ranking.Count == 0)
        {
            Console.WriteLine("No hay puntuaciones guardadas.");
        }
        else
        {
            int position = 1;
            foreach (var entry in ranking.Take(10)) // Top 10
            {
                Console.WriteLine($"{position}. {entry}");
                position++;
            }
        }
       
        Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
        Console.ReadKey(true);
    }
}