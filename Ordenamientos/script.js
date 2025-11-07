// Algoritmos de ordenamiento
const sortingAlgorithms = {
    bubble: {
        name: 'Bubble Sort',
        sort: function(arr) {
            const n = arr.length;
            for (let i = 0; i < n-1; i++) {
                for (let j = 0; j < n-i-1; j++) {
                    if (arr[j] > arr[j+1]) {
                        [arr[j], arr[j+1]] = [arr[j+1], arr[j]];
                    }
                }
            }
            return arr;
        }
    },
    
    insertion: {
        name: 'Inserción Sort',
        sort: function(arr) {
            for (let i = 1; i < arr.length; i++) {
                let key = arr[i];
                let j = i - 1;
                
                while (j >= 0 && arr[j] > key) {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
            return arr;
        }
    },
    
    selection: {
        name: 'Selección Sort',
        sort: function(arr) {
            const n = arr.length;
            
            for (let i = 0; i < n-1; i++) {
                let minIdx = i;
                for (let j = i+1; j < n; j++) {
                    if (arr[j] < arr[minIdx]) {
                        minIdx = j;
                    }
                }
                
                [arr[i], arr[minIdx]] = [arr[minIdx], arr[i]];
            }
            return arr;
        }
    },
    
    quick: {
        name: 'Quick Sort',
        sort: function(arr) {
            if (arr.length <= 1) return arr;
            
            const pivot = arr[Math.floor(arr.length / 2)];
            const left = [];
            const right = [];
            const equal = [];
            
            for (let element of arr) {
                if (element < pivot) left.push(element);
                else if (element > pivot) right.push(element);
                else equal.push(element);
            }
            
            return [...this.sort(left), ...equal, ...this.sort(right)];
        }
    },
    
    merge: {
        name: 'Merge Sort',
        sort: function(arr) {
            if (arr.length <= 1) return arr;
            
            const mid = Math.floor(arr.length / 2);
            const left = this.sort(arr.slice(0, mid));
            const right = this.sort(arr.slice(mid));
            
            return this.merge(left, right);
        },
        
        merge: function(left, right) {
            let result = [];
            let leftIndex = 0;
            let rightIndex = 0;
            
            while (leftIndex < left.length && rightIndex < right.length) {
                if (left[leftIndex] < right[rightIndex]) {
                    result.push(left[leftIndex]);
                    leftIndex++;
                } else {
                    result.push(right[rightIndex]);
                    rightIndex++;
                }
            }
            
            return result.concat(left.slice(leftIndex)).concat(right.slice(rightIndex));
        }
    },
    
    heap: {
        name: 'Heap Sort',
        sort: function(arr) {
            const n = arr.length;
            
            // Construir el heap máximo
            for (let i = Math.floor(n / 2) - 1; i >= 0; i--) {
                this.heapify(arr, n, i);
            }
            
            // Extraer elementos uno por uno del heap
            for (let i = n - 1; i > 0; i--) {
                [arr[0], arr[i]] = [arr[i], arr[0]];
                this.heapify(arr, i, 0);
            }
            
            return arr;
        },
        
        heapify: function(arr, n, i) {
            let largest = i;
            const left = 2 * i + 1;
            const right = 2 * i + 2;
            
            if (left < n && arr[left] > arr[largest]) {
                largest = left;
            }
            
            if (right < n && arr[right] > arr[largest]) {
                largest = right;
            }
            
            if (largest !== i) {
                [arr[i], arr[largest]] = [arr[largest], arr[i]];
                this.heapify(arr, n, largest);
            }
        }
    },
    
    hash: {
        name: 'Hash Sort',
        sort: function(arr) {
            // Implementación simplificada de Hash Sort
            const max = Math.max(...arr);
            const min = Math.min(...arr);
            const range = max - min + 1;
            
            // Crear tabla hash
            const hashTable = Array(range).fill(0);
            
            // Contar ocurrencias
            for (let i = 0; i < arr.length; i++) {
                hashTable[arr[i] - min]++;
            }
            
            // Reconstruir el array ordenado
            let index = 0;
            for (let i = 0; i < range; i++) {
                while (hashTable[i] > 0) {
                    arr[index] = i + min;
                    index++;
                    hashTable[i]--;
                }
            }
            
            return arr;
        }
    },
    
    bucket: {
        name: 'Bucket Sort',
        sort: function(arr) {
            if (arr.length === 0) return arr;
            
            // Determinar valores mínimo y máximo
            let min = arr[0];
            let max = arr[0];
            for (let i = 1; i < arr.length; i++) {
                if (arr[i] < min) min = arr[i];
                else if (arr[i] > max) max = arr[i];
            }
            
            // Crear buckets
            const bucketCount = Math.floor(Math.sqrt(arr.length));
            const buckets = Array(bucketCount);
            for (let i = 0; i < bucketCount; i++) {
                buckets[i] = [];
            }
            
            // Distribuir valores en los buckets
            const range = (max - min + 1) / bucketCount;
            for (let i = 0; i < arr.length; i++) {
                const bucketIndex = Math.floor((arr[i] - min) / range);
                buckets[bucketIndex].push(arr[i]);
            }
            
            // Ordenar cada bucket y concatenar
            let sorted = [];
            for (let i = 0; i < bucketCount; i++) {
                if (buckets[i].length > 0) {
                    // Usar insertion sort para ordenar cada bucket
                    this.insertionSort(buckets[i]);
                    sorted = sorted.concat(buckets[i]);
                }
            }
            
            return sorted;
        },
        
        insertionSort: function(arr) {
            for (let i = 1; i < arr.length; i++) {
                let key = arr[i];
                let j = i - 1;
                
                while (j >= 0 && arr[j] > key) {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
            return arr;
        }
    },
    
    radix: {
        name: 'Radix Sort',
        sort: function(arr) {
            if (arr.length === 0) return arr;
            
            // Encontrar el número máximo para saber el número de dígitos
            const max = Math.max(...arr);
            
            // Hacer counting sort para cada dígito
            for (let exp = 1; Math.floor(max / exp) > 0; exp *= 10) {
                this.countingSort(arr, exp);
            }
            
            return arr;
        },
        
        countingSort: function(arr, exp) {
            const n = arr.length;
            const output = Array(n);
            const count = Array(10).fill(0);
            
            // Contar ocurrencias de cada dígito
            for (let i = 0; i < n; i++) {
                const digit = Math.floor(arr[i] / exp) % 10;
                count[digit]++;
            }
            
            // Cambiar count[i] para que contenga la posición actual
            for (let i = 1; i < 10; i++) {
                count[i] += count[i - 1];
            }
            
            // Construir el array de salida
            for (let i = n - 1; i >= 0; i--) {
                const digit = Math.floor(arr[i] / exp) % 10;
                output[count[digit] - 1] = arr[i];
                count[digit]--;
            }
            
            // Copiar el array de salida al array original
            for (let i = 0; i < n; i++) {
                arr[i] = output[i];
            }
        }
    }
};

// Generadores de arrays
const arrayGenerators = {
    sorted: (size) => {
        const arr = [];
        for (let i = 0; i < size; i++) {
            arr.push(i);
        }
        return arr;
    },
    
    partially: (size) => {
        const arr = [];
        for (let i = 0; i < size; i++) {
            arr.push(i);
        }
        
        // Desordenar parcialmente (intercambiar algunos elementos)
        for (let i = 0; i < size / 10; i++) {
            const idx1 = Math.floor(Math.random() * size);
            const idx2 = Math.floor(Math.random() * size);
            [arr[idx1], arr[idx2]] = [arr[idx2], arr[idx1]];
        }
        
        return arr;
    },
    
    reversed: (size) => {
        const arr = [];
        for (let i = size - 1; i >= 0; i--) {
            arr.push(i);
        }
        return arr;
    }
};

// Variables globales para almacenar resultados
let results = {
    byMethod: {},
    byScenario: {}
};

// Inicializar la aplicación
document.addEventListener('DOMContentLoaded', function() {
    // Configurar event listeners
    document.getElementById('runTests').addEventListener('click', runTests);
    document.getElementById('resetResults').addEventListener('click', resetResults);
    
    // Inicializar gráfico
    initializeChart();
});

// Función para ejecutar las pruebas
async function runTests() {
    // Mostrar indicador de carga
    document.getElementById('loading').style.display = 'block';
    
    // Obtener configuraciones seleccionadas
    const selectedAlgorithms = getSelectedAlgorithms();
    const selectedSizes = getSelectedSizes();
    const selectedScenarios = getSelectedScenarios();
    
    // Limpiar resultados anteriores
    results = {
        byMethod: {},
        byScenario: {}
    };
    
    // Inicializar estructuras de resultados
    for (const algo of selectedAlgorithms) {
        results.byMethod[algo] = {
            name: sortingAlgorithms[algo].name,
            times: [],
            totalTime: 0
        };
    }
    
    for (const scenario of selectedScenarios) {
        results.byScenario[scenario] = {};
        for (const size of selectedSizes) {
            results.byScenario[scenario][size] = {};
        }
    }
    
    // Ejecutar pruebas para cada combinación
    for (const scenario of selectedScenarios) {
        for (const size of selectedSizes) {
            // Generar array para este escenario y tamaño
            const testArray = arrayGenerators[scenario](size);
            
            for (const algo of selectedAlgorithms) {
                // Crear una copia del array para no modificar el original
                const arrayCopy = [...testArray];
                
                // Medir tiempo de ejecución
                const startTime = performance.now();
                sortingAlgorithms[algo].sort(arrayCopy);
                const endTime = performance.now();
                const executionTime = endTime - startTime;
                
                // Almacenar resultados
                results.byMethod[algo].times.push(executionTime);
                results.byMethod[algo].totalTime += executionTime;
                
                results.byScenario[scenario][size][algo] = executionTime;
            }
        }
    }
    
    // Procesar resultados
    processResults();
    
    // Actualizar la interfaz
    updateResultsTables();
    updateChart();
    
    // Ocultar indicador de carga
    document.getElementById('loading').style.display = 'none';
}

// Función para obtener algoritmos seleccionados
function getSelectedAlgorithms() {
    const algorithms = [];
    const checkboxes = document.querySelectorAll('.control-group:nth-child(1) input[type="checkbox"]');
    
    checkboxes.forEach(checkbox => {
        if (checkbox.checked) {
            algorithms.push(checkbox.id);
        }
    });
    
    return algorithms;
}

// Función para obtener tamaños seleccionados
function getSelectedSizes() {
    const sizes = [];
    const checkboxes = document.querySelectorAll('.control-group:nth-child(2) input[type="checkbox"]');
    
    checkboxes.forEach(checkbox => {
        if (checkbox.checked) {
            sizes.push(parseInt(checkbox.id.replace('size', '')));
        }
    });
    
    return sizes;
}

// Función para obtener escenarios seleccionados
function getSelectedScenarios() {
    const scenarios = [];
    const checkboxes = document.querySelectorAll('.control-group:nth-child(3) input[type="checkbox"]');
    
    checkboxes.forEach(checkbox => {
        if (checkbox.checked) {
            scenarios.push(checkbox.id);
        }
    });
    
    return scenarios;
}

// Función para procesar resultados
function processResults() {
    // Calcular promedios por método
    for (const algo in results.byMethod) {
        const algoData = results.byMethod[algo];
        algoData.averageTime = algoData.totalTime / algoData.times.length;
        
        // Encontrar mejor y peor tiempo
        algoData.bestTime = Math.min(...algoData.times);
        algoData.worstTime = Math.max(...algoData.times);
    }
    
    // Procesar resultados por escenario
    for (const scenario in results.byScenario) {
        for (const size in results.byScenario[scenario]) {
            const scenarioData = results.byScenario[scenario][size];
            
            // Encontrar mejor y peor algoritmo para este escenario/tamaño
            let bestAlgo = null;
            let worstAlgo = null;
            let bestTime = Infinity;
            let worstTime = -Infinity;
            
            for (const algo in scenarioData) {
                const time = scenarioData[algo];
                
                if (time < bestTime) {
                    bestTime = time;
                    bestAlgo = algo;
                }
                
                if (time > worstTime) {
                    worstTime = time;
                    worstAlgo = algo;
                }
            }
            
            results.byScenario[scenario][size].bestAlgo = bestAlgo;
            results.byScenario[scenario][size].worstAlgo = worstAlgo;
            results.byScenario[scenario][size].difference = worstTime - bestTime;
        }
    }
}

// Función para actualizar las tablas de resultados
function updateResultsTables() {
    // Actualizar tabla de resultados por método
    const methodTable = document.getElementById('methodResults').getElementsByTagName('tbody')[0];
    methodTable.innerHTML = '';
    
    // Crear array de métodos para ordenar
    const methodsArray = [];
    for (const algo in results.byMethod) {
        methodsArray.push({
            id: algo,
            ...results.byMethod[algo]
        });
    }
    
    // Ordenar por tiempo promedio
    methodsArray.sort((a, b) => a.averageTime - b.averageTime);
    
    // Llenar la tabla
    methodsArray.forEach(method => {
        const row = methodTable.insertRow();
        row.insertCell(0).textContent = method.name;
        row.insertCell(1).textContent = method.averageTime.toFixed(4);
        row.insertCell(2).textContent = method.bestTime.toFixed(4);
        row.insertCell(3).textContent = method.worstTime.toFixed(4);
    });
    
    // Actualizar tabla de resultados por escenario
    const scenarioTable = document.getElementById('scenarioResults').getElementsByTagName('tbody')[0];
    scenarioTable.innerHTML = '';
    
    for (const scenario in results.byScenario) {
        for (const size in results.byScenario[scenario]) {
            const scenarioData = results.byScenario[scenario][size];
            
            // Solo mostrar si tenemos datos válidos
            if (scenarioData.bestAlgo && scenarioData.worstAlgo) {
                const row = scenarioTable.insertRow();
                row.insertCell(0).textContent = `${getScenarioName(scenario)} (${size} elementos)`;
                row.insertCell(1).textContent = sortingAlgorithms[scenarioData.bestAlgo].name;
                row.insertCell(2).textContent = sortingAlgorithms[scenarioData.worstAlgo].name;
                row.insertCell(3).textContent = scenarioData.difference.toFixed(4) + ' ms';
            }
        }
    }
}

// Función para obtener el nombre legible de un escenario
function getScenarioName(scenario) {
    const names = {
        sorted: 'Ordenado',
        partially: 'Mediamente ordenado',
        reversed: 'Inverso'
    };
    
    return names[scenario] || scenario;
}

// Función para inicializar el gráfico
function initializeChart() {
    const ctx = document.getElementById('performanceChart').getContext('2d');
    window.performanceChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [],
            datasets: []
        },
        options: {
            responsive: true,
            plugins: {
                title: {
                    display: true,
                    text: 'Tiempo de Ejecución por Algoritmo y Escenario'
                },
                legend: {
                    position: 'top',
                }
            },
            scales: {
                x: {
                    title: {
                        display: true,
                        text: 'Algoritmos'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Tiempo (ms)'
                    },
                    beginAtZero: true
                }
            }
        }
    });
}

// Función para actualizar el gráfico
function updateChart() {
    if (!window.performanceChart) return;
    
    // Obtener algoritmos seleccionados
    const selectedAlgorithms = getSelectedAlgorithms();
    
    // Preparar datos para el gráfico
    const labels = selectedAlgorithms.map(algo => sortingAlgorithms[algo].name);
    const datasets = [];
    
    // Obtener escenarios seleccionados
    const selectedScenarios = getSelectedScenarios();
    
    // Colores para los diferentes escenarios
    const colors = {
        sorted: 'rgba(54, 162, 235, 0.7)',
        partially: 'rgba(255, 206, 86, 0.7)',
        reversed: 'rgba(255, 99, 132, 0.7)'
    };
    
    // Para cada escenario, crear un dataset
    for (const scenario of selectedScenarios) {
        const scenarioData = [];
        
        for (const algo of selectedAlgorithms) {
            // Calcular el tiempo promedio para este algoritmo en este escenario
            let totalTime = 0;
            let count = 0;
            
            for (const size in results.byScenario[scenario]) {
                if (results.byScenario[scenario][size][algo]) {
                    totalTime += results.byScenario[scenario][size][algo];
                    count++;
                }
            }
            
            const avgTime = count > 0 ? totalTime / count : 0;
            scenarioData.push(avgTime);
        }
        
        datasets.push({
            label: getScenarioName(scenario),
            data: scenarioData,
            backgroundColor: colors[scenario] || 'rgba(201, 203, 207, 0.7)',
            borderColor: colors[scenario] ? colors[scenario].replace('0.7', '1') : 'rgba(201, 203, 207, 1)',
            borderWidth: 1
        });
    }
    
    // Actualizar el gráfico
    window.performanceChart.data.labels = labels;
    window.performanceChart.data.datasets = datasets;
    window.performanceChart.update();
}

// Función para limpiar resultados
function resetResults() {
    results = {
        byMethod: {},
        byScenario: {}
    };
    
    // Limpiar tablas
    document.getElementById('methodResults').getElementsByTagName('tbody')[0].innerHTML = '';
    document.getElementById('scenarioResults').getElementsByTagName('tbody')[0].innerHTML = '';
    
    // Limpiar gráfico
    if (window.performanceChart) {
        window.performanceChart.data.labels = [];
        window.performanceChart.data.datasets = [];
        window.performanceChart.update();
    }
}