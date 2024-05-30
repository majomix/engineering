using DataStructures.Tree.BinarySearchTree.Enumerator;
using System.Collections;

namespace DataStructures.Tree.BinarySearchTree
{
    public class CustomBinarySearchTree<T> : IBinarySearchTree<T>, IEnumerable<T>
    {
        private BinaryTreeNode<T>? _root;

        public BinaryTreeNode<T>? GetRoot()
        {
            return _root;
        }

        public void Insert(T item)
        {
            var node = new BinaryTreeNode<T>(item);

            if (_root == null)
            {
                _root = node;
            }
            else
            {
                InsertNodeRecursively(_root, node);
            }
        }

        private void InsertNodeRecursively(BinaryTreeNode<T> parent, BinaryTreeNode<T> node)
        {
            var comparer = Comparer<T>.Default;

            // parent has bigger value than node
            if (comparer.Compare(parent.Value, node.Value) > 0)
            {
                if (parent.Left == null)
                {
                    parent.Left = node;
                }
                else
                {
                    InsertNodeRecursively(parent.Left, node);
                }
            }
            // node has bigger value than parent
            else
            {
                if (parent.Right == null)
                {
                    parent.Right = node;
                }
                else
                {
                    InsertNodeRecursively(parent.Right, node);
                }
            }
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public BinaryTreeNode<T>? Search(T item)
        {
            if (_root == null)
                return null;

            return SearchNodeRecursively(_root, item);
        }

        private BinaryTreeNode<T>? SearchNodeRecursively(BinaryTreeNode<T>? node, T item)
        {
            if (node == null)
                return null;

            var comparisonResult = Comparer<T>.Default.Compare(node.Value, item);

            return comparisonResult switch
            {
                // equal
                0 => node,
                // parent has bigger value than node
                > 0 => SearchNodeRecursively(node.Left, item),
                // parent has smaller value than node
                _ => SearchNodeRecursively(node.Right, item)
            };
        }

        // in-order traversal
        public IEnumerator<T> GetEnumerator()
        {
            return new BstInOrderEnumerator<T>(this);
        }

        public IEnumerator<T> GetPreOrderEnumerator()
        {
            return new BstPreOrderEnumerator<T>(this);
        }

        public IEnumerator<T> GetPostOrderEnumerator()
        {
            return new BstPostOrderEnumerator<T>(this);
        }

        public IEnumerator<T> GetLevelOrderEnumerator()
        {
            return new BstLevelOrderEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
