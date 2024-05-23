using System.Collections.Generic;
using UnityEngine;

public class Grafo : MonoBehaviour
{
    public List<Vertice> vertices;
    private Dictionary<int[,], Vertice> mapaVertices;
    private int[,] referencia = ConverterArrayParaMatriz(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });

    public static Grafo Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        vertices = new List<Vertice>();
        mapaVertices = new Dictionary<int[,], Vertice>(new MatrizEqualityComparer());
    }

    void Start()
    {
        criarGrafo();
    }

    public void criarGrafo()
    {
        List<int[]> todasVariacoes = GerarTodasVariacoes();
        foreach (int[] item in todasVariacoes)
        {
            int[,] m = ConverterArrayParaMatriz(item);
            Vertice v = new Vertice(); // Certifique-se de que Vertice é um MonoBehaviour ou ajuste conforme necessário
            v.puzzle = (int[,])m.Clone();
            vertices.Add(v);
            mapaVertices.Add(v.puzzle, v);
        }
        int i = 0;
        foreach (Vertice v in vertices)
        {
            gerarDistancias(v);
            i++;
        }
    }

    public void adicionarAresta(Vertice src, Vertice dest)
    {
        if (!src.adj.Contains(dest))
        {
            src.adj.Add(dest);
        }
        if (!dest.adj.Contains(src))
        {
            dest.adj.Add(src);
        }
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
                if (matrix[i, j] == 8)
                {
                    found = true;
                }
            }
        }
        i--;
        j--;

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
            if (v != null)
            {
                adicionarAresta(v, s);
            }
        }
    }

    public Vertice encontrarParente(int[,] matrix)
    {
        if (mapaVertices.ContainsKey(matrix))
        {
            return mapaVertices[matrix];
        }
        return null;
    }

    static List<int[]> GerarTodasVariacoes()
    {
        List<int[]> todasVariacoes = new List<int[]>();
        int[] numeros = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
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
                matriz[i, j] = array[index++];
            }
        }
        return matriz;
    }

    public bool PuzzleCorreto(int[,] matrizAtual, int[,] matrizAlvo)
    {
        MatrizEqualityComparer comparer = new MatrizEqualityComparer();
        return comparer.Equals(matrizAtual, matrizAlvo);
    }

    public void BFS(Vertice u)
    {
        u.cor = 1;

        Queue<Vertice> queue = new Queue<Vertice>();
        queue.Enqueue(u);
        bool encontrado = false;

        while (queue.Count > 0 && !encontrado)
        {
            Vertice v = queue.Dequeue();
            foreach (Vertice w in v.adj)
            {
                if (w.cor == 0)
                {
                    if (PuzzleCorreto(w.puzzle, referencia))
                    {
                        w.anc = v;
                        encontrado = true;
                        break;
                    }
                    queue.Enqueue(w);
                    w.cor = 1;
                    w.d = v.d + 1;
                    w.anc = v;
                }
            }
            if (!encontrado)
            {
                v.cor = 2;
            }
        }
    }

}

public class MatrizEqualityComparer : IEqualityComparer<int[,]>
{
    public bool Equals(int[,] x, int[,] y)
    {
        if (x.GetLength(0) != y.GetLength(0) || x.GetLength(1) != y.GetLength(1))
            return false;

        for (int i = 0; i < x.GetLength(0); i++)
        {
            for (int j = 0; j < x.GetLength(1); j++)
            {
                if (x[i, j] != y[i, j])
                    return false;
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