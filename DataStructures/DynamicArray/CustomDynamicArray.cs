using System.Collections;

namespace DataStructures.DynamicArray
{
    /// <summary>
    /// Custom alternative generic List.
    /// </summary>
    public class CustomDynamicArray<T> : IDynamicArray<T>, IEnumerable<T>
    {
        private const int InitialArraySize = 4;

        private readonly double _growthFactor = 2;
        private readonly double _growthOffset = 0;
        
        private T[] _backingArray;
        private uint _count;

        public T this[uint index]
        {
            get => _backingArray[index];
            set => _backingArray[index] = value;
        }

        public uint Count => _count;

        public CustomDynamicArray()
        {
            _backingArray = new T[InitialArraySize];
            _count = 0;
        }

        public CustomDynamicArray(double[] factors)
        {
            _backingArray = new T[InitialArraySize];
            _count = 0;

            _growthFactor = factors[0];
            _growthOffset = factors[1];
        }

        public CustomDynamicArray(uint length)
        {
            // > 2GB could be an issue
            _backingArray = new T[length];
            _count = 0;
        }

        public CustomDynamicArray(T[] array)
        {
            _backingArray = array;
            _count = (uint)array.Length;
        }

        public void Add(T value)
        {
            EnsureSizeForNextItem();
            _backingArray[_count] = value;
            _count++;
        }

        public bool Insert(uint index, T value)
        {
            if (index > _count)
                return false;

            // insert at the end of list
            if (index == _count)
            {
                Add(value);
                return true;
            }

            // insert somewhere else
            EnsureSizeForNextItem();
            Array.Copy(_backingArray, index, _backingArray, index + 1, _count - index);
            _backingArray[index] = value;
            _count++;

            return true;
        }

        public bool Remove(T item)
        {
            var index = Array.IndexOf(_backingArray, item);
            
            // not present in the collection
            if (index == -1)
                return false;

            return RemoveAt((uint)index);
        }

        public bool RemoveAt(uint index)
        {
            if (index >= _count)
                return false;

            // present at the end of the collection
            if (index == _count - 1)
            {
                _count--;

                return true;
            }

            // present somewhere else
            Array.Copy(_backingArray, index + 1, _backingArray, index, _count - index - 1);
            _count--;

            return true;
        }

        public bool Contains(T value)
        {
            for (var i = 0; i < _count; i++)
            {
                var item = _backingArray[i];

                // EqualityComparer<T>.Default will use Object.Equals only if T doesn't implement IEquatable<T>.
                // If it does implement that interface, it uses IEquatable<T>.Equals.
                var comparer = EqualityComparer<T>.Default;
                if (comparer.Equals(item, value))
                    return true;
            }

            return false;
        }

        public void Clear()
        {
            _backingArray = new T[InitialArraySize];
            _count = 0;
        }

        private void EnsureSizeForNextItem()
        {
            if (_backingArray.Length != _count)
                return;

            // when casting positive fractional values, they are rounded down
            var newSize = (int)(_backingArray.Length * _growthFactor + _growthOffset);

            // copy content
            var temp = new T[newSize];
            Array.Copy(_backingArray, 0, temp, 0, _count);
            _backingArray = temp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>
        {
            private readonly CustomDynamicArray<T> _source;
            private int _currentPosition;

            public Enumerator(CustomDynamicArray<T> source)
            {
                _source = source;
                _currentPosition = -1;
            }

            public bool MoveNext()
            {
                return _source.Count > ++_currentPosition;
            }

            public void Reset()
            {
                _currentPosition = -1;
            }

            public T Current => PointsAtInvalidElement() ? default : _source[(uint)_currentPosition];

            object? IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            private bool PointsAtInvalidElement()
            {
                return _currentPosition == -1 || _currentPosition == _source.Count;
            }
        }
    }
}
