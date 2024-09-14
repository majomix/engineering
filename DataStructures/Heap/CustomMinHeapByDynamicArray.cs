namespace DataStructures.Heap
{
    public class CustomMinHeapByDynamicArray<TKey, TValue> : AbstractHeapByDynamicArray<TKey, TValue>, ICustomMinHeap<TKey, TValue> where TKey : IComparable<TKey>
    {
        public CustomMinHeapByDynamicArray(uint capacity = InitialArraySize) : base(capacity) { }

        public TValue PeekMin()
        {
            return PeekRoot();
        }

        public TValue ExtractMin()
        {
            return ExtractRoot();
        }

        public void DecreaseKey(TValue value, TKey newKey)
        {
            var keyComparer = Comparer<TKey>.Default;
            var index = FindValueIndex(value);

            if (index == IndexOfDummyItem)
                throw new InvalidOperationException($"Value not in heap!");

            if (keyComparer.Compare(newKey, Array[index].Key) >= 0)
                throw new InvalidOperationException($"Key {newKey} is not smaller than previous key {Array[index].Key}!");

            Array[index].Key = newKey;
            SiftUp(index);
        }

        protected override bool NodesBreaksHeapProperty(TKey leftKey, TKey rightKey)
        {
            var comparer = Comparer<TKey>.Default;
            return comparer.Compare(leftKey, rightKey) > 0;
        }
    }
}
