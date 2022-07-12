using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SparseGraph<TNode> : IGraph<TNode>
{
    private HashSet<TNode> nodes = new HashSet<TNode>();
    public IEnumerable<TNode> Nodes { get { return nodes; } }

    private Dictionary<TNode, HashSet<TNode>> edges = new Dictionary<TNode, HashSet<TNode>>();

    // return related nodes
    public IEnumerable<TNode> Related(TNode a)
    {
        if (edges.ContainsKey(a))
            return edges[a];
        else
            return new List<TNode>();
    }

    // add edge between nodes
    // adding nodes to others if they don't contented
    public void Connect(TNode a, TNode b)
    {
        Add(a);
        Add(b);
        edges[a].Add(b);
        edges[b].Add(a);
    }

    // remove edge between nodes
    public void Disconnected(TNode a, TNode b)
    {
        if (edges.ContainsKey(a))
        {
            edges[a].Remove(b);
        }
        if (edges.ContainsKey(b))
        {
            edges[b].Remove(a);
        }
    }

    // return true if there is edge (a, b)
    public bool IsConnect(TNode a, TNode b)
    {
        if (edges.ContainsKey(a))
            return edges[a].Contains(b);
        return false;
    }

    // add node to others
    public void Add(TNode a)
    {
        if (nodes.Contains(a))
            return;
        nodes.Add(a);
        edges[a] = new HashSet<TNode>();
    }

    // delete all nodes
    public void CLear()
    {
        edges.Clear();
        nodes.Clear();
    }
}
