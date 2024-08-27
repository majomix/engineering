namespace Algorithms.Sort
{
    /// <summary>
    /// Idea:
    /// Perform gapped insertion sort with ever decreasing gap. After each pass, the list will be gap-sorted. This achieves faster spread between large and small values.
    ///
    /// Complexity:
    /// Time: Depends on gaps. Worst case O(n²) for Shell gaps to O(n^(3/2)) for Knuth gaps. Average reaching O(n*log^2(n)).
    /// Space: O(1)
    /// </summary>
    internal class ShellSortAlgorithm<TKey, TValue> : ISortingAlgorithm<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TValue[] Sort(TValue[] input, Func<TValue, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;

            var gaps = GetGapsByKnuth(input.Length);
            
            foreach (var gap in gaps)
            {
                // Iteration starts at gap because insertion sort always assumes one-item list as sorted.
                // Thus by setting i to gap there are already multiple one-item sorted lists.
                for (var i = gap; i < input.Length; i++)
                {
                    var keyToInsert = keySelector(input[i]);

                    // For each sub-array, we start inspecting items from index 1 to the last, comparing it to previous indices until 0.
                    // This maintains the rule of insertion sort that after ith pass i items are left-sorted.
                    // For gap > 1 j and j+1 are different sub-arrays. This means the insertion sort is performed in an interleaved way.
                    for (var j = i; j >= gap; j -= gap)
                    {
                        var keyAtPreviousGapIndex = keySelector(input[j - gap]);

                        if (comparer.Compare(keyAtPreviousGapIndex, keyToInsert) > 0)
                        {
                            (input[j], input[j - gap]) = (input[j - gap], input[j]);
                        }
                    }
                }
            }

            return input;
        }

        private List<int> GetGapsByShell(int length)
        {
            var steps = new List<int>();

            while (length > 1)
            {
                length /= 2;

                steps.Add(length);
            }

            return steps;
        }

        private List<int> GetGapsByLazaurus(int length)
        {
            var steps = new List<int>();

            length /= 2;

            while (length > 1)
            {
                length /= length;

                steps.Add(2 * length + 1);
            }

            steps.Add(1);

            return steps;
        }

        private List<int> GetGapsByKnuth(int length)
        {
            var steps = new List<int>();

            var step = 1;

            while (step < length)
            {
                steps.Add(step);
                step = step * 3 + 1;
            }

            steps.Reverse();

            return steps;
        }
    }
}
