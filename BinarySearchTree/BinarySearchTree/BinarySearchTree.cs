using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    public class Node<T>
    {
        public T Value { get; set; }

        public Node<T> LeftNode;
        public Node<T> RightNode;

        public Node(T value)
        {
            Value = value;
        }
    }

    public class BinarySearchTree<T> where T : IComparable
    {
        private Node<T> _globalRoot;
        public Node<T> InsertNode(Node<T> root, T value)
        {
            if (root == null)
            {
                root = new Node<T>(value);
                if(_globalRoot == null)
                    _globalRoot = root;
            }
            else
            {
                if (value.CompareTo(root.Value) <= 0)
                {
                    root.LeftNode = InsertNode(root.LeftNode, value);
                }
                else if (value.CompareTo(root.Value) >= 0)
                {
                    root.RightNode = InsertNode(root.RightNode, value);
                }
            }
            return root;
        }

        public IEnumerable<T> PreorderTraverseTree(Node<T> root)
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

        public void DeleteNode(ref Node<T> root, T value)
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

        private Node<T> Delete(ref Node<T> root)
        {
            if (_globalRoot == root)
            {
                Console.WriteLine("Deletion of root element is not allowed");
                return root;
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
                Replace(ref root);
            }
            return root;
        }

        private void Replace(ref Node<T> root)
        {
            if (root.LeftNode == null)
                root = root.RightNode;
            else { Replace(ref root.LeftNode);}
        }
    }
}
