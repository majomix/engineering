namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Create buckets according to the radix. Sort by digit using a stable algorithm, e.g. counting sort.
    ///
    /// Complexity:
    /// Time: O(n * k) where k is the key length
    /// Space: O(n)
    /// </summary>
    public class RadixSortAlgorithm<TValue> : ISortingAlgorithm<int, TValue>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, int> keySelector)
        {
            var max = input.Max(keySelector);

            for (int radix = 1; max / radix > 0; radix *= 10)
            {
                var currentRadix = radix;
                input = CountingSort(input, value => (keySelector(value) / currentRadix) % 10);
            }

            return input;
        }

        private TValue[] CountingSort(TValue[] input, Func<TValue, int> keySelector)
        {
            var keysHistogram = new int[10];
            var output = new TValue[input.Length];

            foreach (var element in input)
            {
                var elementKey = keySelector(element);
                keysHistogram[elementKey]++;
            }

            for (var i = 1; i <= 9; i++)
            {
                keysHistogram[i] += keysHistogram[i - 1];
            }

            for (var i = input.Length - 1; i >= 0; i--)
            {
                var elementKey = keySelector(input[i]);
                keysHistogram[elementKey]--;
                output[keysHistogram[elementKey]] = input[i];
            }

            return output;
        }
    }
}
