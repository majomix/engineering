namespace DataStructures.LinkedList
{
    /// <summary>
    /// Node for doubly linked list.
    /// </summary>
    public class DoublyLinkedListNode<T>
    {
        public DoublyLinkedListNode(T value, ILinkedList<T> parent)
        {
            Parent = parent;
            Value = value;
        }

        public ILinkedList<T> Parent { get; internal set; }

        public DoublyLinkedListNode<T>? Previous { get; internal set; }

        public DoublyLinkedListNode<T>? Next { get; internal set; }

        public T Value { get; set; }
    }
}
