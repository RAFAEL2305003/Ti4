using System;
using System.Collections.Generic;

public class Vertice
{
    public int[,] puzzle;
    public int d, cor;
    public Vertice anc;
    public List<Vertice> adj;

    public Vertice() {
        puzzle = new int[3, 3];
        d = 0;
        anc = null;
        adj = new List<Vertice>();
    }
}