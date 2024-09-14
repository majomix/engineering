namespace DataStructures.PriorityQueue
{
    public interface IMaxPriorityQueue<in TKey, TValue>
    {
        /// <summary>
        /// Insert a value into priority queue based on key.
        /// </summary>
        /// <param name="key">Sort key.</param>
        /// <param name="value">Value to store.</param>
        void Insert(TKey key, TValue value);

        /// <summary>
        /// Returns the maximum element from the priority queue without removing it.
        /// </summary>
        /// <returns>Maximum element.</returns>
        TValue PeekMaximum();

        /// <summary>
        /// Extracts the maximum element from the priority queue.
        /// </summary>
        /// <returns>Maximum element.</returns>
        TValue ExtractMaximum();

        /// <summary>
        /// Number of elements.
        /// </summary>
        uint Count { get; }
    }
}
