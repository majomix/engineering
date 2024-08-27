namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Pass through the list n times. In pass i, the array of length i-1 is sorted. Take the ith element and swap it leftwards to the correct place.
    ///
    /// Complexity:
    /// Time: O(n²)
    /// Space: O(1)
    /// </summary>
    internal class InsertionSortAlgorithm<TKey, TValue> : ISortingAlgorithm<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;

            for (var i = 0; i < input.Length; i++)
            {
                var keyToInsert = keySelector(input[i]);

                for (var j = i - 1; j >= 0; j--)
                {
                    var currentKey = keySelector(input[j]);

                    if (comparer.Compare(currentKey, keyToInsert) > 0)
                    {
                        (input[j], input[j + 1]) = (input[j + 1], input[j]);
                    }
                }
            }

            return input;
        }
    }
}
