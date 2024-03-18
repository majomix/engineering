using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day3
{
    public class PalindromeIndex
    {
        /// <summary>
        /// PASSES 15/15
        ///
        /// Given a string of lowercase letters in the range ascii[a-z], determine the index of a character that can be removed to make the string a palindrome.
        /// There may be more than one solution, but any will do. If the word is already a palindrome or there is no solution, return -1.
        /// Otherwise, return the index of a character to remove. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int palindromeIndex(string s)
        {
            for (var i = 0; i < s.Length / 2; i++)
            {
                var indexFront = i;
                var indexBack = s.Length - i - 1;

                if (s[indexFront] != s[indexBack])
                {
                    var candidate1 = s.Substring(0, indexFront) + s.Substring(indexFront + 1, s.Length - indexFront - 1);
                    if (PalindromeIndexInternal(candidate1))
                    {
                        return indexFront;
                    }

                    var candidate2 = s.Substring(0, indexBack) + s.Substring(indexBack + 1, s.Length - indexBack - 1);
                    if (PalindromeIndexInternal(candidate2))
                    {
                        return indexBack;
                    }
                }
            }

            return -1;
        }

        private static bool PalindromeIndexInternal(string s)
        {
            for (var i = 0; i < s.Length / 2; i++)
            {
                var indexFront = i;
                var indexBack = s.Length - i - 1;

                if (s[indexFront] != s[indexBack])
                {
                    return false;
                }
            }

            return true;
        }

        public static void TestCase()
        {
            var shouldBeMinus1 = palindromeIndex("aaa");
            var shouldBe3 = palindromeIndex("aaab");
            var shouldBe0 = palindromeIndex("baa");
        }
    }
}
