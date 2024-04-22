using DataStructures.LinkedList;

namespace DataStructures.Stack
{
    /// <summary>
    /// Custom implementation of Last In First Out collection based on circular doubly linked list.
    /// </summary>
    public class CustomStackByLinkedList<T> : ICustomStack<T>
    {
        private readonly CustomCircularDoublyLinkedList<T> _linkedList = new();

        public uint Count { get; private set; }

        public void Push(T item)
        {
            _linkedList.Add(item);
            Count++;
        }

        public T Pop()
        {
            var tailNode = _linkedList.GetTail();

            if (tailNode == null)
                throw new InvalidOperationException("Stack is empty.");

            _linkedList.RemoveNode(tailNode);
            Count--;

            return tailNode.Value;
        }

        public T Peek()
        {
            var tailNode = _linkedList.GetTail();

            if (tailNode == null)
                throw new InvalidOperationException("Stack is empty.");

            return tailNode.Value;
        }

        public void Clear()
        {
            _linkedList.Clear();
            Count = 0;
        }
    }
}
