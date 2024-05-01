namespace DataStructures.Deque
{
    /// <summary>
    /// Deque implemented by circular dynamic array.
    /// The layout is:
    /// [FRONT / LEFT] ... [BACK / RIGHT]
    /// </summary>
    public class CustomDequeByCircularArray<T> : ICustomDeque<T>
    {
        private const int InitialArraySize = 4;

        private T[] _array;
        private int _frontIndex;
        private int _backIndex;

        public uint Count { get; private set; }

        public CustomDequeByCircularArray(int capacity = InitialArraySize)
        {
            _array = new T[capacity];
            Count = 0;
            _frontIndex = _array.Length - 1;
            _backIndex = 0;
        }

        public void PushFront(T value)
        {
            if (IsBufferFull())
            {
                Reallocate();
            }

            _array[_frontIndex] = value;
            _frontIndex = Decrement(_frontIndex);
            Count++;
        }

        public T PopFront()
        {
            if (Count == 0)
                throw new InvalidOperationException("Deque is empty.");

            _frontIndex = Increment(_frontIndex);
            var value = _array[_frontIndex];
            Count--;

            return value;
        }

        public void PushBack(T value)
        {
            if (IsBufferFull())
            {
                Reallocate();
            }

            _array[_backIndex] = value;
            _backIndex = Increment(_backIndex);
            Count++;
        }

        public T PopBack()
        {
            if (Count == 0)
                throw new InvalidOperationException("Deque is empty.");

            _backIndex = Decrement(_backIndex);
            var value = _array[_backIndex];
            Count--;

            return value;
        }

        public void Clear()
        {
            _array = new T[_array.Length];
            Count = 0;
            _frontIndex = _array.Length - 1;
            _backIndex = 0;
        }

        private bool IsBufferFull()
        {
            return Count == _array.Length;
        }

        private int Increment(int index)
        {
            return (index + 1) % _array.Length;
        }

        private int Decrement(int index)
        {
            return (index - 1 + _array.Length) % _array.Length;
        }

        public bool Reallocate()
        {
            var newSize = _array.Length * 2;

            // copy content
            var temp = new T[newSize];
            var lengthToEnd = Count - _backIndex;
            Array.Copy(_array, _backIndex, temp, 0, lengthToEnd);
            Array.Copy(_array, 0, temp, lengthToEnd, _backIndex);

            _backIndex = (int)Count;
            _frontIndex = newSize - 1;
            _array = temp;

            return true;
        }
    }
}
