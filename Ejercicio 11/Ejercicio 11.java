import java.util.*;

public class HashSortSimplificado {
    
    public static void main(String[] args) {
        int[] valores = generarValoresAleatorios(15, 100);
        
        System.out.println("Valores originales:");
        System.out.println(Arrays.toString(valores));
        int[] valoresOrdenados = hashSortSimplificado(valores);
        
        System.out.println("\nValores ordenados:");
        System.out.println(Arrays.toString(valoresOrdenados));
    }
    

    public static int[] generarValoresAleatorios(int cantidad, int max) {
        Random random = new Random();
        int[] valores = new int[cantidad];
        
        for (int i = 0; i < cantidad; i++) {
            valores[i] = random.nextInt(max);
        }
        
        return valores;
    }
    public static int[] hashSortSimplificado(int[] valores) {
        if (valores.length == 0) {
            return new int[0];
        }
        int max = encontrarMaximo(valores);
        
        boolean[] tablaHash = new boolean[max + 1];
        
        for (int valor : valores) {
            if (valor >= 0 && valor < tablaHash.length) {
                tablaHash[valor] = true;
            }
        }
        
        List<Integer> ordenados = new ArrayList<>();
        for (int i = 0; i < tablaHash.length; i++) {
            if (tablaHash[i]) {
                ordenados.add(i);
            }
        }
        
        int[] resultado = new int[ordenados.size()];
        for (int i = 0; i < ordenados.size(); i++) {
            resultado[i] = ordenados.get(i);
        }
        
        return resultado;
    }
    
    public static int encontrarMaximo(int[] valores) {
        int max = valores[0];
        for (int i = 1; i < valores.length; i++) {
            if (valores[i] > max) {
                max = valores[i];
            }
        }
        return max;
    }
    
    public static int[] hashSortConTreeSet(int[] valores) {
        TreeSet<Integer> set = new TreeSet<>();
        
        for (int valor : valores) {
            set.add(valor);
        }
        
        int[] resultado = new int[set.size()];
        int index = 0;
        for (int valor : set) {
            resultado[index++] = valor;
        }
        
        return resultado;
    }
}