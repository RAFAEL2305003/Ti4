using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PlayerPuzzle : MonoBehaviour
{

    public PlayerPiece piecePrefab;

    public PlayerPiece[,] pieces = new PlayerPiece[3, 3];

    public Sprite[] sprites;

    Grafo grafo;

    // Start is called before the first frame update
    void Start()
    {
        grafo = Grafo.Instance;
        int[] array = ConverterMatrizParaArray(grafo.vertices[200].puzzle);
        Init(array);
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
                PlayerPiece piece = Instantiate(piecePrefab, new Vector2(x-6.5f, y-2.5f), Quaternion.identity);
                piece.Init(x, y, array[n], sprites[array[n]], ClickToSwap);
                pieces[x, y] = piece;
                // Debug.Log(pieces[x, y].index);
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
