namespace DataStructures.CircularArray
{
    public interface ICircularArray<T>
    {
        /// <summary>
        /// Returns the number of items in collection.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Adds an item at the current write index and advances the write pointer.
        /// </summary>
        /// <param name="value">Item to add.</param>
        /// <returns><value>True</value> if successfully added, <value>False</value> otherwise.</returns>
        bool Add(T value);

        /// <summary>
        /// Gets item at the current read index and advances the read pointer.
        /// </summary>
        /// <param name="value">Item or <value>default</value> if <see cref="Count"/> is 0.</param>
        /// <returns>True if value is valid, false if not.</returns>
        public bool TryGet(out T? value);

        /// <summary>
        /// Peeks at item at the current read index and advances the read pointer.
        /// </summary>
        /// <param name="value">Item or <value>default</value> if <see cref="Count"/> is 0.</param>
        /// <returns>True if value is valid, false if not.</returns>
        public bool TryPeek(out T? value);

        /// <summary>
        /// Clears the whole circular array.
        /// </summary>
        void Clear();
    }
}
