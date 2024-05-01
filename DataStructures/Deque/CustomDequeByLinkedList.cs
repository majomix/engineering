using DataStructures.LinkedList;

namespace DataStructures.Deque
{
    /// <summary>
    /// Deque implemented by circular doubly linked list.
    /// The layout is:
    /// [FRONT / LEFT / HEAD] ... [BACK / RIGHT / TAIL]
    /// </summary>
    public class CustomDequeByLinkedList<T> : ICustomDeque<T>
    {
        private readonly CustomCircularDoublyLinkedList<T> _linkedList = new();

        public uint Count { get; private set;  }

        public void PushFront(T value)
        {
            _linkedList.AddFront(value);
            Count++;
        }

        public T PopFront()
        {
            var headNode = _linkedList.GetHead();

            if (headNode == null)
                throw new InvalidOperationException("Deque is empty.");

            _linkedList.RemoveNode(headNode);
            Count--;

            return headNode.Value;
        }

        public void PushBack(T value)
        {
            _linkedList.Add(value);
            Count++;
        }

        public T PopBack()
        {
            var headNode = _linkedList.GetTail();

            if (headNode == null)
                throw new InvalidOperationException("Deque is empty.");

            _linkedList.RemoveNode(headNode);
            Count--;

            return headNode.Value;
        }
    }
}
