namespace DataStructures.Heap
{
    public class CustomMaxHeapByDynamicArray<TKey, TValue> : AbstractHeapByDynamicArray<TKey, TValue>, ICustomMaxHeap<TKey, TValue> where TKey : IComparable<TKey>
    {
        public CustomMaxHeapByDynamicArray(uint capacity = InitialArraySize) : base(capacity) { }

        public TValue PeekMax()
        {
            return PeekRoot();
        }

        public TValue ExtractMax()
        {
            return ExtractRoot();
        }

        public void IncreaseKey(TValue value, TKey newKey)
        {
            var keyComparer = Comparer<TKey>.Default;
            var index = FindValueIndex(value);

            if (index == IndexOfDummyItem)
                throw new InvalidOperationException($"Value not in heap!");

            if (keyComparer.Compare(newKey, Array[index].Key) <= 0)
                throw new InvalidOperationException($"Key {newKey} is not larger than previous key {Array[index].Key}!");

            Array[index].Key = newKey;
            SiftUp(index);
        }

        protected override bool NodesBreaksHeapProperty(TKey leftKey, TKey rightKey)
        {
            var comparer = Comparer<TKey>.Default;
            return comparer.Compare(leftKey, rightKey) < 0;
        }
    }
}
