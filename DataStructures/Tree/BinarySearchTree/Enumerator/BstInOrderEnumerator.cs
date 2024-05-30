using System.Collections;

namespace DataStructures.Tree.BinarySearchTree.Enumerator
{
    public struct BstInOrderEnumerator<T> : IEnumerator<T>
    {
        private readonly BinaryTreeNode<T>? _root;
        private readonly Queue<T> _treeSnapshot = new();
        private T _current = default;

        public BstInOrderEnumerator(CustomBinarySearchTree<T> tree)
        {
            _root = tree.GetRoot();
            FillQueue(_root);
        }

        private void FillQueue(BinaryTreeNode<T>? node)
        {
            if (node == null)
                return;

            FillQueue(node.Left);
            _treeSnapshot.Enqueue(node.Value);
            FillQueue(node.Right);
        }

        public bool MoveNext()
        {
            if (_treeSnapshot.Count == 0)
                return false;

            _current = _treeSnapshot.Dequeue();

            return true;
        }

        public void Reset()
        {
            _treeSnapshot.Clear();
            FillQueue(_root);
        }

        public readonly T Current => _current;

        object? IEnumerator.Current => Current;

        public void Dispose()
        { }
    }
}
