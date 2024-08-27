namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Divide & Conquer. Use recursion to split arrays into one-element arrays. On the way back, merge sorted arrays using extra memory.
    ///
    /// Optimizations:
    /// Only allocate working space once.
    /// Once the first list is merged, the remainder of the second list is already there.
    ///
    /// Complexity:
    /// Time: O(n*log(n))
    /// Space: O(n)
    /// </summary>
    internal class MergeSortAlgorithm<TKey, TValue> : ISortingAlgorithm<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, TKey> keySelector)
        {
            var temporaryArray = new TValue[input.Length];

            MergeSortRecursively(input, temporaryArray, 0, input.Length - 1, keySelector);

            return input;
        }

        private void MergeSortRecursively(TValue[] input, TValue[] temporaryArray, int leftIndex, int rightIndex, Func<TValue, TKey> keySelector)
        {
            if (leftIndex >= rightIndex)
                return;

            int middleIndex = leftIndex + (rightIndex - leftIndex) / 2;
            
            MergeSortRecursively(input, temporaryArray, leftIndex, middleIndex, keySelector);
            MergeSortRecursively(input, temporaryArray, middleIndex + 1, rightIndex, keySelector);
            MergeArrays(input, temporaryArray, leftIndex, middleIndex, rightIndex, keySelector);
        }

        /// <summary>
        /// Merge pre-sorted arrays starting with one-item arrays at indices 0 and 1.
        /// </summary>
        private void MergeArrays(TValue[] input, TValue[] temporaryArray, int leftIndex, int middleIndex, int rightIndex, Func<TValue, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;

            // copy both halves into extra space
            for (var i = leftIndex; i <= rightIndex; i++)
            {
                temporaryArray[i] = input[i];
            }

            var tempLeftIndex = leftIndex;
            var tempRightIndex = middleIndex + 1;
            var writeIndex = leftIndex;

            // place element either from the left array or the right array in position given by write pointer
            while (tempLeftIndex <= middleIndex && tempRightIndex <= rightIndex)
            {
                var currentLeftKey = keySelector(temporaryArray[tempLeftIndex]);
                var currentRightKey = keySelector(temporaryArray[tempRightIndex]);

                if (comparer.Compare(currentLeftKey, currentRightKey) <= 0)
                {
                    input[writeIndex] = temporaryArray[tempLeftIndex];
                    tempLeftIndex++;
                }
                else
                {
                    input[writeIndex] = temporaryArray[tempRightIndex];
                    tempRightIndex++;
                }

                writeIndex++;
            }

            // if there are some items left in the right array, they're in the correct position
            // if there are some items left in the left array, copy them using write pointer
            var remainingLeftItems = middleIndex - tempLeftIndex;
            for (var i = 0; i <= remainingLeftItems; i++)
            {
                input[writeIndex++] = temporaryArray[tempLeftIndex++];
            }
        }
    }
}
