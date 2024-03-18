using System.Collections.Generic;

namespace DynamicProgramming
{
    /// <summary>
    /// Write a function countConstruct(target, wordBank) that accepts a target string and an array of strings.
    /// The function should return the number of ways that the target can be constructed by concatenating elements of the wordBank array.
    /// You may reuse elements of wordBank as many times as needed. 
    /// </summary>
    public class Task7CountConstruct
    {
        private Dictionary<string, int> _memo = new Dictionary<string, int>();

        public int CountConstructTabulation(string target, List<string> wordBank)
        {
            var table = new int[target.Length + 1];

            table[0] = 1; // empty string can be constructed

            for (var i = 0; i < target.Length; i++)
            {
                if (table[i] != 0)
                {
                    foreach (var word in wordBank)
                    {
                        if (i + word.Length <= target.Length)
                        {
                            var substring = target.Substring(i, word.Length);
                            if (substring == word)
                            {
                                table[i + word.Length] += table[i];
                            }
                        }
                    }
                }
            }

            return table[target.Length];
        }

        public int CountConstructMemoization(string target, List<string> wordBank)
        {
            if (_memo.ContainsKey(target))
            {
                return _memo[target];
            }

            if (target == string.Empty)
                return 1;

            var finalResult = 0;

            foreach (var word in wordBank)
            {
                if (target.StartsWith(word))
                {
                    var suffix = target.Substring(word.Length);
                    finalResult += CountConstructMemoization(suffix, wordBank);
                }
            }

            _memo[target] = finalResult;

            return finalResult;
        }

        public int CountConstruct(string target, List<string> wordBank)
        {
            if (target == string.Empty)
                return 1;

            var finalResult = 0;

            foreach (var word in wordBank)
            {
                if (target.StartsWith(word))
                {
                    var suffix = target.Substring(word.Length);
                    finalResult += CountConstruct(suffix, wordBank);
                }
            }

            return finalResult;
        }

        public static void TestCase()
        {
            var canConstruct = new Task7CountConstruct();
            
            var shouldBe2 = canConstruct.CountConstruct("purple", new List<string> { "purp", "p", "ur", "le", "purpl" });
            var shouldBe1 = canConstruct.CountConstruct("abcdef", new List<string> { "ab", "abc", "cd", "def", "abcd" });
            var shouldBe0 = canConstruct.CountConstruct("skateboard", new List<string> { "bo", "rd", "ate", "t", "ska", "sk", "boar" });
            var shouldBe4 = canConstruct.CountConstruct("enterapotentpot", new List<string> { "a", "p", "ent", "enter", "ot", "o", "t" });
            var shouldBe4Tab = canConstruct.CountConstructTabulation("enterapotentpot", new List<string> { "a", "p", "ent", "enter", "ot", "o", "t" });
            var shouldBe0_2 = canConstruct.CountConstructMemoization("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new List<string> { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" });
            var shouldBe0_2Tab = canConstruct.CountConstructTabulation("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new List<string> { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" });
        }
    }
}
