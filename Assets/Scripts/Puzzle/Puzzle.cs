using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Puzzle : MonoBehaviour
{

    public Piece piecePrefab;

    public Piece[,] pieces = new Piece[3, 3];

    public Sprite[] sprites;

    Grafo grafo;

    // Start is called before the first frame update
    void Start()
    {
        grafo = Grafo.Instance;
        grafo.BFS(grafo.vertices[200]);
        List<Vertice> vs = new List<Vertice>();
        Vertice v = grafo.vertices[0];
        vs.Insert(0, v);

        while (v.anc != null)
        {
            v = v.anc;
            vs.Insert(0, v);
        }

        int[] array = ConverterMatrizParaArray(vs[0].puzzle);
        Init(array);
        StartCoroutine(PlaySolution(vs));

    }

    IEnumerator PlaySolution(List<Vertice> vs)
    {
        // Aguarda 1 segundo antes de continuar para a próxima iteração
        yield return new WaitForSeconds(1.2f);
        for (int k = 0; k < vs.Count - 1; k++)
        {
            bool found = false;
            int i = 0, j = 0;
            for (i = 0; i < 3 && !found; i++)
            {
                for (j = 0; j < 3 && !found; j++)
                {
                    if (vs[k].puzzle[i, j] == 8)
                    {
                        found = true;
                    }
                }
            }
            i--;
            j--;

            int ind = vs[k + 1].puzzle[i, j];

            found = false;
            for (int m = 0; m < 3 && !found; m++)
            {
                for (int n = 0; n < 3 && !found; n++)
                {
                    if (vs[k].puzzle[m, n] == ind)
                    {
                        i = m;
                        j = n;
                        found = true;
                    }
                }
            }

            // Transpor coordenadas para corresponder ao sistema de coordenadas em `pieces`
            int x = j;
            int y = 2 - i;

            ClickToSwap(x, y);

            // Aguarda 1 segundo antes de continuar para a próxima iteração
            yield return new WaitForSeconds(1.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init(int[] array)
    {
        int n = 0;
        for (int y = 2; y >= 0; y--)
        {
            for (int x = 0; x < 3; x++)
            {
                Piece piece = Instantiate(piecePrefab, new Vector2(x + 4.5f, y - 2), Quaternion.identity);
                piece.Init(x, y, array[n], sprites[array[n]], ClickToSwap);
                pieces[x, y] = piece;
                n++;
            }
        }
    }

    void ClickToSwap(int x, int y)
    {

        int dx = getDx(x, y);
        int dy = getDy(x, y);



        var from = pieces[x, y];
        var target = pieces[x + dx, y + dy];

        pieces[x, y] = target;
        pieces[x + dx, y + dy] = from;

        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);
    }

    int getDx(int x, int y)
    {
        if (x < 2 && pieces[x + 1, y].IsEmpty())
        {
            return 1;
        }

        if (x > 0 && pieces[x - 1, y].IsEmpty())
        {
            return -1;
        }

        return 0;
    }

    int getDy(int x, int y)
    {
        if (y < 2 && pieces[x, y + 1].IsEmpty())
        {
            return 1;
        }

        if (y > 0 && pieces[x, y - 1].IsEmpty())
        {
            return -1;
        }

        return 0;
    }

    public static int[] ConverterMatrizParaArray(int[,] matriz)
    {
        int numRows = matriz.GetLength(0);
        int numCols = matriz.GetLength(1);
        int[] array = new int[numRows * numCols];

        int index = 0;
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                array[index++] = matriz[i, j];
            }
        }

        return array;
    }

}