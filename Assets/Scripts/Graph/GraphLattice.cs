using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphLattice : SparseGraph<Vector2Int>
{
    private int width, height;

    public int Width {  get { return width; } }
    public int Height { get { return height; } }

    public bool IsInside(Vector2Int p)
    {
        return (p.x < width) && (p.y < height) && (p.x >= 0) && (p.y >= 0);
    }

    public void ConnectGrid()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector2Int w = new Vector2Int(i, j);
                Vector2Int u = new Vector2Int(i + 1, j);
                Vector2Int v = new Vector2Int(i, j + 1);
                if (IsInside(u)) Connect(w, u);
                if (IsInside(v)) Connect(w, v);
            }
        }
    }

    public GraphLattice(int width, int height)
    {
        this.width = width;
        this.height = height;
        ConnectGrid();
    }
}
