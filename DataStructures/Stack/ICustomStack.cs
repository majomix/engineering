namespace DataStructures.Stack
{
    /// <summary>
    /// Custom implementation of Last In First Out collection.
    /// </summary>
    public interface ICustomStack<T>
    {
        /// <summary>
        /// Returns the number of items in collection.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Adds an item at the end of the stack, resizing it if needed.
        /// </summary>
        /// <param name="value">Item to add.</param>
        void Push(T value);

        /// <summary>
        /// Removes and returns the most recently added item from the collection.
        /// </summary>
        /// <returns>Value of the most recently added item.</returns>
        T? Pop();

        /// <summary>
        /// Returns but does not remove the most recently added item from the collection.
        /// </summary>
        /// <returns>Value of the most recently added item.</returns>
        T? Peek();

        /// <summary>
        /// Clears the whole stack.
        /// </summary>
        void Clear();
    }
}
