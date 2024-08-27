namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Pass through the list n times. In each pass, find the minimum and swap it with element at the index of the current pass.
    ///
    /// Complexity:
    /// Time: O(n²)
    /// Space: O(1)
    /// </summary>
    internal class SelectionSortAlgorithm<TKey, TValue> : ISortingAlgorithm<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;

            for (var i = 0; i < input.Length; i++)
            {
                var smallestIndex = i;

                for (var j = i; j < input.Length; j++)
                {
                    var currentKey = keySelector(input[j]);
                    var smallestKey = keySelector(input[smallestIndex]);

                    if (comparer.Compare(smallestKey, currentKey) > 0)
                    {
                        smallestIndex = j;
                    }
                }

                (input[i], input[smallestIndex]) = (input[smallestIndex], input[i]);
            }

            return input;
        }
    }
}
