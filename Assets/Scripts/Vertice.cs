using System;
using System.Collections.Generic;
using System.Text;

public class Vertice
{
    public int[,] puzzle;
    public int d, cor;
    public Vertice anc;
    public List<Vertice> adj;

    public Vertice()
    {
        puzzle = new int[3, 3];
        d = 0;
        cor = 0;
        anc = null;
        adj = new List<Vertice>();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Puzzle:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                sb.Append(puzzle[i, j] + " ");
            }
            sb.AppendLine();
        }
        sb.AppendLine($"Distância: {d}");
        sb.AppendLine($"Cor: {cor}");
        sb.AppendLine($"Ancestral: {(anc != null ? anc.ToString() : "Nenhum")}");

        sb.Append("Adjacências: ");
        foreach (var v in adj)
        {
            sb.Append(v.d + " ");
        }

        return sb.ToString();
    }
}