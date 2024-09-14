using DataStructures.Heap;

namespace DataStructures.PriorityQueue
{
    /// <summary>
    /// Custom implementation of priority queue (priority given to the smallest keys) by min-heap.
    /// </summary>
    public class CustomMinPriorityQueueByMinHeap<TKey, TValue> : IMinPriorityQueue<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly CustomMinHeapByDynamicArray<TKey, TValue> _heap = new();

        public void Insert(TKey key, TValue value)
        {
            _heap.Insert(key, value);
        }

        public TValue PeekMinimum()
        {
            return _heap.PeekMin();
        }

        public TValue ExtractMinimum()
        {
            return _heap.ExtractMin();
        }

        public void DecreasePriority(TValue value, TKey newKey)
        {
            _heap.DecreaseKey(value, newKey);
        }

        public uint Count => _heap.Count;
    }
}
