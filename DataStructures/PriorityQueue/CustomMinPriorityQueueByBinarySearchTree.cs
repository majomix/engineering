using DataStructures.Tree.BinarySearchTree;

namespace DataStructures.PriorityQueue
{
    /// <summary>
    /// Custom implementation of priority queue (priority given to the smallest keys) by binary search tree.
    /// </summary>
    internal class CustomMinPriorityQueueByBinarySearchTree<TKey, TValue> : IMinPriorityQueue<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly CustomBinarySearchTree<PriorityItem<TKey, TValue>> _tree = new();

        public void Insert(TKey key, TValue value)
        {
            _tree.Insert(new PriorityItem<TKey, TValue> { Key = key, Value = value });
        }

        public TValue GetMinimum()
        {
            return GetMinimumItem().Value!;
        }

        public TValue ExtractMinimum()
        {
            var minimumItem = GetMinimumItem();
            _tree.Remove(minimumItem);

            return minimumItem.Value!;
        }

        private PriorityItem<TKey, TValue> GetMinimumItem()
        {
            var root = _tree.GetRoot();
            if (root == null)
                throw new InvalidOperationException("Priority queue is empty.");

            var current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current.Value;
        }
    }
}
