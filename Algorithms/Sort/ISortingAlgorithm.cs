namespace Algorithms.Sort;

public interface ISortingAlgorithm<in TKey, TValue> where TKey : IComparable<TKey>
{
    TValue[] Sort(TValue[] input, Func<TValue, TKey> keySelector);
}
