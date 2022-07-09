using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class Labyrinth : MonoBehaviour
{
    [SerializeField]
    private int width, height;

    [SerializeField]
    private float cell_width, cell_height, cell_high;

    [SerializeField]
    private GameObject block;

    private GraphLabyrinth graph;
    private WallBuilder builder;

    public int Width { get { return 2*width + 1; } }
    public int Height { get { return 2*height + 1; } }

    public Vector2Int WallCellPosition(Vector2Int a, Vector2Int b)
    {
        Vector2Int d = a - b;
        if (d.magnitude != 1f)
            return new Vector2Int(0, 0);

        return 2 * b + new Vector2Int(1,1) + d;
    }

    public void BuildInnerWalls()
    {
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                if ((i % 2 == 0) && (j % 2 == 0))
                    builder.BuildWall(i, j);
            }
        }
    }

    public void BuildRegionEdge()
    {
        for (int i = 0; i < Height; ++i)
        {
            builder.BuildWall(0, i);
            builder.BuildWall(Width - 1, i);
        }
        for (int i = 0; i < Width; ++i)
        {
            builder.BuildWall(i, 0);
            builder.BuildWall(i, Height - 1);
        }
    }

    public void Start()
    {
        builder = new WallBuilder(block, gameObject, Width, Height, cell_width, cell_height, cell_high);
        BuildInnerWalls();
        BuildRegionEdge();
    }
}
