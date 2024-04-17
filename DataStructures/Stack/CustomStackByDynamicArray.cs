using DataStructures.DynamicArray;

namespace DataStructures.Stack
{
    public class CustomStackByDynamicArray<T> : ICustomStack<T>
    {
        private const int InitialArraySize = 4;

        private readonly CustomDynamicArray<T> _array;

        public uint Count => _array.Count;

        public CustomStackByDynamicArray(uint capacity = InitialArraySize)
        {
            _array = new CustomDynamicArray<T>(capacity);
        }

        public void Push(T item)
        {
            _array.Add(item);
        }

        public T? Pop()
        {
            if (Count == 0)
                return default;

            var indexToPop = Count - 1;
            var item = _array[indexToPop];
            _array.RemoveAt(indexToPop);

            return item;
        }

        public T? Peek()
        {
            if (Count == 0)
                return default;

            return _array[Count - 1];
        }

        public void Clear()
        {
            _array.Clear();
        }
    }
}
