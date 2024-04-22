using DataStructures.CircularArray;
using DataStructures.Helpers;

namespace DataStructures.Queue
{
    internal class CustomQueueByCircularArray<T> : ICustomQueue<T>
    {
        private readonly CircularArrayWithoutOverwrite<T> _array;
        private const int InitialArraySize = 4;

        public uint Count => _array.Count;

        public CustomQueueByCircularArray(int capacity = InitialArraySize)
        {
            _array = new CircularArrayWithoutOverwrite<T>(ReallocationPolicy.DynamicReallocation, capacity);
        }

        public void Enqueue(T value)
        {
            _array.Add(value);
        }

        public T Dequeue()
        {
            var hasValue = _array.TryGet(out var value);

            if (!hasValue)
                throw new InvalidOperationException("Queue is empty.");

            return value!;
        }

        public T Peek()
        {
            var hasValue = _array.TryPeek(out var value);

            if (!hasValue)
                throw new InvalidOperationException("Queue is empty.");

            return value!;
        }

        public void Clear()
        {
            _array.Clear();
        }
    }
}
