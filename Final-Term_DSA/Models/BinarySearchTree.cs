using System;
using System.Collections.Generic;

namespace CircularLinkedListApp.Models
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public TreeNode<T> Root { get; private set; }

        public BinarySearchTree()
        {
            Root = null;
        }


        public bool Insert(T data)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>(data);
                return true;
            }

            TreeNode<T> current = Root;
            while (true)
            {
                int compareResult = data.CompareTo(current.Data);
                if (compareResult == 0)
                {

                    return false;
                }
                else if (compareResult < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new TreeNode<T>(data);
                        return true;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new TreeNode<T>(data);
                        return true;
                    }
                    current = current.Right;
                }
            }
        }


        public bool Search(T data)
        {
            TreeNode<T> current = Root;
            while (current != null)
            {
                int compareResult = data.CompareTo(current.Data);
                if (compareResult == 0)
                    return true;
                else if (compareResult < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return false;
        }


        public List<T> InOrderTraversal()
        {
            List<T> result = new List<T>();
            InOrderHelper(Root, result);
            return result;
        }

        private void InOrderHelper(TreeNode<T> node, List<T> result)
        {
            if (node != null)
            {
                InOrderHelper(node.Left, result);
                result.Add(node.Data);
                InOrderHelper(node.Right, result);
            }
        }


        public List<T> PreOrderTraversal()
        {
            List<T> result = new List<T>();
            PreOrderHelper(Root, result);
            return result;
        }

        private void PreOrderHelper(TreeNode<T> node, List<T> result)
        {
            if (node != null)
            {
                result.Add(node.Data);
                PreOrderHelper(node.Left, result);
                PreOrderHelper(node.Right, result);
            }
        }

        // Post-order traversal
        public List<T> PostOrderTraversal()
        {
            List<T> result = new List<T>();
            PostOrderHelper(Root, result);
            return result;
        }

        private void PostOrderHelper(TreeNode<T> node, List<T> result)
        {
            if (node != null)
            {
                PostOrderHelper(node.Left, result);
                PostOrderHelper(node.Right, result);
                result.Add(node.Data);
            }
        }

        // Delete a node
        public bool Delete(T data)
        {
            Root = DeleteRec(Root, data);
            return true;
        }

        private TreeNode<T> DeleteRec(TreeNode<T> root, T data)
        {
            if (root == null)
                return root;

            int compareResult = data.CompareTo(root.Data);
            if (compareResult < 0)
                root.Left = DeleteRec(root.Left, data);
            else if (compareResult > 0)
                root.Right = DeleteRec(root.Right, data);
            else
            {

                if (root.Left == null)
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;


                root.Data = MinValue(root.Right);


                root.Right = DeleteRec(root.Right, root.Data);
            }

            return root;
        }

        private T MinValue(TreeNode<T> node)
        {
            T minv = node.Data;
            while (node.Left != null)
            {
                minv = node.Left.Data;
                node = node.Left;
            }
            return minv;
        }
    }
}
