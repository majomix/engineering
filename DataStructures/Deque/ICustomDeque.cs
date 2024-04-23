namespace DataStructures.Deque
{
    public interface ICustomDeque<T>
    {
        /// <summary>
        /// Returns the number of items in collection.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Adds an item to the left side of the deque, resizing it if needed.
        /// </summary>
        /// <param name="value">Item to add.</param>
        void PushFront(T value);

        /// <summary>
        /// Removes and returns the most left-most item from the collection.
        /// </summary>
        /// <returns>Value of the left-most added item.</returns>
        T PopFront();

        /// <summary>
        /// Adds an item to the right side of the deque, resizing it if needed.
        /// </summary>
        /// <param name="value">Item to add.</param>
        void PushBack(T value);

        /// <summary>
        /// Removes and returns the most right-most item from the collection.
        /// </summary>
        /// <returns>Value of the right-most added item.</returns>
        T PopBack();
    }
}
