namespace DataStructures.Heap
{
    public class HeapNode<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TKey? Key { get; set; }
        public TValue? Value { get; set; }
    }
}
