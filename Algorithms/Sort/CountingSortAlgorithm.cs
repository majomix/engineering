namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Build a histogram of key frequencies and use it to create map of keys to sorted indices.
    /// Iterate the collection backwards and place keys into their respective sorted positions in an output array.
    ///
    /// Complexity:
    /// Time: O(n + k) where k is the range of keys
    /// Space: O(n + k) where k is the range of keys
    /// </summary>
    internal class CountingSortAlgorithm<TValue> : ISortingAlgorithm<int, TValue>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, int> keySelector)
        {
            var maximumKeyValue = 10000;
            var negativeCompensation = maximumKeyValue / 2;
            var keysHistogram = new int[maximumKeyValue + 1];
            var output = new TValue[input.Length];

            foreach (var element in input)
            {
                var elementKey = keySelector(element) + negativeCompensation;
                keysHistogram[elementKey]++;
            }

            for (var i = 1; i <= maximumKeyValue; i++)
            {
                keysHistogram[i] += keysHistogram[i - 1];
            }

            for (var i = input.Length - 1; i >= 0; i--)
            {
                var elementKey = keySelector(input[i]) + negativeCompensation;
                keysHistogram[elementKey]--;
                output[keysHistogram[elementKey]] = input[i];
            }

            return output;
        }
    }
}
