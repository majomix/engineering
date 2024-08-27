namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Divide & Conquer. Use recursion to split arrays into smaller and smaller ones and sort those.
    /// Find pivot, partition and repeat on sub-arrays created by split point.
    ///
    /// Complexity:
    /// Time: Worst case O(n²), average O(n*log(n))
    /// Space: O(1)
    /// </summary>
    internal class QuickSortAlgorithm<TKey, TValue> : ISortingAlgorithm<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, TKey> keySelector)
        {
            return SortRecursivelyByHoare(input, 0, input.Length - 1, keySelector);
        }

        private TValue[] SortRecursivelyByHoare(TValue[] input, int leftIndex, int rightIndex, Func<TValue, TKey> keySelector)
        {
            if (leftIndex < 0 || rightIndex < 0 || leftIndex >= rightIndex)
                return input;

            var partitionIndex = PartitionHoare(input, leftIndex, rightIndex, keySelector);
            SortRecursivelyByHoare(input, leftIndex, partitionIndex, keySelector); // split point is included!
            SortRecursivelyByHoare(input, partitionIndex + 1, rightIndex, keySelector);

            return input;
        }

        /// <summary>
        /// Pivot is always the first element. Array is inspected with two pointers.
        /// On each side, move pointers as long as the boundary conditions hold: left side has values lower than pivot, right side higher than pivot.
        /// If not, swap. Once the pointers cross, split point is found.
        ///
        /// Worst case is sorted input and pivot as first element.
        /// </summary>
        private int PartitionHoare(TValue[] input, int leftIndex, int rightIndex, Func<TValue, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;

            var pivotKey = keySelector(input[leftIndex]);

            var leftPointer = leftIndex;
            var rightPointer = rightIndex;

            while (true)
            {
                while (leftPointer < input.Length && comparer.Compare(keySelector(input[leftPointer]), pivotKey) < 0)
                {
                    leftPointer++;
                }

                while (rightPointer >= 0 && comparer.Compare(keySelector(input[rightPointer]), pivotKey) > 0)
                {
                    rightPointer--;
                }

                if (leftPointer >= rightPointer)
                    return rightPointer;

                (input[leftPointer], input[rightPointer]) = (input[rightPointer], input[leftPointer]);
            }
        }

        private TValue[] SortRecursivelyByLomuto(TValue[] input, int leftIndex, int rightIndex, Func<TValue, TKey> keySelector)
        {
            // base case - empty array or 1 item is sorted
            if (input.Length < 2 || leftIndex < 0 || leftIndex >= rightIndex)
                return input;

            var partitionIndex = PartitionLomuto(input, leftIndex, rightIndex, keySelector);
            SortRecursivelyByLomuto(input, leftIndex, partitionIndex - 1, keySelector);
            SortRecursivelyByLomuto(input, partitionIndex + 1, rightIndex, keySelector);

            return input;
        }

        /// <summary>
        /// Pivot is always the last element. Whole array is searched from the left.
        /// Split point is being determined as the number of elements smaller than pivot.
        /// If element smaller than pivot is found, it is swapped with the current split point candidate and the split point is advanced.
        /// </summary>
        private int PartitionLomuto(TValue[] input, int leftIndex, int rightIndex, Func<TValue, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;

            var pivotKey = keySelector(input[rightIndex]);
            var splitPoint = leftIndex;

            for (var currentIndex = leftIndex; currentIndex < rightIndex; currentIndex++)
            {
                var currentKey = keySelector(input[currentIndex]);
                if (comparer.Compare(currentKey, pivotKey) <= 0)
                {
                    (input[splitPoint], input[currentIndex]) = (input[currentIndex], input[splitPoint]);

                    splitPoint++;
                }
            }

            (input[splitPoint], input[rightIndex]) = (input[rightIndex], input[splitPoint]);

            return splitPoint;
        }
    }
}
