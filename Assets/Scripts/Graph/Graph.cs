using System.Collections;
using System.Collections.Generic;
using System;


public interface IGraph<TNode>
{
    public IEnumerable<TNode> Nodes { get; }

    // return related nodes
    public IEnumerable<TNode> Related(TNode a);

    // add edge between nodes
    // adding nodes to others if they don't contented
    public void Connect(TNode a, TNode b);

    // remove edge between nodes
    public void Disconnected(TNode a, TNode b);

    // return true if there is edge (a, b)
    public bool IsConnect(TNode a, TNode b);

    // delete all nodes
    public void CLear();
};
