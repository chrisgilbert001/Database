using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    /// <summary>
    /// Implementation of a B Tree
    /// Code inspiration from sources for B-Tree:
    /// https://github.com/justcoding121/Advanced-Algorithms/blob/master/src/Advanced.Algorithms/DataStructures/Tree/BTree.cs
    /// https://github.com/rsdcastro/btree-dotnet/blob/master/BTree/Entry.cs
    /// </summary>
    class BTree<TKey, TValue> where TKey : IComparable
    {
        public BTreeNode<TKey, TValue> RootNode { get; set; }
        public int MaxKeys { get; set; }
        public int MinKeys { get; set; }
        public string name;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxKeys"></param>
        public BTree(int maxKeys)
        {
            MaxKeys = maxKeys;
            MinKeys = maxKeys / 2;
            RootNode = null;
        }

        /// <summary>
        /// Base insert method do add stuff.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pointer"></param>
        public void Insert(TKey value, TValue pointer)
        {
            // Add a root node if it doesnt exist
            if (RootNode == null)
            {
                // Initialise a new root node with the given value.
                BTreeKey<TKey, TValue> newKey = new BTreeKey<TKey, TValue>() { Key = value, Pointer = pointer };
                RootNode = new BTreeNode<TKey, TValue>(MaxKeys, null) { Keys = new List<BTreeKey<TKey, TValue>>() { newKey } };
            }
            // If there is a root node already
            else
            {
                BTreeNode<TKey, TValue> insertNode = FindWhereToInsert(value, RootNode);

                InsertIntoNode(value, pointer, insertNode);
            }
        }
        
        public BTreeKey<TKey, TValue> Search(TKey search, BTreeNode<TKey, TValue> node)
        {
            int i = 0;
            int n = node.KeyCount;

            // Find where the entry would be placed in an ordered list.
            while (i < n && node.Keys[i].Key.CompareTo(search) < 0)
            {
                i++;
            }

            if (i < n)
            {
                if (node.Keys[i].Equals(search))
                {
                    return node.Keys[i];
                }
            }

            if (node.IsLeaf == true)
            {
                throw new Exception();
            }

            return Search(search, node.Children[i]);
        }

        /// <summary>
        /// Helper method which finds the node where we need to insert the new key
        /// </summary>
        /// <param name="search"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public BTreeNode<TKey, TValue> FindWhereToInsert(TKey search, BTreeNode<TKey, TValue> node)
        {
            if (node.IsLeaf == true)
            {
                for (int i = 0; i < node.KeyCount; i++)
                {
                    // if we find the key then we cant add it again
                    if (node.Keys[i].Equals(search))
                    {
                        // throw new Exception();
                        return null;
                    }
                }
                // If we are in a leaf node and we don't find it then this is where to add it
                return node;
            }
            else
            {

                int i = 0;
                int n = node.KeyCount;

                while (i < n && node.Keys[i].Key.CompareTo(search) < 0)
                {
                    i++;
                }

                if (i < n)
                {
                    if (node.Keys[i].Equals(search))
                    {
                        throw new Exception();
                    }
                    else
                    {
                        return FindWhereToInsert(search, node.Children[i]);
                    }
                }
                else
                {
                    return FindWhereToInsert(search, node.Children[i]);
                }
            }
        }
        


        /// <summary>
        /// Insert a value into the node
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pointer"></param>
        /// <param name="node"></param>
        public void InsertIntoNode(TKey value, TValue pointer, BTreeNode<TKey, TValue> node)
        {
            if (node == null)
            { return; }

            int i = 0;
            int n = node.KeyCount;

            while (i < n && node.Keys[i].Key.CompareTo(value) < 0)
            {
                i++;
            }

            node.Keys.Insert(i, new BTreeKey<TKey, TValue>() { Key = value, Pointer = pointer });
           

            // If the node is full
            if (node.KeyCount > MaxKeys)
            {
                Split(node);
            }
        }

        /// <summary>
        /// Insert an existing key into a node
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pointer"></param>
        /// <param name="node"></param>
        public void InsertIntoNode(BTreeKey<TKey, TValue> key, BTreeNode<TKey, TValue> node)
        {
            if (node.KeyCount == 0)
            {
                node.Keys.Insert(0, key);
            }
            else
            {
                if (node.Keys.Last().CompareTo(key) < 0)
                {
                    node.Keys.Insert(node.KeyCount, key);
                }
                else
                {
                    // Find where to insert the new key
                    for (int i = 0; i < node.KeyCount; i++)
                    {
                        if (node.Keys[i].CompareTo(key) > 0)
                        {
                            node.Keys.Insert(i, key);
                            break;
                        }
                    }
                }

                // If the node is full
                if (node.KeyCount > MaxKeys)
                {
                    Split(node);
                }
            }
        }

        /// <summary>
        /// Splits a child node
        /// </summary>
        public void Split(BTreeNode<TKey, TValue> node)
        {
            BTreeNode<TKey, TValue> newNode = new BTreeNode<TKey, TValue>(MaxKeys, node.Parent) { Keys = new List<BTreeKey<TKey, TValue>>() };
            // Find median and place above
            BTreeKey<TKey, TValue> median = node.Keys[MaxKeys / 2];

            // If it is the root node we need to create a new root
            if (node.Parent == null)
            {
                BTreeNode<TKey, TValue> newRoot = new BTreeNode<TKey, TValue>(MaxKeys, null) { Keys = new List<BTreeKey<TKey, TValue>>(), Children = new List<BTreeNode<TKey, TValue>>() };

                newRoot.Children.Add(node);

                node.Parent = newRoot;

                RootNode = newRoot;
            }

            node.Parent.Children.Add(newNode);
            newNode.Parent = node.Parent;

            // Remove the median
            node.Keys.RemoveAt(MaxKeys / 2);

            // Add it to the new node and remove from previous
            for (int i = (MaxKeys / 2); i < node.KeyCount; i = (MaxKeys / 2))
            {
                newNode.Keys.Add(node.Keys[i]);
                node.Keys.Remove(node.Keys[i]);
            }

            // Add it to the new node and remove from previous
            for (int i = node.KeyCount + 1; i < node.Children.Count; i = node.KeyCount + 1)
            {
                newNode.Children.Add(node.Children[i]);
                node.Children[i].Parent = newNode;
                node.Children.Remove(node.Children[i]);
            }

            // Insert the median value into the parent
            InsertIntoNode(median, node.Parent);
        }

        /// <summary>
        /// Delete from the Tree
        /// </summary>
        /// <param name="search"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public BTreeNode<TKey, TValue> DeleteFromTree(TKey search, BTreeNode<TKey, TValue> node)
        {
            if (node.IsLeaf == true)
                return DeleteFromLeaf(search, node);
            else
            {
                for (int i = 0; i < node.KeyCount; i++)
                {
                    // if we find the key delete it
                    if (node.Keys[i].Equals(search))
                    {
                        DeleteFromInternal(search, node, i);
                        return null;
                    }
                    // If it is less than the key we need to check it's child
                    else if (node.Keys[i].Key.CompareTo(search) > 0)
                    {
                        return DeleteFromTree(search, node.Children[i]);
                    }
                }
                // Check the last child 
                return DeleteFromTree(search, node.Children.Last());
            }
        }

        /// <summary>
        /// Delete a value from the leaf node
        /// </summary>
        /// <param name="search"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public BTreeNode<TKey, TValue> DeleteFromLeaf(TKey search, BTreeNode<TKey, TValue> node)
        {
            BTreeKey<TKey, TValue> key = Search(search, node);
            if (key != null)
            {
                node.Keys.Remove(key);
                if (node.KeyCount < MinKeys)
                {
                    Balance(node);
                }
                return null;
            }
            // If we are in a leaf node and we don't find it then it doesn't exist
            else
                throw new Exception();
        }

        /// <summary>
        /// Returns a specific keys index from a node
        /// </summary>
        /// <param name="search"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public int GetKeyIndex(TKey search, BTreeNode<TKey, TValue> node)
        {
            return node.Keys.FindIndex(x => x.Key.Equals(search));
        }       

        /// <summary>
        /// Returns a specific nodes index
        /// </summary>
        /// <param name="search"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public int GetNodeIndex(BTreeNode<TKey, TValue> node)
        {
            return node.Parent.Children.FindIndex(x => x.Equals(node));
        }

        /// <summary>
        /// Delete a key from an internal node
        /// </summary>
        public void DeleteFromInternal(TKey search, BTreeNode<TKey, TValue> node, int index)
        {
            int i = index;
            BTreeKey<TKey, TValue> key = null;
            TreeAndNode wrapper = GetMaxFromSubTree(node.Children[i]);
            node.Keys[i] = wrapper.Key;

            if (wrapper.Node.KeyCount < MinKeys)
            {
                Balance(wrapper.Node);
            }
        }

        /// <summary>
        /// Get the maximum value from a sub tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public TreeAndNode GetMaxFromSubTree(BTreeNode<TKey, TValue> node)
        {
            if (node.IsLeaf == true)
            {
                TreeAndNode wrapper = new TreeAndNode();
                wrapper.Key = node.Keys.Last();
                wrapper.Node = node;
                node.Keys.Remove(wrapper.Key);
                return wrapper;
            }
            else
            {
                return GetMaxFromSubTree(node.Children.Last());
            }
        }
        
        /// <summary>
        /// Balances the sub tree after a deletion
        /// </summary>
        /// <param name="node"></param>
        public void Balance(BTreeNode<TKey, TValue> node)
        {
            // Get the index of the node
            int nodeIndex = GetNodeIndex(node);

            BTreeNode<TKey, TValue> rightNode = null;
            BTreeNode<TKey, TValue> leftNode = null;

            // If it is the root then do nothing
            if (node.Parent == null)
            {
                return;
            }

            if (nodeIndex + 1 < node.Parent.Children.Count)
            {
                rightNode = node.Parent.Children[nodeIndex + 1];
                // If there is spare in the right sibling
                if (node.Parent.Children[nodeIndex + 1].KeyCount > MinKeys)
                {
                    RotateLeft(node, nodeIndex, rightNode);
                    return;
                }
            }
            else
            {
                rightNode = null;
            }

            if (nodeIndex - 1 >= 0)
            {
                leftNode = node.Parent.Children[nodeIndex - 1];
                // If there is spare in the left sibling
                if (node.Parent.Children[nodeIndex - 1].KeyCount > MinKeys)
                {
                    RotateRight(node, nodeIndex, leftNode);
                    return;
                }
            }
            else
            {
                leftNode = null;
            }

            if(true)
            {
                Merge(node, nodeIndex, leftNode, rightNode);
                return;
            }
        }

        /// <summary>
        /// Rotate left to balance the tree.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeIndex"></param>
        /// <param name="rightNode"></param>
        public void RotateLeft(BTreeNode<TKey, TValue> node, int nodeIndex, BTreeNode<TKey, TValue> rightNode)
        {
            int newIndex = GetSepIndex(nodeIndex, node);
            node.Keys.Add(node.Parent.Keys[newIndex]);

            node.Parent.Keys[newIndex] = rightNode.Keys[0];
            rightNode.Keys.RemoveAt(0);
        }

        /// <summary>
        /// Rotate right to balance the tree
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeIndex"></param>
        /// <param name="leftNode"></param>
        public void RotateRight(BTreeNode<TKey, TValue> node, int nodeIndex, BTreeNode<TKey, TValue> leftNode)
        {
            int newIndex = GetSepIndex(nodeIndex, node);
            node.Keys.Add(node.Parent.Keys[newIndex]);

            node.Parent.Keys[newIndex] = leftNode.Keys.Last();
            leftNode.Keys.RemoveAt(leftNode.Keys.Count - 1);
        }

        /// <summary>
        /// Merge two siblings together
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeIndex"></param>
        /// <param name="leftNode"></param>
        /// <param name="rightNode"></param>
        public void Merge(BTreeNode<TKey, TValue> node, int nodeIndex, BTreeNode<TKey, TValue> leftNode, BTreeNode<TKey, TValue> rightNode)
        {
            
            int newIndex = GetSepIndex(nodeIndex, node);
            if (nodeIndex == 0)
            {
                leftNode = node;
            }
            else
            {
                rightNode = node;
            }
            leftNode.Keys.Add(node.Parent.Keys[newIndex]);
            leftNode.Keys.AddRange(rightNode.Keys);

            foreach (BTreeNode<TKey, TValue> child in rightNode.Children)
            {
                child.Parent = leftNode;
            }

            leftNode.Children.AddRange(rightNode.Children);

            node.Parent.Keys.RemoveAt(newIndex);
            node.Parent.Children.Remove(rightNode);
            rightNode = null;

            if (leftNode.Parent.KeyCount < MinKeys && leftNode.Parent.Parent != null)
            {
                Balance(leftNode.Parent);
            }
            else if (leftNode.Parent.KeyCount < MinKeys && leftNode.Parent.Parent == null)
            {
                leftNode.Parent = null;
                RootNode = leftNode;
            }
        }

        /// <summary>
        /// Gets the index of the seperator to perform rotation
        /// </summary>
        /// <param name="nodeIndex"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public int GetSepIndex(int nodeIndex, BTreeNode<TKey, TValue> node)
        {
             if (nodeIndex == node.Parent.KeyCount)
            {
                return nodeIndex - 1;
            }
            else
            {
                return nodeIndex;
            }
        }

        /// <summary>
        ///  Wrapper class to contain both a key and a node.
        /// </summary>
        internal class TreeAndNode
        {
            public BTreeNode<TKey, TValue> Node;
            public BTreeKey<TKey, TValue> Key;
        }
    }
}
