using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// https://www.code-recipe.com/post/longest-substring-without-repeating-characters
    /// </summary>
    public class LongestSubstringWithoutRepeatingCharacters
    {
        public int LengthOfLongestSubstring(string s)
        {
            HashSet<char> characters = new HashSet<char>();

            var maximum = 0;
            var totalMaximum = 1;

            foreach (var character in s)
            {
                maximum++;
                if (characters.Contains(character))
                {
                    if (totalMaximum < maximum)
                    {
                        totalMaximum = maximum;
                    }
                }
                characters.Add(character);
            }

            return totalMaximum;
        }

        public static void TestCase()
        {
            var sub = new LongestSubstringWithoutRepeatingCharacters();
            var shouldBe3Again = sub.LengthOfLongestSubstring("pwwkew");
            var shouldBe3Again2 = sub.LengthOfLongestSubstring("awabcdefgwa");

            var shouldBe3 = sub.LengthOfLongestSubstring("abcabcbb");
            var shouldBe1 = sub.LengthOfLongestSubstring("bbbbb");
        }
    }
}
