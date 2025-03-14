﻿using System.Collections;

namespace DataStructures.Tree.BinarySearchTree.Enumerator
{
    public struct BstLevelOrderEnumerator<T> : IEnumerator<T>
    {
        private readonly BinaryTreeNode<T>? _root;
        private readonly Queue<T> _treeSnapshot = new();
        private T _current = default!;

        public BstLevelOrderEnumerator(CustomBinarySearchTree<T> tree)
        {
            _root = tree.GetRoot();
            FillQueue(_root);
        }

        private void FillQueue(BinaryTreeNode<T>? root)
        {
            if (root == null)
                return;

            var levelOrderQueue = new Queue<BinaryTreeNode<T>>();
            levelOrderQueue.Enqueue(root);

            while (levelOrderQueue.Count != 0)
            {
                var node = levelOrderQueue.Dequeue();
                _treeSnapshot.Enqueue(node.Value);

                if (node.Left != null)
                {
                    levelOrderQueue.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    levelOrderQueue.Enqueue(node.Right);
                }
            }
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
