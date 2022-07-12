using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSequence<TNode>
{

    static public IEnumerable<TNode> default_permutation(IEnumerable<TNode> x)
    { 
        return x;
    }

    static public IEnumerable<TNode> rand_permutation(IEnumerable<TNode> x)
    {
        List<int> ind_list = new List<int>();
        for (int i = 0; i < x.Count(); ++i) ind_list.Add(i);

        List<int> re_list = new List<int>();
        for (int k = 0; k < x.Count(); ++k)
        {
            int ind = Random.Range(0, ind_list.Count);
            re_list.Add(ind_list[ind]);
            ind_list.RemoveAt(ind);
        }

        List<TNode> list_orig = x.ToList();
        List<TNode> list_res = new List<TNode>();
        string deb_line = "";
        foreach (int i in re_list)
        {
            deb_line += i.ToString() + " ";
            list_res.Add(list_orig[i]);
        }
        Debug.Log(deb_line);

        return list_res;
    }
}
