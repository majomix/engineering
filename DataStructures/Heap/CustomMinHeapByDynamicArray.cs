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

        protected override bool NodesBreaksHeapProperty(TKey leftKey, TKey rightKey)
        {
            var comparer = Comparer<TKey>.Default;
            return comparer.Compare(leftKey, rightKey) > 0;
        }
    }
}
