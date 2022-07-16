using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LabyrinthGenerator
{

    public static IGraph<Vector2Int> Generate(int width, int height, Vector2Int start_point)
    {
        GraphLattice pre_graph = new GraphLattice(width, height);

        Func<IEnumerable<Vector2Int>, IEnumerable<Vector2Int>> chooser =
            RandomSequence<Vector2Int>.rand_permutation;

        MarkingForGraph<Vector2Int> mfg = new MarkingForGraph<Vector2Int>(pre_graph);
        IGraph<Vector2Int> _graph = new GraphLattice(width, height);
        mfg.DepthSearchTree(ref _graph, start_point, chooser);
        
        return _graph;
    }
}
