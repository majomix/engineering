using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming
{
    /// <summary>
    /// Write a function allConstruct(target, wordBank) that accepts a target string and an array of strings.
    /// The function should return a 2D array containing all of the ways that the target can be constructed by concatenating elements of the wordBank array.
    /// Each element of the 2D array should represent one combination that constructs the target.
    /// You may reuse elements of wordBank as many times as needed. 
    /// </summary>
    public class Task8AllConstruct
    {
        private Dictionary<string, List<List<string>>> _memo = new Dictionary<string, List<List<string>>>();

        /// <summary>
        /// O(n^m) time
        /// O(n^m) space
        /// </summary>
        public List<List<string>> AllConstructTabulation(string target, List<string> wordBank)
        {
            var table = new List<List<string>>[target.Length + 1];
            for (var i = 0; i < table.Length; i++)
            {
                table[i] = new List<List<string>>();
            }

            table[0].Add(new List<string>()); // empty string can be constructed

            for (var i = 0; i < target.Length; i++)
            {
                foreach (var word in wordBank)
                {
                    if (i + word.Length <= target.Length)
                    {
                        var substring = target.Substring(i, word.Length);
                        if (substring == word)
                        {
                            foreach (var newCombination in table[i])
                            {
                                var newCombinationCopy = newCombination.ToList();
                                newCombinationCopy.Add(word);
                                table[i + word.Length].Add(newCombinationCopy);
                            }
                        }
                    }
                }
            }

            return table[target.Length];
        }

        /// <summary>
        /// O(n^m) time
        /// O(m) space
        /// </summary>
        public List<List<string>> AllConstructMemoization(string target, List<string> wordBank)
        {
            if (_memo.ContainsKey(target))
            {
                return _memo[target];
            }

            if (target == string.Empty)
                return new List<List<string>> { new List<string>() };

            var finalResult = new List<List<string>>();

            foreach (var word in wordBank)
            {
                if (target.StartsWith(word))
                {
                    var suffix = target.Substring(word.Length);
                    var suffixWays = AllConstructMemoization(suffix, wordBank);
                    foreach (var suffixWay in suffixWays)
                    {
                        suffixWay.Insert(0, word);
                        finalResult.Add(suffixWay);
                    }
                }
            }

            _memo[target] = finalResult;

            return finalResult;
        }

        /// <summary>
        /// O(n^m) time
        /// O(m) space
        /// </summary>
        public List<List<string>> AllConstruct(string target, List<string> wordBank)
        {
            if (target == string.Empty)
                return new List<List<string>> { new List<string>() };

            var finalResult = new List<List<string>>();

            foreach (var word in wordBank)
            {
                if (target.StartsWith(word))
                {
                    var suffix = target.Substring(word.Length);
                    var suffixWays = AllConstruct(suffix, wordBank);
                    foreach (var suffixWay in suffixWays)
                    {
                        suffixWay.Insert(0, word);
                        finalResult.Add(suffixWay);
                    }
                }
            }

            return finalResult;
        }

        public static void TestCase()
        {
            var canConstruct = new Task8AllConstruct();

            var shouldBe1_0 = canConstruct.AllConstruct("purple", new List<string> { "purp", "le" });
            var shouldBe2 = canConstruct.AllConstruct("purple", new List<string> { "purp", "p", "ur", "le", "purpl" });
            var shouldBe1 = canConstruct.AllConstruct("abcdef", new List<string> { "ab", "abc", "cd", "def", "abcd" });
            var shouldBe0 = canConstruct.AllConstruct("skateboard", new List<string> { "bo", "rd", "ate", "t", "ska", "sk", "boar" });
            var shouldBe4 = canConstruct.AllConstruct("enterapotentpot", new List<string> { "a", "p", "ent", "enter", "ot", "o", "t" });
            var shouldBe4Tab = canConstruct.AllConstructTabulation("enterapotentpot", new List<string> { "a", "p", "ent", "enter", "ot", "o", "t" });
            var shouldBe0_2 = canConstruct.AllConstructMemoization("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new List<string> { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" });
            var shouldBe0_2Tab = canConstruct.AllConstructTabulation("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new List<string> { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" });
        }
    }
}
