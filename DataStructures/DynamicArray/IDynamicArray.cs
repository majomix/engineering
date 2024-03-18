namespace DataStructures.DynamicArray
{
    public interface IDynamicArray<T>
    {
        /// <summary>
        /// Access collection by indices.
        /// </summary>
        /// <param name="index">Index to access.</param>
        /// <returns>Item at the index.</returns>
        T this[uint index] { get; set; }

        /// <summary>
        /// Returns the number of items in collection.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Adds an item at the end of the array, resizing it if needed.
        /// </summary>
        /// <param name="value">Item to add.</param>
        void Add(T value);

        /// <summary>
        /// Adds an item at the desired position, copying the rest of the collection to position +1.
        /// Insertions at the back are legal.
        /// </summary>
        /// <param name="index">Index to copy the item. Must be less or equal than Count.</param>
        /// <param name="value">Item to add.</param>
        /// <returns></returns>
        bool Insert(uint index, T value);

        /// <summary>
        /// Finds item in the collection and tries to remove it, rearranging the rest of the collection.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>True if found, false otherwise.</returns>
        bool Remove(T item);

        /// <summary>
        /// Removes an item from specified index, rearranging the rest of the collection.
        /// </summary>
        /// <param name="index">Index to remove. Must be smaller than count.</param>
        /// <returns>True if found, false otherwise.</returns>
        bool RemoveAt(uint index);

        /// <summary>
        /// Checks in linear time whether given item is located in collection.
        /// </summary>
        /// <param name="value">Item to check.</param>
        /// <returns>True if collection contains item, false otherwise</returns>
        bool Contains(T value);

        /// <summary>
        /// Clears the whole array, resetting internal size to default.
        /// </summary>
        void Clear();
    }
}
