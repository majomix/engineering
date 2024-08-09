using DataStructures.Heap;

namespace DataStructures.PriorityQueue
{
    /// <summary>
    /// Custom implementation of priority queue (priority given to the largest keys) by max-heap.
    /// </summary>
    internal class CustomMaxPriorityQueueByMaxHeap<TKey, TValue> : IMaxPriorityQueue<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly CustomMaxHeapByDynamicArray<TKey, TValue> _heap = new();

        public void Insert(TKey key, TValue value)
        {
            _heap.Insert(key, value);
        }

        public TValue GetMaximum()
        {
            return _heap.PeekMax();
        }

        public TValue ExtractMaximum()
        {
            return _heap.ExtractMax();
        }
    }
}
