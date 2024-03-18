namespace LeetCode.Easy
{
    /// <summary>
    /// https://leetcode.com/problems/rearrange-characters-to-make-target-string/
    /// </summary>
    public class RearrangeSubsequentCharacters
    {

        public int RearrangeCharacters(string s, string target)
        {
            var windowSize = target.Length;
            var targetStringLetterCounts = GetHistogram(target);
            var sourceStringLetterCounts = GetHistogram(s);
            var result = 0;

            var matchingPairs = sourceStringLetterCounts.Where(pair => targetStringLetterCounts.ContainsKey(pair.Key)).ToList();

            if (matchingPairs.Count == targetStringLetterCounts.Count)
            {
                var smallestOccurence = int.MaxValue;

                foreach (var pair in matchingPairs)
                {
                    var possibleOccurences = pair.Value / targetStringLetterCounts[pair.Key];
                    if (smallestOccurence > possibleOccurences)
                    {
                        smallestOccurence = possibleOccurences;
                    }
                }
                result = smallestOccurence;
            }

            return result;
        }

        private Dictionary<char, int> GetHistogram(string s)
        {
            var histogram = new Dictionary<char, int>();

            foreach (var letter in s)
            {
                if (histogram.ContainsKey(letter))
                {
                    histogram[letter]++;
                }
                else
                {
                    histogram[letter] = 1;
                }
            }

            return histogram;
        }

        public int RearrangeConsecutiveCharacters(string s, string target)
        {
            var windowSize = target.Length;
            var letterCounts = new Dictionary<char, int>();
            var windowLetterCounts = new Dictionary<char, int>();
            var result = 0;

            for (var currentLetter = 'a'; currentLetter <= 'z'; currentLetter++)
            {
                letterCounts[currentLetter] = 0;
                windowLetterCounts[currentLetter] = 0;
            }

            foreach (var letter in target)
            {
                letterCounts[letter]++;
            }

            for (var i = 0; i < s.Length; i++)
            {
                windowLetterCounts[s[i]]++;

                if (i > windowSize - 1)
                {
                    var characterToDropOut = s[i - 1];
                    windowLetterCounts[characterToDropOut]--;
                }

                if (i >= windowSize - 1)
                {
                    if (letterCounts.Keys.All(k => windowLetterCounts.ContainsKey(k) && windowLetterCounts[k] == letterCounts[k]))
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public static void TestCase()
        {
            var solution = new RearrangeSubsequentCharacters();

            var expectedTwo = solution.RearrangeCharacters("ilovecodingonleetcode", "code");
            var expectedOne = solution.RearrangeCharacters("abcba", "abc");
            var expectedOneAgain = solution.RearrangeCharacters("abbaccaddaeea", "aaaaa");
        }
    }
}
