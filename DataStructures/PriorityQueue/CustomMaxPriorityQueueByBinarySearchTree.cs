using DataStructures.Tree.BinarySearchTree;

namespace DataStructures.PriorityQueue
{
    /// <summary>
    /// Custom implementation of priority queue (priority given to the largest keys) by binary search tree.
    /// </summary>
    public class CustomMaxPriorityQueueByBinarySearchTree<TKey, TValue> : IMaxPriorityQueue<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly CustomBinarySearchTree<PriorityItem<TKey, TValue>> _tree = new();

        public void Insert(TKey key, TValue value)
        {
            _tree.Insert(new PriorityItem<TKey, TValue> { Key = key, Value = value });
        }

        public TValue PeekMaximum()
        {
            return GetMaximumItem().Value!;
        }

        public TValue ExtractMaximum()
        {
            var minimumItem = GetMaximumItem();
            _tree.Remove(minimumItem);

            return minimumItem.Value!;
        }

        private PriorityItem<TKey, TValue> GetMaximumItem()
        {
            var root = _tree.GetRoot();
            if (root == null)
                throw new InvalidOperationException("Priority queue is empty.");

            var current = root;
            while (current.Right != null)
            {
                current = current.Right;
            }

            return current.Value;
        }

        public uint Count => (uint)_tree.Count();
    }
}
