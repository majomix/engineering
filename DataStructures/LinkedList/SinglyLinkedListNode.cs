namespace DataStructures.LinkedList
{
    /// <summary>
    /// Node for singly linked list.
    /// </summary>
    public class SinglyLinkedListNode<T>
    {
        public SinglyLinkedListNode(T value, ILinkedList<T> parent)
        {
            Parent = parent;
            Value = value;
        }

        public ILinkedList<T> Parent { get; internal set; }

        public SinglyLinkedListNode<T>? Next { get; internal set; }

        public T Value { get; set; }
    }
}
