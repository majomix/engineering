using DataStructures.LinkedList;

namespace DataStructures.Queue
{
    /// <summary>
    /// Custom implementation of First In First Out collection.
    /// </summary>
    public class CustomQueueByLinkedList<T> : ICustomQueue<T>
    {
        private readonly CustomCircularDoublyLinkedList<T> _linkedList = new();

        public uint Count { get; private set; }

        public void Enqueue(T value)
        {
            _linkedList.Add(value);
            Count++;
        }

        public T Dequeue()
        {
            var headNode = _linkedList.GetHead();

            if (headNode == null)
                throw new InvalidOperationException("Queue is empty.");

            _linkedList.RemoveNode(headNode);
            Count--;

            return headNode.Value;
        }

        public T Peek()
        {
            var headNode = _linkedList.GetHead();

            if (headNode == null)
                throw new InvalidOperationException("Queue is empty.");

            return headNode.Value;
        }

        public void Clear()
        {
            _linkedList.Clear();
            Count = 0;
        }
    }
}
