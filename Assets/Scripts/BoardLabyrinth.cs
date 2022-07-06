using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject Block;

    [SerializeField]
    private float cell_width, cell_height, cell_high;

    private GameObject[,] block_matrix;

    abstract public int Width { get; }
    abstract public int Height { get; }

    public float CellWidth { get { return cell_width; } }
    public float CellHeight { get { return cell_height; } }


    // i and j can negative
    public GameObject BuildWall(int i, int j)
    {
        if ((i < 0) || (j < 0) || (i >= Width) || (j >= Height))
        {
            return null;
        }
        if (block_matrix[i, j] == null)
        {
            Vector3 shift = new Vector3((1 - Width) * 0.5f * CellWidth, CellHeight * 0.5f, (1 - Height) * 0.5f * CellHeight);
            Vector3 pos = new Vector3(i * CellWidth, 0, j * CellHeight);

            GameObject obj = Instantiate(Block, transform.position + pos + shift, Quaternion.identity);
            obj.transform.parent = transform;
            block_matrix[i, j] = obj;
        }
        return block_matrix[i, j];
    }

    void BuildRegionEdge()
    {
        for (int i = 0; i < Height; ++i)
        {
            BuildWall(0, i);
            BuildWall(Width - 1, i);
        }
        for (int i = 0; i < Width; ++i)
        {
            BuildWall(i, 0);
            BuildWall(i, Height - 1);
        }
    }

    public abstract void BuildInnerWalls();

    protected void Init()
    {
        block_matrix = new GameObject[Width, Height];
        BuildRegionEdge();
        BuildInnerWalls();
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
