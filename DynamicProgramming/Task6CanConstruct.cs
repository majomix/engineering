using System.Collections.Generic;

namespace DynamicProgramming
{
    /// <summary>
    /// Write a function canConstruct(target, wordBank) that accepts a string and an array of strings.
    /// The function should return a boolean indicating whether or not the target can be constructed by concatening elements of the wordBank array.
    /// You may reuse elements of wordBank as many times as needed.
    /// </summary>
    public class Task6CanConstruct
    {
        private Dictionary<string, bool> _memo = new Dictionary<string, bool>();

        /// <summary>
        /// m = target
        /// n = wordBank.Count
        /// O(m^2*n) time
        /// O(m) space
        /// </summary>
        public bool CanConstructTabulation(string target, List<string> wordBank)
        {
            var table = new bool[target.Length + 1];

            table[0] = true; // empty string can be constructed

            for (var i = 0; i < target.Length; i++)
            {
                if (table[i])
                {
                    foreach (var word in wordBank)
                    {
                        if (i + word.Length <= target.Length)
                        {
                            var substring = target.Substring(i, word.Length);
                            if (substring == word)
                            {
                                table[i + word.Length] = true;
                            }
                        }
                    }
                }
            }

            return table[target.Length];
        }

        /// <summary>
        /// time O(n * m^2)
        /// </summary>
        public bool CanConstructMemoization(string target, List<string> wordBank)
        {
            if (_memo.ContainsKey(target))
            {
                return _memo[target];
            }

            if (target == string.Empty)
                return true;

            foreach (var word in wordBank)
            {
                if (target.StartsWith(word))
                {
                    var suffix = target.Substring(word.Length);
                    if (CanConstructMemoization(suffix, wordBank))
                    {
                        _memo[target] = true;
                        return true;
                    }
                }
            }

            _memo[target] = false;

            return false;
        }

        /// <summary>
        /// Start from the whole word, remove prefixes until the string is empty. Do not remove elements from the middle.
        /// m = target.Length
        /// n = wordBank.Count
        /// height = m
        /// width = worst-case all entries from word bank can be used
        /// O(n^m * m) time - n^m is the number of recursive nodes, m is the cost for substring
        /// O(m^2) space - m is the height of tree and other m is memory for substring
        /// </summary>
        public bool CanConstruct(string target, List<string> wordBank)
        {
            if (target == string.Empty)
                return true;

            foreach (var word in wordBank)
            {
                if (target.StartsWith(word))
                {
                    var suffix = target.Substring(word.Length);
                    if (CanConstruct(suffix, wordBank))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void TestCase()
        {
            var canConstruct = new Task6CanConstruct();

            var true1 = canConstruct.CanConstruct("abcdef", new List<string> { "ab", "abc", "cd", "def", "abcd" });
            var false1 = canConstruct.CanConstruct("skateboard", new List<string> { "bo", "rd", "ate", "t", "ska", "sk", "boar" });
            var true2 = canConstruct.CanConstruct("enterapotentpot", new List<string> { "a", "p", "ent", "enter", "ot", "o", "t" });
            var trueTab = canConstruct.CanConstructTabulation("enterapotentpot", new List<string> { "a", "p", "ent", "enter", "ot", "o", "t" });
            var false2 = canConstruct.CanConstructMemoization("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new List<string> { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" });
            var false2Tab = canConstruct.CanConstructTabulation("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new List<string> { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" });
        }
    }
}
