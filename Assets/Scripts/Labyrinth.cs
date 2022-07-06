using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Labyrinth : WallBuilder
{
    [SerializeField]
    private int width, height;

    override public int Width { get { return 2*width + 1; } }
    override public int Height { get { return 2*height + 1; } }

    public override void BuildInnerWalls()
    {
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                if ((i % 2 == 0) && (j % 2 == 0))
                    BuildWall(i, j);
            }
        }
    }
}
