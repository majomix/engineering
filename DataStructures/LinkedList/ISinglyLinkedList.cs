namespace DataStructures.LinkedList
{
    public interface ISinglyLinkedList<T>
    {
        /// <summary>
        /// Find linked list node in linear time.
        /// </summary>
        /// <param name="value">Value to find.</param>
        /// <returns>First linked list node with given value, <see langword="null" /> if no such value is found.</returns>
        SinglyLinkedListNode<T>? FindNode(T value);

        /// <summary>
        /// Gets linked list head.
        /// </summary>
        /// <returns>Returns head or <see langword="null" /> if list is empty.</returns>
        SinglyLinkedListNode<T>? GetHead();

        /// <summary>
        /// Insert value after given node.
        /// </summary>
        /// <param name="node">Node after which the value should be inserted.</param>
        /// <param name="value">Value to insert.</param>
        bool InsertAfter(SinglyLinkedListNode<T> node, T value);

        /// <summary>
        /// Inject node after given node and respect original chain.
        /// </summary>
        /// <param name="node">Node after which the value should be inserted.</param>
        /// <param name="nodeToInject">Node to inject and connect to the original successor of its new parent.</param>
        bool InjectNode(SinglyLinkedListNode<T> node, SinglyLinkedListNode<T> nodeToInject);

        /// <summary>
        /// Removes an element node from the linked list in linear time.
        /// </summary>
        /// <param name="node">Item node to remove.</param>
        /// <returns><see langword="true" /> if successful, <see langword="false" /> otherwise.</returns>
        bool Remove(SinglyLinkedListNode<T> node);

        /// <summary>
        /// Reverse singly linked list.
        /// </summary>
        void Reverse();
    }
}
