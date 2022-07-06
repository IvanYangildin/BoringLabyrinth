using System.Collections;
using System.Collections.Generic;
using System;


public class TreeNode<TNode>
{
    private TNode value;
    public TNode Value { get { return value; } }

    public TreeNode(TNode v)
    {
        value = v;
    }

    public TNode Parent { get; set; }
    public HashSet<TNode> Children { get; set; }
}

public class TreeGraph<TNode> : IGraph<TNode> where TNode : IComparable
{
    TreeNode<TNode> head;
    public TreeNode<TNode> Head { get { return head; } }

    Dictionary<TNode, TreeNode<TNode>> trans;
    public IEnumerable<TNode> Nodes 
    { 
        get 
        {
            return trans.Keys; 
        } 
    }

    public TreeGraph(TNode h)
    {
        head = new TreeNode<TNode>(h);
        trans[h] = head;
    }

    // return related nodes
    public IEnumerable<TNode> Related(TNode a)
    {
        return trans[a].Children;
    }

    // add edge between nodes
    // adding b to nodes if it isn't contented
    public void Connect(TNode a, TNode b)
    {
        if (trans.ContainsKey(a))
        {
            if (trans.ContainsKey(b))
                throw new Exception("try to create wrong tree, b-node is already here");
            trans[a].Children.Add(b);

            TreeNode<TNode> node = new TreeNode<TNode>(b);
            node.Parent = a;
            trans[b] = node;
        }
    }

    // delete b
    public void Disconnected(TNode a, TNode b)
    {
        trans[a].Children.Remove(b);

        HashSet<TNode> children = new HashSet<TNode>();
        children.Add(b);
        while (children.Count > 0)
        {
            HashSet<TNode> new_children = new HashSet<TNode>();
            foreach (TNode node in children)
            {
                new_children.UnionWith(trans[node].Children);
            }
            children = new_children;
        }
    }

    // delete (p,ch) edge and add (q, ch)
    public void Reconnected(TNode p, TNode ch, TNode q)
    {
        trans[p].Children.Remove(ch);
        trans[q].Children.Add(ch);
        trans[ch].Parent = q;
    }

    // return true if there is edge (a, b)
    public bool IsConnect(TNode a, TNode b)
    {
        return trans[b].Parent.Equals(b);
    }

    // delete all nodes
    public void CLear()
    {
    }
}
