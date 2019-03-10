using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    /// <summary>
    /// Implementation of a B Tree
    /// https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/BTree.cs
    /// </summary>
    class BTree
    {
        public BTreeNode RootNode { get; set; }
        public int MaxKeys { get; set; }

        public BTree(int maxKeys)
        {
            MaxKeys = maxKeys;
            RootNode = null;
        }

        public void StartInsert(int value)
        {
            // Add a root node if it doesnt exist
            if (RootNode == null)
            {
                // Initialise a new root node with the given value.
                RootNode = new BTreeNode(MaxKeys, null) { Keys = { [0] = value } };
                RootNode.KeyCount++;
            }
            else
            {
                BTreeNode insertNode = FindWhereToInsert();
                Insert(insertNode);
            }
        }

        public static void Insert(BTreeNode insertNode)
        {

        }

        public BTreeNode FindWhereToInsert()
        {
            return null;
        }
    }
}
