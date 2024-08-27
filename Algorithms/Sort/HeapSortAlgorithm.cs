using DataStructures.Heap;

namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Build heap in linear time. Extract root and place it at the end of the array. Repeat until empty.
    ///
    /// Optimizations:
    /// (x) Heap sort should operate directly on the underlying array rather than on copy.
    ///
    /// Complexity:
    /// Time: O(n + n*log(n))
    /// Space: O(1) (this simplified implementation is O(n))
    /// </summary>
    internal class HeapSortAlgorithm<TKey, TValue> : ISortingAlgorithm<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, TKey> keySelector)
        {
            var heap = new CustomMaxHeapByDynamicArray<TKey, TValue>();
            heap.BuildHeap(input, keySelector);

            for (var i = 0; i < input.Length; i++)
            {
                var root = heap.ExtractMax();
                input[input.Length - 1 - i] = root;
            }

            return input;
        }
    }
}
