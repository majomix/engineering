using DataStructures.DynamicArray;

namespace DataStructures.Stack
{
    /// <summary>
    /// Custom implementation of Last In First Out collection.
    /// </summary>
    public interface ICustomStack<T>
    {
        void Push(T item);
        T? Pop();
        T? Peek();
        void Clear();
    }

    public class CustomStack<T> : ICustomStack<T>
    {
        private CustomDynamicArray<T> _array;

        public uint Count { get; private set; }

        public CustomStack()
        {
            _array = new CustomDynamicArray<T>();
        }

        public CustomStack(uint capacity)
        {
            _array = new CustomDynamicArray<T>(capacity);
        }

        public void Push(T item)
        {
            _array.Add(item);
            Count++;
        }

        public T? Pop()
        {
            if (Count == 0)
                return default;

            var item = _array[--Count];

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
            Count = 0;
        }
    }
}
