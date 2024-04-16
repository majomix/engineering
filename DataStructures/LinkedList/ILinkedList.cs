namespace DataStructures.LinkedList
{
    public interface ILinkedList<in T>
    {
        /// <summary>
        /// Adds an element to the end of the linked list. Also serves as a collection initializer.
        /// </summary>
        /// <param name="item">Value to add.</param>
        void Add(T item);

        /// <summary>
        /// Adds an element to the front of the linked list. Also serves as a collection initializer.
        /// </summary>
        /// <param name="item">Value to add.</param>
        void AddFront(T item);

        /// <summary>
        /// Clears the whole linked list, resetting internal size to default.
        /// </summary>
        void Clear();

        /// <summary>
        /// Removes an element from the linked list in linear time.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns><see langword="true" /> if successful, <see langword="false" />otherwise.</returns>
        bool Remove(T item);
    }
}
