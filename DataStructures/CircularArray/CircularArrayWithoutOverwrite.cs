namespace DataStructures.CircularArray
{
    /// <summary>
    /// Single consumer circular array without overwriting.
    /// </summary>
    public class CircularArrayWithoutOverwrite<T> : ICircularArray<T>
    {
        private T[] _array;
        private int _readIndex;
        private int _writeIndex;

        public uint Count { get; private set; }

        public CircularArrayWithoutOverwrite(int capacity)
        {
            _array = new T[capacity];
        }

        public bool Add(T item)
        {
            if (IsBufferFull())
            {
                return false;
            }

            var writeIndex = GetWriteIndex();
            _array[writeIndex] = item;
            _writeIndex = Increment(_writeIndex);
            Count++;

            return true;
        }

        public T? Get()
        {
            if (Count == 0)
                return default;

            var item = _array[_readIndex];
            _readIndex = Increment(_readIndex);
            Count--;

            return item;
        }

        public void Clear()
        {
            _array = new T[_array.Length];
            Count = 0;
            _writeIndex = 0;
            _readIndex = 0;
        }

        private bool IsBufferFull()
        {
            return Count == _array.Length;
        }

        private int GetWriteIndex()
        {
            return _writeIndex % _array.Length;
        }

        private int Increment(int index)
        {
            return (index + 1) % _array.Length;
        }

        private int Decrement(int index)
        {
            return (index + _array.Length) % _array.Length;
        }
    }
}
