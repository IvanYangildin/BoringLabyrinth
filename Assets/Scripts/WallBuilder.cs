using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder
{
    private GameObject block;
    private GameObject father;

    private int width, height;
    private float cell_width, cell_height, cell_high;

    private GameObject[,] block_matrix;

    public GameObject Block { get { return block; } }
    public GameObject Father { get { return father; } }

    public int Width { get { return width; } }
    public int Height { get { return height; } }

    public float CellWidth { get { return cell_width; } }
    public float CellHeight { get { return cell_height; } }
    public float CellHigh { get { return cell_high; } }


    // i and j can negative
    public GameObject BuildWall(int i, int j)
    {
        if ((i < 0) || (j < 0) || (i >= Width) || (j >= Height))
        {
            return null;
        }
        if (block_matrix[i, j] == null)
        {
            Vector3 shift = new Vector3((1 - Width) * 0.5f * CellWidth, CellHigh * 0.5f, (1 - Height) * 0.5f * CellHeight);
            Vector3 pos = new Vector3(i * CellWidth, 0, j * CellHeight);

            GameObject obj = GameObject.Instantiate(Block, 
                father.transform.position + pos + shift, 
                Quaternion.identity);
            obj.transform.parent = father.transform;
            block_matrix[i, j] = obj;
        }
        return block_matrix[i, j];
    }

    public GameObject BuildWall(Vector2Int pos)
    {
        return BuildWall(pos.x, pos.y);
    }

    public WallBuilder(GameObject block, GameObject father, int width, int height, 
        float cell_width, float cell_height, float cell_high)
    {
        this.block = block;
        this.father = father;
        this.width = width;
        this.height = height;
        this.cell_width = cell_width;
        this.cell_height = cell_height;
        this.cell_high = cell_high;
        this.block_matrix = new GameObject[Width, Height];
    }

}
