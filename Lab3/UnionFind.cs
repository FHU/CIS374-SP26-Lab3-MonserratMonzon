using System;
using System.Collections.Generic;

namespace Lab3;

public class UnionFind<T>
{
    private Dictionary<T,T> parent;
    private Dictionary<T, int> rank;

    public UnionFind(IEnumerable<T> elements) 
    {
        parent = new Dictionary<T,T>();
        rank = new Dictionary<T,int>();

        // initalize dictionaries 
        foreach( T element in elements)
        {
            parent[element] = element;
            rank[element] = 1;
        }
    }

    // Find the top most parent of item
    public T Find(T item)
    {
        if(!parent.ContainsKey(item))
        {
            throw new ArgumentException($"Item {item} was not in the Union-Find structure.");
        }

        if( !parent[item].Equals(item))
        {
            parent[item] = Find( parent[item]);
        }

        return parent[item];
    }

    public void Union(T item1, T item2)
    {
        T root1 = Find(item1);
        T root2 = Find(item2);

        // same group
        if( root1.Equals(root2) ) 
            return;


        if( rank[root1] > rank[root2] ) 
        {
            parent[root2] = root1;
        }
        else if( rank[root1] < rank[root2] )
        {
            parent[root1] = root2;
        }
        else {
            parent[root2] = root1;
            rank[root1]++;
        }
    }

    public bool Connected(T item1, T item2)
    {
        return Find(item1).Equals( Find(item2));
    }

    // TODO 
    /// <summary>
    /// Determines if all the items are in the same group/component 
    /// </summary>
    /// <returns>Returns true if all the items are in the same group. </returns>
    public bool AreAllConnected() 
    {
        if (parent.Count == 0)
            return true;

        T root = default;
        bool rootSet = false;
        foreach (var item in parent.Keys)
        {
            if (!rootSet)
            {
                root = Find(item);
                rootSet = true;
            }
            else if (!Find(item).Equals(root))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Resets all the items to be in "their own" group
    /// </summary>
    public void Reset()
    {
        foreach (var item in parent.Keys)
        {
            parent[item] = item;
            rank[item] = 1;
        }
    }

}
