namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Pass through the list n times. In each pass, swap neighbors rightwards if needed. After each pass, the end of the list will contain one more sorted element.
    ///
    /// Complexity:
    /// Time: O(n²)
    /// Space: O(1)
    /// </summary>
    internal class BubbleSortAlgorithm<TKey, TValue> : ISortingAlgorithm<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 1; j < input.Length; j++)
                {
                    var currentKey = keySelector(input[j]);
                    var previousKey = keySelector(input[j - 1]);

                    if (comparer.Compare(previousKey, currentKey) > 0)
                    {
                        (input[j], input[j - 1]) = (input[j - 1], input[j]);
                    }
                }
            }

            return input;
        }
    }
}
