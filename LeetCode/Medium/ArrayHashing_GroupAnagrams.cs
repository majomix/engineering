using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given an array of strings strs, group the anagrams together. You can return the answer in any order.
    /// Constraints:
    /// 1 <= strs.length <= 10^4
    /// 0 <= strs[i].length <= 100
    /// strs[i] consists of lowercase English letters
    /// </summary>
    internal class ArrayHashing_GroupAnagrams
    {
        // time O(m*n*26)
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var dict = new Dictionary<string, IList<string>>();

            foreach (var str in strs)
            {
                var charCounts = new int[26];
                
                foreach (var character in str)
                {
                    var index = character - 'a';
                    charCounts[index]++;
                }

                var key = string.Join(" ", charCounts);
                
                if (!dict.ContainsKey(key))
                {
                    dict[key] = new List<string>();
                }

                dict[key].Add(str);
            }

            return dict.Values.ToList();
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_GroupAnagrams();
            var shouldBeCountOf3 = arrhash.GroupAnagrams(new string[] { "eat", "tea", "tan", "ate", "nat", "bat" });
            var shouldBeCountOf1WithEmptyString = arrhash.GroupAnagrams(new string[] { "" });
            var shouldBeCountOf1WithA = arrhash.GroupAnagrams(new string[] { "a" });
        }
    }
}
