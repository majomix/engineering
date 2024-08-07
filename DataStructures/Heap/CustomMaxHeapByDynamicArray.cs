namespace DataStructures.Heap
{
    internal class CustomMaxHeapByDynamicArray<TKey, TValue> : AbstractHeapByDynamicArray<TKey, TValue>, ICustomMaxHeap<TKey, TValue> where TKey : IComparable<TKey>
    {
        public CustomMaxHeapByDynamicArray(uint capacity = InitialArraySize) : base(capacity) { }

        public TValue ExtractMax()
        {
            return ExtractRoot();
        }

        protected override bool NodesBreaksHeapProperty(TKey leftKey, TKey rightKey)
        {
            var comparer = Comparer<TKey>.Default;
            return comparer.Compare(leftKey, rightKey) < 0;
        }
    }
}
