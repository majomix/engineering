namespace Algorithms.Sort
{
    internal class CountingSortAlgorithm<TValue> : ISortingAlgorithm<int, TValue>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, int> keySelector)
        {
            var maximumKeyValue = 10000;
            var negativeCompensation = maximumKeyValue / 2;
            var count = new int[maximumKeyValue + 1];
            var output = new TValue[input.Length];

            foreach (var element in input)
            {
                var elementKey = keySelector(element) + negativeCompensation;
                count[elementKey]++;
            }

            for (var i = 1; i <= maximumKeyValue; i++)
            {
                count[i] += count[i - 1];
            }

            for (var i = input.Length - 1; i >= 0; i--)
            {
                var elementKey = keySelector(input[i]) + negativeCompensation;
                count[elementKey]--;
                output[count[elementKey]] = input[i];
            }

            return output;
        }
    }
}
