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

        public bool Remove(T item)
        {
            // empty tree
            if (_root == null)
                return false;

            var searchResult = Search(item);
            var nodeToRemove = GetNodeToRemoveFromParent(searchResult.Parent, item);
            var parent = searchResult.Parent;

            // item does not exist in the tree
            if (nodeToRemove == null)
                return false;

            // if the node to remove is a leaf or has one subtree, it can be safely removed
            // parent's link has to be replaced by the removed node's ancestor, if any
            if (nodeToRemove.IsLeaf || nodeToRemove.HasOneSubtree)
            {
                // either left, right or null if leaf
                var nodeToLink = nodeToRemove.Left ?? nodeToRemove.Right;

                if (parent == null)
                {
                    _root = nodeToLink;
                    return true;
                }

                if (parent.Left == nodeToRemove)
                {
                    parent.Left = nodeToLink;
                    return true;
                }

                parent.Right = nodeToLink;
                return true;
            }

            // if the node to remove has two subtrees, it must be replaced by predecessor, then removed
            // predecessor in this case is guaranteed to be the maximum node in the left subtree
            // parent's link as well as the predecessor's subtree has to be adjusted
            if (nodeToRemove.HasTwoSubtrees)
            {
                var predecessorSearchResult = FindMaximumInLeftSubtree(nodeToRemove);
                var parentOfPredecessor = predecessorSearchResult.Parent!;
                var predecessor = predecessorSearchResult.Result!;

                // predecessor was its parent's right child
                // the predecessor node is guaranteed to only have left child
                parentOfPredecessor.Right = predecessor.Left;

                // relink predecessor with children of the node to remove
                predecessor.Left = nodeToRemove.Left;
                predecessor.Right = nodeToRemove.Right;

                // removal of root
                if (_root == nodeToRemove)
                {
                    _root = predecessor;
                }
            }

            return true;
        }

        private BinaryTreeNode<T>? GetNodeToRemoveFromParent(BinaryTreeNode<T>? parent, T item)
        {
            // empty tree
            if (_root == null)
                return null;

            var comparer = Comparer<T>.Default;
            
            // no parent means either root or not present in the tree
            if (parent == null && comparer.Compare(_root.Value, item) == 0)
                return _root;

            // no parent at this point means item not found in the tree
            if (parent == null)
                return null;

            // parent exists so the item must be in one of the children
            // check for left child
            if (parent.Left != null && comparer.Compare(parent.Left.Value, item) == 0)
                return parent.Left;

            // remaining option is the right child
            return parent.Right;
        }

        // successor is left-most node in right subtree; if no right subtree exists, it has to be one of ancestors
        // predecessor is right-most node in the left subtree; if no left subtree exists, for right leaf it is parent and for left leaf it is one of ancestors
        // in this case we're only searching for the right-most node in the left subtree
        private BinaryTreeSearchResult<T> FindMaximumInLeftSubtree(BinaryTreeNode<T> root)
        {
            if (root.Left == null)
                return new BinaryTreeSearchResult<T>(null, null);

            var parent = root;
            var current = root.Left;
            while (current.Right != null)
            {
                parent = current;
                current = current.Right;
            }

            return new BinaryTreeSearchResult<T>(current, parent);
        }

        public BinaryTreeSearchResult<T> Search(T item)
        {
            return SearchNodeRecursively(_root, null, item);
        }

        private BinaryTreeSearchResult<T> SearchNodeRecursively(BinaryTreeNode<T>? node, BinaryTreeNode<T>? parent, T item)
        {
            if (node == null)
                return new BinaryTreeSearchResult<T>(null, null);

            var comparisonResult = Comparer<T>.Default.Compare(node.Value, item);

            return comparisonResult switch
            {
                // equal
                0 => new BinaryTreeSearchResult<T>(node, parent),
                // parent has bigger value than node
                > 0 => SearchNodeRecursively(node.Left, node, item),
                // parent has smaller value than node
                _ => SearchNodeRecursively(node.Right, node, item)
            };
        }

        /// <summary>
        /// Gets in-order enumerator.
        /// </summary>
        /// <returns>In-order enumerator.</returns>
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
