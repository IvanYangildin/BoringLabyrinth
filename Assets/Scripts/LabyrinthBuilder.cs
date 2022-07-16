using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class LabyrinthBuilder : MonoBehaviour
{
    [SerializeField]
    private int width, height;

    [SerializeField]
    private float cell_width, cell_height, cell_high;

    [SerializeField]
    private GameObject block;

    [SerializeField]
    private GameObject ground;

    private IGraph<Vector2Int> graph;
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

    public Vector3 PhysicPosition(Vector2Int pos)
    {
        return new Vector3(pos.x * cell_width, 0, pos.y * cell_height);
    }

    private void BuildInnerWalls()
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

    private void BuildRegionEdge()
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

    private void BuildLabyrinth()
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

    private void BuildGround()
    {
        GameObject obj = GameObject.Instantiate(ground,
                transform.position,
                Quaternion.identity);
        obj.transform.parent = transform;
        obj.transform.localScale = 0.1f * new Vector3(WidthPh * cell_width, 1, HeightPh * cell_height);
    }

    public void Start()
    {
        builder = new WallBuilder(block, gameObject, WidthPh, HeightPh, 
            cell_width, cell_height, cell_high);

        graph = LabyrinthGenerator.Generate(width, height, Vector2Int.zero);

        BuildInnerWalls();
        BuildRegionEdge();
        BuildLabyrinth();
        BuildGround();
    }
}
