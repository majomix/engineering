namespace DataStructures.Queue
{
    public interface ICustomQueue<T>
    {
        /// <summary>
        /// Returns the number of items in collection.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Adds an item to the queue, resizing it if needed.
        /// </summary>
        /// <param name="value">Item to add.</param>
        void Enqueue(T value);

        /// <summary>
        /// Removes and returns the oldest item from the collection.
        /// </summary>
        /// <returns>Value of the oldest added item.</returns>
        T? Dequeue();

        /// <summary>
        /// Returns but does not remove the oldest item from the collection.
        /// </summary>
        /// <returns>Value of the oldest added item.</returns>
        T? Peek();

        /// <summary>
        /// Clears the whole queue.
        /// </summary>
        void Clear();
    }
}
