using DataStructures.Heap;

namespace DataStructures.PriorityQueue
{
    /// <summary>
    /// Custom implementation of priority queue (priority given to the smallest keys) by min-heap.
    /// </summary>
    internal class CustomMinPriorityQueueByMinHeap<TKey, TValue> : IMinPriorityQueue<TKey, TValue> where TKey : IComparable<TKey>
    {
        private readonly CustomMinHeapByDynamicArray<TKey, TValue> _heap = new();

        public void Insert(TKey key, TValue value)
        {
            _heap.Insert(key, value);
        }

        public TValue GetMinimum()
        {
            return _heap.PeekMin();
        }

        public TValue ExtractMinimum()
        {
            return _heap.ExtractMin();
        }
    }
}
