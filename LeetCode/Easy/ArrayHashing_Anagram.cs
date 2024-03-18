using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Easy
{
    /// <summary>
    /// Given two strings s and t, return true if t is an anagram of s, and false otherwise.
    /// An Anagram is a word or phrase formed by rearranging the letters of a different word or phrase, typically using all the original letters exactly once.
    /// </summary>
    public class ArrayHashing_Anagram
    {
        /// <summary>
        /// time O(2s + t)
        /// space O(s + t)
        /// 
        /// another solution: sort them both, O(nlogn) time, O(1)
        /// </summary>
        public bool IsAnagram(string s, string t)
        {
            var hashMapSource = new Dictionary<char, int>();
            var hashMapTarget = new Dictionary<char, int>();

            foreach (var character in s)
            {
                if (!hashMapSource.ContainsKey(character))
                {
                    hashMapSource[character] = 1;
                }
                else
                {
                    hashMapSource[character]++;
                }
            }

            foreach (var character in t)
            {
                if (!hashMapTarget.ContainsKey(character))
                {
                    hashMapTarget[character] = 1;
                }
                else
                {
                    hashMapTarget[character]++;
                }
            }

            if (hashMapSource.Keys.Count != hashMapTarget.Keys.Count)
            {
                return false;
            }

            foreach (var key in hashMapSource.Keys)
            {
                if (!hashMapTarget.ContainsKey(key) || hashMapTarget[key] != hashMapSource[key])
                {
                    return false;
                }
            }

            return true;
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_Anagram();
            var shouldBeTrue = arrhash.IsAnagram("anagram", "nagaram");
            var shouldBeFalse = arrhash.IsAnagram("rat", "car");
        }
    }
}
