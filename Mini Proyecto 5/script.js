// Configuración del juego
const gameState = {
    level: 1,
    score: 0,
    lives: 5,
    completedPuzzles: 0,
    currentDifficulty: 'very-easy',
    difficulties: {
        'very-easy': { name: 'Muy Fácil', min: 36, max: 44 },
        'easy': { name: 'Fácil', min: 32, max: 35 },
        'normal': { name: 'Normal', min: 28, max: 31 },
        'hard': { name: 'Difícil', min: 24, max: 27 },
        'expert': { name: 'Experto', min: 17, max: 23 }
    },
    selectedCell: null,
    grid: [],
    solution: [],
    initialGrid: []
};

// Elementos del DOM
const sudokuGrid = document.getElementById('sudoku-grid');
const levelElement = document.getElementById('level');
const scoreElement = document.getElementById('score');
const livesElement = document.getElementById('lives');
const difficultyElement = document.getElementById('difficulty');
const messageElement = document.getElementById('message');
const levelUpModal = document.getElementById('level-up');
const completedLevelElement = document.getElementById('completed-level');
const continueButton = document.getElementById('continue');

// Inicialización del juego
function initGame() {
    createSudokuGrid();
    setupEventListeners();
    generateNewPuzzle();
    updateUI();
}

// Crear la cuadrícula del Sudoku
function createSudokuGrid() {
    sudokuGrid.innerHTML = '';
    for (let i = 0; i < 81; i++) {
        const cell = document.createElement('div');
        cell.className = 'cell';
        cell.dataset.index = i;
        cell.addEventListener('click', () => selectCell(i));
        sudokuGrid.appendChild(cell);
    }
}

// Configurar event listeners
function setupEventListeners() {
    // Botones de dificultad
    document.querySelectorAll('.difficulty-btn').forEach(btn => {
        btn.addEventListener('click', (e) => {
            document.querySelectorAll('.difficulty-btn').forEach(b => b.classList.remove('active'));
            e.target.classList.add('active');
            gameState.currentDifficulty = e.target.dataset.difficulty;
            generateNewPuzzle();
            updateUI();
        });
    });

    // Botones de números
    document.querySelectorAll('.number-btn').forEach(btn => {
        btn.addEventListener('click', (e) => {
            if (gameState.selectedCell !== null) {
                const number = parseInt(e.target.dataset.number);
                if (number === 0) {
                    clearCell();
                } else {
                    fillCell(number);
                }
            }
        });
    });

    // Botones de control
    document.getElementById('new-game').addEventListener('click', generateNewPuzzle);
    document.getElementById('check').addEventListener('click', checkSolution);
    document.getElementById('solve').addEventListener('click', solvePuzzle);
    document.getElementById('save').addEventListener('click', saveGame);
    document.getElementById('load').addEventListener('click', loadGame);
    continueButton.addEventListener('click', continueToNextLevel);

    // Teclado
    document.addEventListener('keydown', handleKeyPress);
}

// Seleccionar celda
function selectCell(index) {
    // Quitar selección anterior
    if (gameState.selectedCell !== null) {
        document.querySelector(`.cell[data-index="${gameState.selectedCell}"]`).classList.remove('selected');
    }

    // No permitir seleccionar celdas fijas
    if (gameState.initialGrid[Math.floor(index / 9)][index % 9] !== 0) {
        gameState.selectedCell = null;
        return;
    }

    // Seleccionar nueva celda
    gameState.selectedCell = index;
    document.querySelector(`.cell[data-index="${index}"]`).classList.add('selected');
    
    // Resaltar celdas relacionadas
    highlightRelatedCells(index);
}

// Resaltar celdas relacionadas
function highlightRelatedCells(index) {
    // Quitar resaltado anterior
    document.querySelectorAll('.cell').forEach(cell => {
        cell.classList.remove('highlighted');
    });

    const row = Math.floor(index / 9);
    const col = index % 9;

    // Resaltar fila y columna
    for (let i = 0; i < 9; i++) {
        document.querySelector(`.cell[data-index="${row * 9 + i}"]`).classList.add('highlighted');
        document.querySelector(`.cell[data-index="${i * 9 + col}"]`).classList.add('highlighted');
    }

    // Resaltar cuadrante 3x3
    const startRow = Math.floor(row / 3) * 3;
    const startCol = Math.floor(col / 3) * 3;
    
    for (let i = startRow; i < startRow + 3; i++) {
        for (let j = startCol; j < startCol + 3; j++) {
            document.querySelector(`.cell[data-index="${i * 9 + j}"]`).classList.add('highlighted');
        }
    }

    // La celda seleccionada debe mantenerse con el estilo selected
    document.querySelector(`.cell[data-index="${index}"]`).classList.remove('highlighted');
    document.querySelector(`.cell[data-index="${index}"]`).classList.add('selected');
}

// Llenar celda con número
function fillCell(number) {
    if (gameState.selectedCell === null) return;

    const row = Math.floor(gameState.selectedCell / 9);
    const col = gameState.selectedCell % 9;

    // Verificar si el movimiento es válido
    if (isValidMove(row, col, number)) {
        gameState.grid[row][col] = number;
        updateGridDisplay();
        clearMessage();
        
        // Verificar si el sudoku está completo
        if (isPuzzleComplete()) {
            handlePuzzleCompletion();
        }
    } else {
        // Movimiento inválido, perder una vida
        gameState.lives--;
        updateUI();
        
        if (gameState.lives <= 0) {
            gameOver();
        } else {
            showMessage('Movimiento incorrecto! Pierdes una vida.', 'error');
        }
    }
}

// Limpiar celda
function clearCell() {
    if (gameState.selectedCell === null) return;

    const row = Math.floor(gameState.selectedCell / 9);
    const col = gameState.selectedCell % 9;

    gameState.grid[row][col] = 0;
    updateGridDisplay();
    clearMessage();
}

// Verificar si el movimiento es válido
function isValidMove(row, col, number) {
    // Verificar fila
    for (let i = 0; i < 9; i++) {
        if (gameState.grid[row][i] === number && i !== col) {
            return false;
        }
    }

    // Verificar columna
    for (let i = 0; i < 9; i++) {
        if (gameState.grid[i][col] === number && i !== row) {
            return false;
        }
    }

    // Verificar cuadrante 3x3
    const startRow = Math.floor(row / 3) * 3;
    const startCol = Math.floor(col / 3) * 3;
    
    for (let i = startRow; i < startRow + 3; i++) {
        for (let j = startCol; j < startCol + 3; j++) {
            if (gameState.grid[i][j] === number && (i !== row || j !== col)) {
                return false;
            }
        }
    }

    return true;
}

// Verificar si el puzzle está completo
function isPuzzleComplete() {
    for (let i = 0; i < 9; i++) {
        for (let j = 0; j < 9; j++) {
            if (gameState.grid[i][j] === 0 || gameState.grid[i][j] !== gameState.solution[i][j]) {
                return false;
            }
        }
    }
    return true;
}

// Manejar la finalización de un puzzle
function handlePuzzleCompletion() {
    gameState.score += 100 * gameState.level;
    gameState.completedPuzzles++;
    
    showMessage(`¡Felicidades! Has completado el sudoku. Puntuación: +${100 * gameState.level}`, 'success');
    
    // Verificar si se completaron 5 puzzles en el nivel actual
    if (gameState.completedPuzzles >= 5) {
        levelUp();
    } else {
        // Generar nuevo puzzle después de un breve retraso
        setTimeout(() => {
            generateNewPuzzle();
        }, 2000);
    }
    
    updateUI();
}

// Subir de nivel
function levelUp() {
    gameState.level++;
    gameState.completedPuzzles = 0;
    
    completedLevelElement.textContent = gameState.level - 1;
    levelUpModal.classList.add('active');
}

// Continuar al siguiente nivel
function continueToNextLevel() {
    levelUpModal.classList.remove('active');
    generateNewPuzzle();
    updateUI();
}

// Game Over
function gameOver() {
    showMessage('¡Game Over! Has perdido todas tus vidas.', 'error');
    setTimeout(() => {
        if (confirm('¡Game Over! ¿Quieres jugar de nuevo?')) {
            resetGame();
        }
    }, 1000);
}

// Reiniciar juego
function resetGame() {
    gameState.level = 1;
    gameState.score = 0;
    gameState.lives = 5;
    gameState.completedPuzzles = 0;
    gameState.currentDifficulty = 'very-easy';
    
    document.querySelectorAll('.difficulty-btn').forEach(btn => btn.classList.remove('active'));
    document.querySelector('.difficulty-btn.very-easy').classList.add('active');
    
    generateNewPuzzle();
    updateUI();
}

// Generar nuevo puzzle
function generateNewPuzzle() {
    // Generar una solución válida
    gameState.solution = generateSolvedSudoku();
    
    // Crear una copia para el puzzle
    gameState.grid = JSON.parse(JSON.stringify(gameState.solution));
    gameState.initialGrid = JSON.parse(JSON.stringify(gameState.solution));
    
    // Eliminar números según la dificultad
    const difficulty = gameState.difficulties[gameState.currentDifficulty];
    const cellsToRemove = getRandomInt(difficulty.min, difficulty.max);
    
    removeNumbers(cellsToRemove);
    
    // Actualizar la visualización
    updateGridDisplay();
    clearMessage();
    
    // Reiniciar selección
    if (gameState.selectedCell !== null) {
        document.querySelector(`.cell[data-index="${gameState.selectedCell}"]`).classList.remove('selected');
        gameState.selectedCell = null;
    }
}

// Generar un Sudoku resuelto
function generateSolvedSudoku() {
    const grid = Array(9).fill().map(() => Array(9).fill(0));
    
    // Llenar la diagonal de cuadrantes 3x3 con números aleatorios
    for (let i = 0; i < 9; i += 3) {
        fillBox(grid, i, i);
    }
    
    // Resolver el resto del Sudoku
    solveSudoku(grid);
    
    return grid;
}

// Llenar un cuadrante 3x3 con números aleatorios
function fillBox(grid, row, col) {
    const numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
    shuffleArray(numbers);
    
    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
            grid[row + i][col + j] = numbers.pop();
        }
    }
}

// Resolver Sudoku usando backtracking
function solveSudoku(grid) {
    const emptyCell = findEmptyCell(grid);
    if (!emptyCell) return true; // Sudoku resuelto
    
    const [row, col] = emptyCell;
    
    for (let num = 1; num <= 9; num++) {
        if (isSafe(grid, row, col, num)) {
            grid[row][col] = num;
            
            if (solveSudoku(grid)) {
                return true;
            }
            
            grid[row][col] = 0; // Backtrack
        }
    }
    
    return false; // No solution
}

// Encontrar celda vacía
function findEmptyCell(grid) {
    for (let i = 0; i < 9; i++) {
        for (let j = 0; j < 9; j++) {
            if (grid[i][j] === 0) {
                return [i, j];
            }
        }
    }
    return null;
}

// Verificar si es seguro colocar un número
function isSafe(grid, row, col, num) {
    // Verificar fila
    for (let i = 0; i < 9; i++) {
        if (grid[row][i] === num) return false;
    }
    
    // Verificar columna
    for (let i = 0; i < 9; i++) {
        if (grid[i][col] === num) return false;
    }
    
    // Verificar cuadrante 3x3
    const startRow = Math.floor(row / 3) * 3;
    const startCol = Math.floor(col / 3) * 3;
    
    for (let i = startRow; i < startRow + 3; i++) {
        for (let j = startCol; j < startCol + 3; j++) {
            if (grid[i][j] === num) return false;
        }
    }
    
    return true;
}

// Eliminar números del Sudoku resuelto
function removeNumbers(count) {
    let removed = 0;
    
    while (removed < count) {
        const row = getRandomInt(0, 8);
        const col = getRandomInt(0, 8);
        
        if (gameState.grid[row][col] !== 0) {
            gameState.grid[row][col] = 0;
            gameState.initialGrid[row][col] = 0;
            removed++;
        }
    }
}

// Actualizar la visualización de la cuadrícula
function updateGridDisplay() {
    for (let i = 0; i < 9; i++) {
        for (let j = 0; j < 9; j++) {
            const index = i * 9 + j;
            const cell = document.querySelector(`.cell[data-index="${index}"]`);
            
            cell.textContent = gameState.grid[i][j] !== 0 ? gameState.grid[i][j] : '';
            
            // Establecer clases según el estado de la celda
            cell.classList.remove('fixed', 'error');
            
            if (gameState.initialGrid[i][j] !== 0) {
                cell.classList.add('fixed');
            } else if (gameState.grid[i][j] !== 0 && gameState.grid[i][j] !== gameState.solution[i][j]) {
                cell.classList.add('error');
            }
        }
    }
}

// Comprobar solución
function checkSolution() {
    let hasErrors = false;
    
    for (let i = 0; i < 9; i++) {
        for (let j = 0; j < 9; j++) {
            const index = i * 9 + j;
            const cell = document.querySelector(`.cell[data-index="${index}"]`);
            
            if (gameState.grid[i][j] !== 0 && gameState.grid[i][j] !== gameState.solution[i][j]) {
                cell.classList.add('error');
                hasErrors = true;
            }
        }
    }
    
    if (hasErrors) {
        showMessage('Hay errores en tu solución.', 'error');
    } else if (isPuzzleComplete()) {
        showMessage('¡Felicidades! La solución es correcta.', 'success');
    } else {
        showMessage('La solución parece correcta hasta ahora.', 'success');
    }
}

// Resolver puzzle
function solvePuzzle() {
    gameState.grid = JSON.parse(JSON.stringify(gameState.solution));
    updateGridDisplay();
    showMessage('Puzzle resuelto.', 'success');
}

// Guardar partida
function saveGame() {
    const gameData = {
        level: gameState.level,
        score: gameState.score,
        lives: gameState.lives,
        completedPuzzles: gameState.completedPuzzles,
        currentDifficulty: gameState.currentDifficulty,
        grid: gameState.grid,
        solution: gameState.solution,
        initialGrid: gameState.initialGrid
    };
    
    const dataString = JSON.stringify(gameData, null, 2);
    const blob = new Blob([dataString], { type: 'text/plain' });
    const url = URL.createObjectURL(blob);
    
    const a = document.createElement('a');
    a.href = url;
    a.download = 'sudoku_save.txt';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
    
    showMessage('Partida guardada correctamente.', 'success');
}

// Cargar partida
function loadGame() {
    const input = document.createElement('input');
    input.type = 'file';
    input.accept = '.txt';
    
    input.onchange = e => {
        const file = e.target.files[0];
        if (!file) return;
        
        const reader = new FileReader();
        reader.onload = event => {
            try {
                const gameData = JSON.parse(event.target.result);
                
                // Validar que el archivo tiene la estructura correcta
                if (!gameData.grid || !gameData.solution || !gameData.initialGrid) {
                    throw new Error('Archivo de guardado inválido');
                }
                
                // Actualizar el estado del juego
                gameState.level = gameData.level || 1;
                gameState.score = gameData.score || 0;
                gameState.lives = gameData.lives || 5;
                gameState.completedPuzzles = gameData.completedPuzzles || 0;
                gameState.currentDifficulty = gameData.currentDifficulty || 'very-easy';
                gameState.grid = gameData.grid;
                gameState.solution = gameData.solution;
                gameState.initialGrid = gameData.initialGrid;
                
                // Actualizar la interfaz
                updateGridDisplay();
                updateUI();
                
                // Actualizar botón de dificultad activo
                document.querySelectorAll('.difficulty-btn').forEach(btn => {
                    btn.classList.remove('active');
                    if (btn.dataset.difficulty === gameState.currentDifficulty) {
                        btn.classList.add('active');
                    }
                });
                
                showMessage('Partida cargada correctamente.', 'success');
            } catch (error) {
                showMessage('Error al cargar la partida: ' + error.message, 'error');
            }
        };
        reader.readAsText(file);
    };
    
    input.click();
}

// Manejar presión de teclas
function handleKeyPress(e) {
    if (gameState.selectedCell === null) return;
    
    const key = e.key;
    
    if (key >= '1' && key <= '9') {
        fillCell(parseInt(key));
    } else if (key === 'Backspace' || key === 'Delete' || key === '0') {
        clearCell();
    } else if (key === 'ArrowUp' && gameState.selectedCell >= 9) {
        selectCell(gameState.selectedCell - 9);
    } else if (key === 'ArrowDown' && gameState.selectedCell < 72) {
        selectCell(gameState.selectedCell + 9);
    } else if (key === 'ArrowLeft' && gameState.selectedCell % 9 !== 0) {
        selectCell(gameState.selectedCell - 1);
    } else if (key === 'ArrowRight' && gameState.selectedCell % 9 !== 8) {
        selectCell(gameState.selectedCell + 1);
    }
}

// Actualizar interfaz de usuario
function updateUI() {
    levelElement.textContent = gameState.level;
    scoreElement.textContent = gameState.score;
    difficultyElement.textContent = gameState.difficulties[gameState.currentDifficulty].name;
    
    // Actualizar vidas
    const lifeElements = document.querySelectorAll('.life');
    lifeElements.forEach((life, index) => {
        if (index < gameState.lives) {
            life.classList.remove('lost');
        } else {
            life.classList.add('lost');
        }
    });
}

// Mostrar mensaje
function showMessage(text, type) {
    messageElement.textContent = text;
    messageElement.className = `message ${type}`;
}

// Limpiar mensaje
function clearMessage() {
    messageElement.textContent = '';
    messageElement.className = 'message';
}

// Utilidades
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function shuffleArray(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
}

// Inicializar el juego cuando se carga la página
window.addEventListener('DOMContentLoaded', initGame);