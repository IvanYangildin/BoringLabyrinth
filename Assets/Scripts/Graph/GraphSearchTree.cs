using System.Collections;
using System.Collections.Generic;
using System;


public class MarkingForGraph<TNode>
{
    // marking graph
    public IGraph<TNode> Graph { get; set; }

    private Dictionary<TNode, bool> mark;

    // mark node
    public void Mark(TNode a)
    {
        mark[a] = true;
    }

    //return true if node is marked
    public bool IsMarked(TNode a)
    {
        if (mark.ContainsKey(a))
        {
            return mark[a];
        }
        return false;
    }

    // delete all marks
    public void UnmarkAll()
    {
        mark.Clear();
    }

    private void dfs(ref IGraph<TNode> tree, TNode head, 
        Func<IEnumerable<TNode>, IEnumerable<TNode>> chooser)
    {
        Mark(head);
        foreach (TNode child in chooser(Graph.Related(head)))
        {
            if (IsMarked(child))
                continue;
            tree.Connect(head, child);
            dfs(ref tree, child, chooser);
        }
    }

    // tree - where result is printed
    // head - first node to detour
    // chooser - way to order related nodes
    public void DepthSearchTree(ref IGraph<TNode> tree, TNode head, 
        Func<IEnumerable<TNode>, IEnumerable<TNode>> chooser)
    {
        tree.CLear();
        if (!(Graph is null))
            dfs(ref tree, head, chooser);
    }
}
