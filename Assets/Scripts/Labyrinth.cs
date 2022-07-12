using System;
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

    public int WidthPh { get { return 2*width + 1; } }
    public int HeightPh { get { return 2*height + 1; } }

    public Vector2Int WallCellPosition(Vector2Int a, Vector2Int b)
    {
        Vector2Int d = a - b;
        if (d.magnitude != 1f)
            return new Vector2Int(0, 0);

        return 2 * b + new Vector2Int(1,1) + d;
    }

    public void BuildInnerWalls()
    {
        for (int i = 0; i < WidthPh; ++i)
        {
            for (int j = 0; j < HeightPh; ++j)
            {
                if ((i % 2 == 0) && (j % 2 == 0))
                    builder.BuildWall(i, j);
            }
        }
    }

    public void BuildRegionEdge()
    {
        for (int i = 0; i < HeightPh; ++i)
        {
            builder.BuildWall(0, i);
            builder.BuildWall(WidthPh - 1, i);
        }
        for (int i = 0; i < WidthPh; ++i)
        {
            builder.BuildWall(i, 0);
            builder.BuildWall(i, HeightPh - 1);
        }
    }

    public void BuildLabyrinth()
    {
        Vector2Int[] dir = new Vector2Int[4];
        dir[0] = new Vector2Int(0, -1);
        dir[1] = new Vector2Int(0, 1);
        dir[2] = new Vector2Int(-1, 0);
        dir[3] = new Vector2Int(1, 0);
        
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                Vector2Int p = new Vector2Int(i, j);
                foreach (var s in dir)
                    if (!graph.IsConnect(p, p + s))
                    {
                        builder.BuildWall(WallCellPosition(p, p + s));
                    }
            }
        }
    }

    public void Start()
    {
        builder = new WallBuilder(block, gameObject, WidthPh, HeightPh, 
            cell_width, cell_height, cell_high);
        GraphLabyrinth pre_graph = new GraphLabyrinth(width, height);

        Func<IEnumerable<Vector2Int>, IEnumerable<Vector2Int>> chooser = 
            RandomSequence<Vector2Int>.rand_permutation;
        MarkingForGraph<Vector2Int> mfg = new MarkingForGraph<Vector2Int>(pre_graph);
        IGraph<Vector2Int> _graph = new GraphLabyrinth(width, height);
        mfg.DepthSearchTree(ref _graph, new Vector2Int(0,0), chooser);
        graph = (GraphLabyrinth) _graph;

        BuildInnerWalls();
        BuildRegionEdge();
        BuildLabyrinth();
    }
}
