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
    class BTreeNode
    {
        public BTreeNode Parent { get; set; } 
        public BTreeNode[] Children { get; set; }
        public int[] Keys { get; set; }
        public int KeyCount { get; set; }

        public BTreeNode(int maxKeys, BTreeNode parent)
        {
            Parent = parent;
            Children = new BTreeNode[maxKeys + 1];
        }

        public  BTreeNode GetParent() {
            return this.Parent;
        }

        public BTreeNode[] GetChildren()
        {
            return this.Children;
        }

        public void Insert()
        {

        }
    }
}
