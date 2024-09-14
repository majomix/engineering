using DataStructures.Heap;

namespace DataStructures.PriorityQueue
{
    /// <summary>
    /// Custom implementation of priority queue (priority given to the largest keys) by max-heap.
    /// </summary>
    public class CustomMaxPriorityQueueByMaxHeap<TKey, TValue> : IMaxPriorityQueue<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly CustomMaxHeapByDynamicArray<TKey, TValue> _heap = new();

        public void Insert(TKey key, TValue value)
        {
            _heap.Insert(key, value);
        }

        public TValue PeekMaximum()
        {
            return _heap.PeekMax();
        }

        public TValue ExtractMaximum()
        {
            return _heap.ExtractMax();
        }

        public void IncreasePriority(TValue value, TKey newKey)
        {
            _heap.IncreaseKey(value, newKey);
        }

        public uint Count => _heap.Count;
    }
}
