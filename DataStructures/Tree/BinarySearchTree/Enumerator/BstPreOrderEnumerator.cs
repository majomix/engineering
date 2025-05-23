﻿using System.Collections;

namespace DataStructures.Tree.BinarySearchTree.Enumerator
{
    public struct BstPreOrderEnumerator<T> : IEnumerator<T>
    {
        private readonly BinaryTreeNode<T>? _root;
        private readonly Queue<T> _treeSnapshot = new();
        private T _current = default!;

        public BstPreOrderEnumerator(CustomBinarySearchTree<T> tree)
        {
            _root = tree.GetRoot();
            FillQueue(_root);
        }

        private void FillQueue(BinaryTreeNode<T>? node)
        {
            if (node == null)
                return;

            _treeSnapshot.Enqueue(node.Value);
            FillQueue(node.Left);
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
