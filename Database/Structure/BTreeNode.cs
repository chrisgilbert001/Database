using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    /// <summary>
    /// Node for my BTree
    /// https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/BTree.cs
    /// </summary>
    class BTreeNode <TKey, TValue> where TKey : IComparable
    {
        public BTreeNode<TKey, TValue> Parent { get; set; }
        public List<BTreeNode<TKey, TValue>> Children { get; set; }
        public List<BTreeKey<TKey, TValue>> Keys { get; set; }
        public int KeyCount
        {
            get
            {
                return Keys.Count;
            }
        }
        public bool IsLeaf
        {
            get
            {
                return Children.Count == 0;
            }
        }

        public BTreeNode(int maxKeys, BTreeNode<TKey, TValue> parent)
        {
            Parent = parent;
            Children = new List<BTreeNode<TKey, TValue>>();
        }
    }
}
