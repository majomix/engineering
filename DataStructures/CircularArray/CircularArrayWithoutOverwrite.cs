using DataStructures.CircularArray.Reallocation;
using DataStructures.Helpers;

namespace DataStructures.CircularArray
{
    /// <summary>
    /// Single consumer circular array without overwriting.
    /// </summary>
    public class CircularArrayWithoutOverwrite<T> : ICircularArray<T>
    {
        private const int InitialArraySize = 4;

        private T[] _array;
        private int _readIndex;
        private int _writeIndex;

        private readonly Dictionary<ReallocationPolicy, IReallocationPolicy<T>> _reallocationStrategies = new() {
            { ReallocationPolicy.NoReallocation, new NoReallocationPolicy<T>() },
            { ReallocationPolicy.DynamicReallocation, new DynamicReallocationPolicy<T>() }
        };

        private readonly IReallocationPolicy<T> _reallocationPolicy;

        public uint Count { get; private set; }

        public CircularArrayWithoutOverwrite(
            ReallocationPolicy reallocationPolicy = ReallocationPolicy.NoReallocation,
            int capacity = InitialArraySize)
        {
            _array = new T[capacity];
            _reallocationPolicy = _reallocationStrategies[reallocationPolicy];
        }

        public bool Add(T item)
        {
            if (IsBufferFull())
            {
                if (!_reallocationPolicy.Reallocate(ref _array, ref _readIndex, ref _writeIndex, (int)Count))
                {
                    return false;
                }
            }

            var writeIndex = GetWriteIndex();
            _array[writeIndex] = item;
            _writeIndex = Increment(_writeIndex);
            Count++;

            return true;
        }

        public bool TryGet(out T? value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }

            value = _array[_readIndex];
            _readIndex = Increment(_readIndex);
            Count--;

            return true;
        }

        public bool TryPeek(out T? value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }

            value = _array[_readIndex];

            return true;
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
