﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BinarySearchTree
{
    /// <summary>
    /// Represents a node in a (binary) tree.
    /// </summary>
    /// <typeparam name="T">The payload of the node as generic type.</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// The payload of the node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// An item with lower a value than the value of this node will become a LeftNode.
        /// </summary>
        public Node<T> LeftNode;
        /// <summary>
        /// An item with higher a value than the value of this node will become a RightNode.
        /// </summary>
        public Node<T> RightNode;

        /// <summary>
        /// Creates a new Node.
        /// </summary>
        /// <param name="value">Payload of the new node</param>
        public Node(T value)
        {
            Value = value;
        }
    }


    /// <summary>
    /// Data structure that stores items and allows fast lookup, insertion and deletion.
    /// </summary>
    /// <typeparam name="T">The type of the trees items.</typeparam>
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private Node<T> _globalRoot;
        /// <summary>
        /// Inserts a new node in a existing tree
        /// </summary>
        /// <param name="root">The root node of the tree</param>
        /// <param name="value">Value to be inserted into the tree</param>
        /// <returns></returns>
        /*public Node<T> InsertNode(ref Node<T> root, T value)
        {
            if (root == null)
            {
                root = new Node<T>(value);
                if (_globalRoot == null)
                    _globalRoot = root;
            }
            else
            {
                if (value.CompareTo(root.Value) <= 0) 
                {
                    root.LeftNode = InsertNode(root.LeftNode, value);
                }
                else if (value.CompareTo(root.Value) > 0) //Items with the same value are ignored, use >= to insert them into the three
                {
                    root.RightNode = InsertNode(root.RightNode, value);
                }
            }
            return root;
        }*/


        public void InsertNode(T value)
        {
            if (_globalRoot == null)
            {
                _globalRoot = new Node<T>(value);
            }
            else
            {
                InsertNode(_globalRoot, value);
            }
        }

        private static Node<T> InsertNode(Node<T> root, T value)
        {
            if (root == null)
            {
                root = new Node<T>(value);
            }
            else
            {
                if (value.CompareTo(root.Value) <= 0)
                {
                    root.LeftNode = InsertNode(root.LeftNode, value);
                }
                else if (value.CompareTo(root.Value) > 0)//Items with the same value are ignored, use >= to insert them into the three
                {
                    root.RightNode = InsertNode(root.RightNode, value);
                }
            }
            return root;
        }

        /// <summary>
        /// Preorder traversal of the tree. Visites the root, then visits the left sub-tree, after that it visits the right sub-tree.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> PreorderTraverseTree()
        {
            if (_globalRoot == null) yield break;

            foreach (var node in PreorderTraverseTree(_globalRoot))
                yield return node;
        }

        private static IEnumerable<T> PreorderTraverseTree(Node<T> root)
        {
            if (root == null) yield break;
            yield return root.Value;
            foreach (var v in PreorderTraverseTree(root.LeftNode))
            {
                yield return v;
            }

            foreach (var v in PreorderTraverseTree(root.RightNode))
            {
                yield return v;
            }
        }

        /// <summary>
        /// Inorder traversal of the tree.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> InOrderTraverseTree()
        {
            if (_globalRoot == null) yield break;

            foreach (var node in InOrderTraverseTree(_globalRoot))
                yield return node;
        }

        private static IEnumerable<T> InOrderTraverseTree(Node<T> root)
        {
            if (root == null) yield break;
            foreach (var v in InOrderTraverseTree(root.LeftNode))
            {
                yield return v;
            }
            yield return root.Value;
            foreach (var v in InOrderTraverseTree(root.RightNode))
            {
                yield return v;
            }
        }

        /// <summary>
        /// Deletes a node from the tree.
        /// </summary>
        /// <param name="value">Value of the node which is to be deleted.</param>
        public void DeleteNode(T value)
        {
            if (_globalRoot == null) return;
            DeleteNode(ref _globalRoot, value);
        }

        private void DeleteNode(ref Node<T> root, T value)
        {
            if (root == null) return;
            if (root.Value.Equals(value))
                root = Delete(ref root);
            else if (value.CompareTo(root.Value) <= 0)
                DeleteNode(ref root.LeftNode, value);
            else if (value.CompareTo(root.Value) >= 0)
            {
                DeleteNode(ref root.RightNode, value);
            }
        }

        /// <summary>
        /// Traverses the tree to find and return a Node with a certain value.
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <returns></returns>
        public T FindNode(T value)
        {
            var res = FindNode(_globalRoot, value);

            return res.Value;
        }

        private static Node<T> FindNode(Node<T> root, T value)
        {
            Node<T> res = null;
            if (root.LeftNode != null)
                res = FindNode(root.LeftNode, value);

            if (value.CompareTo(root.Value) == 0)
                return root;

            if (res == null && root.RightNode != null)
                res = FindNode(root.RightNode, value);

            return res;
        }

        /// <summary>
        /// Only use with custom implementations of DeleteNode!
        /// </summary>
        /// <param name="root">The root node of the tree.</param>
        /// <returns></returns>
        public Node<T> Delete(ref Node<T> root)
        {
            var tempValue = default(T);

            if (_globalRoot == root && root.LeftNode == null && root.RightNode == null)
            {
                //Deletion of root element is allowed  - to for forbid it, return root
                return null;
            }
            if (root.LeftNode == null && root.RightNode == null)
            {
                root = null;
            }
            else if (root.LeftNode == null)
            {
                root = root.RightNode;
            }
            else if (root.RightNode == null)
            {
                root = root.LeftNode;
            }
            else
            {
                Replace(ref root, ref tempValue);
                root.Value = tempValue;
            }
            return root;
        }

        private static void Replace(ref Node<T> root, ref T newValue)
        {
            if (root == null) return;
            if (root.LeftNode == null)
            {
                newValue = root.Value;
                root = root.RightNode;
            }
            else
            {
                Replace(ref root.LeftNode, ref newValue);
            }
        }

        public BinarySearchTree<T> BalancedTree()
        {
            var balanced = new BinarySearchTree<T>();
            var inorder = InOrderTraverseTree().ToArray();

            balanced._globalRoot = BalanceTree(inorder, 0, inorder.Length - 1);

            return balanced;
        }

        private static Node<T> BalanceTree(IReadOnlyList<T> inorder, int startIndex, int endIndex)
        {
            if (startIndex > endIndex) return null;

            var middIndex = (startIndex + endIndex) / 2;

            var root = new Node<T>(inorder[middIndex]);

            root.LeftNode = BalanceTree(inorder, startIndex, middIndex - 1);
            root.RightNode = BalanceTree(inorder, middIndex + 1, endIndex);

            return root;
        }
    }
}
