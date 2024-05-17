/*using System;
using System.Collections.Generic;

public class Grafo
{
    private List<Vertice> vertices;
    private Dictionary<int[,], Vertice> mapaVertices;

    public static void Main()
    {
        Grafo a = new Grafo();
        a.criarGrafo();
    }

    public Grafo()
    {
        vertices = new List<Vertice>();
        mapaVertices = new Dictionary<int[,], Vertice>(new MatrizEqualityComparer());
    }

    public void criarGrafo()
    {
        List<int[]> todasVariacoes = GerarTodasVariacoes();
        foreach (int[] item in todasVariacoes)
        {
            int[,] m = ConverterArrayParaMatriz(item);
            Vertice v = new Vertice();
            v.puzzle = (int[,])m.Clone();
            vertices.Add(v);
            mapaVertices.Add(v.puzzle, v); // Adiciona ao dicionário
        }
        int i = 0;
        foreach (Vertice v in vertices)
        {
            Console.WriteLine(i);
            gerarDistancias(v);
            i++;
        }
    }

    public void adicionarAresta(Vertice src, Vertice dest)
    {
        src.adj.Add(dest);
        dest.adj.Add(src);
    }

    public void gerarDistancias(Vertice s)
    {
        int i = 0, j = 0, aux = 0;
        bool found = false;
        int[,] matrix = (int[,])s.puzzle.Clone();
        // Procurando a posição do número 0
        for (i = 0; i < 3 && !found; i++)
        {
            for (j = 0; j < 3 && !found; j++)
            {
                if (matrix[i, j] == 0)
                {
                    found = true;
                }
            }
        }
        i--;
        j--;

        // Encontrando a distância Hamming de 1
        List<int[,]> parentes = new List<int[,]>();

        if (i + 1 <= 2)
        {
            parentes.Add((int[,])matrix.Clone());
            parentes[aux][i, j] = matrix[i + 1, j];
            parentes[aux][i + 1, j] = matrix[i, j];
            aux++;
        }
        if (i - 1 >= 0)
        {
            parentes.Add((int[,])matrix.Clone());
            parentes[aux][i, j] = matrix[i - 1, j];
            parentes[aux][i - 1, j] = matrix[i, j];
            aux++;
        }
        if (j + 1 <= 2)
        {
            parentes.Add((int[,])matrix.Clone());
            parentes[aux][i, j + 1] = matrix[i, j];
            parentes[aux][i, j] = matrix[i, j + 1];
            aux++;
        }
        if (j - 1 >= 0)
        {
            parentes.Add((int[,])matrix.Clone());
            parentes[aux][i, j - 1] = matrix[i, j];
            parentes[aux][i, j] = matrix[i, j - 1];
        }

        foreach (int[,] item in parentes)
        {
            Vertice v = encontrarParente(item);
            adicionarAresta(v, s);
        }
    }

    public Vertice encontrarParente(int[,] matrix)
    {
        if (mapaVertices.ContainsKey(matrix))
        {
            return mapaVertices[matrix];
        }
        return null; // Se não encontrado
    }

    static List<int[]> GerarTodasVariacoes()
    {
        List<int[]> todasVariacoes = new List<int[]>();

        // Números de 0 a 8 representando as peças
        int[] numeros = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        // Gerar todas as permutações possíveis dos números de 0 a 8
        GerarPermutacoesRecursivas(numeros, 0, todasVariacoes);

        return todasVariacoes;
    }

    static void GerarPermutacoesRecursivas(int[] numeros, int inicio, List<int[]> todasVariacoes)
    {
        if (inicio >= numeros.Length)
        {
            todasVariacoes.Add((int[])numeros.Clone());
            return;
        }

        for (int i = inicio; i < numeros.Length; i++)
        {
            TrocarElementos(numeros, inicio, i);
            GerarPermutacoesRecursivas(numeros, inicio + 1, todasVariacoes);
            TrocarElementos(numeros, inicio, i);
        }
    }

    static void TrocarElementos(int[] numeros, int indiceA, int indiceB)
    {
        int temp = numeros[indiceA];
        numeros[indiceA] = numeros[indiceB];
        numeros[indiceB] = temp;
    }

    static int[,] ConverterArrayParaMatriz(int[] array)
    {
        int[,] matriz = new int[3, 3];
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                matriz[i, j] = array[index];
                index++;
            }
        }
        return matriz;
    }
}

// Classe auxiliar para comparar matrizes
public class MatrizEqualityComparer : IEqualityComparer<int[,]>
{
    public bool Equals(int[,] x, int[,] y)
    {
        if (x.GetLength(0) != y.GetLength(0) || x.GetLength(1) != y.GetLength(1))
        {
            return false;
        }

        for (int i = 0; i < x.GetLength(0); i++)
        {
            for (int j = 0; j < x.GetLength(1); j++)
            {
                if (x[i, j] != y[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public int GetHashCode(int[,] obj)
    {
        unchecked
        {
            int hash = 17;
            for (int i = 0; i < obj.GetLength(0); i++)
            {
                for (int j = 0; j < obj.GetLength(1); j++)
                {
                    hash = hash * 23 + obj[i, j].GetHashCode();
                }
            }
            return hash;
        }
    }
}
*/