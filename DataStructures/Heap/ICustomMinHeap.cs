﻿namespace DataStructures.Heap
{
    public interface ICustomMinHeap<in TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Insert a value into min-heap based on key.
        /// </summary>
        /// <param name="key">Sort key.</param>
        /// <param name="value">Value to store.</param>
        void Insert(TKey key, TValue value);

        /// <summary>
        /// Returns the minimum element from the heap without removing it.
        /// </summary>
        /// <returns>Minimum element.</returns>
        TValue PeekMin();

        /// <summary>
        /// Extracts the minimum element from the heap.
        /// </summary>
        /// <returns>Minimum element.</returns>
        TValue ExtractMin();

        /// <summary>
        /// Builds heap from an array of unordered items in linear time.
        /// </summary>
        /// <param name="items">Unordered array of items.</param>
        /// <param name="keySelector">Predicate to select key from the value elements.</param>
        void BuildHeap(TValue[] items, Func<TValue, TKey> keySelector);
        
        /// <summary>
        /// Decreases key of given value.
        /// </summary>
        /// <param name="value">Value to look for.</param>
        /// <param name="newKey">New key.</param>
        void DecreaseKey(TValue value, TKey newKey);

        /// <summary>
        /// Clear heap.
        /// </summary>
        void Clear();

        /// <summary>
        /// Number of elements.
        /// </summary>
        uint Count { get; }
    }
}
