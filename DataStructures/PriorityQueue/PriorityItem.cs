namespace DataStructures.PriorityQueue
{
    public class PriorityItem<TKey, TValue> : IComparable<PriorityItem<TKey, TValue>> where TKey : IComparable<TKey>
    {
        public TKey? Key { get; set; }
        public TValue? Value { get; set; }

        public int CompareTo(PriorityItem<TKey, TValue>? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Comparer<TKey?>.Default.Compare(Key, other.Key);
        }
    }
}
