namespace DataStructures.PriorityQueue
{
    public interface IMinPriorityQueue<in TKey, TValue>
    {
        /// <summary>
        /// Insert a value into priority queue based on key.
        /// </summary>
        /// <param name="key">Sort key.</param>
        /// <param name="value">Value to store.</param>
        void Insert(TKey key, TValue value);

        /// <summary>
        /// Returns the minimum element from the priority queue without removing it.
        /// </summary>
        /// <returns>Minimum element.</returns>
        TValue GetMinimum();

        /// <summary>
        /// Extracts the minimum element from the priority queue.
        /// </summary>
        /// <returns>Minimum element.</returns>
        TValue ExtractMinimum();
    }
}
