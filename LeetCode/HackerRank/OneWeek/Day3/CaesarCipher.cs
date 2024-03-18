using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.Easy;

namespace LeetCode.HackerRank.OneWeek.Day3
{
    public class CaesarCipher
    {
        /// <summary>
        /// PASSES 12/12
        ///
        /// Julius Caesar protected his confidential information by encrypting it using a cipher.
        /// Caesar's cipher shifts each letter by a number of letters. If the shift takes you past
        /// the end of the alphabet, just rotate back to the front of the alphabet. In the case of
        /// a rotation by 3, w, x, y and z would map to z, a, b and c.
        /// </summary>
        public static string caesarCipher(string s, int k)
        {
            var output = new StringBuilder();

            foreach (var letter in s)
            {
                if (letter is >= 'a' and <= 'z')
                {
                    output.Append((char)(((letter - 'a' + k) % ('z' - 'a' + 1)) + 'a'));
                }
                else if (letter is >= 'A' and <= 'Z')
                {
                    output.Append((char)(((letter - 'A' + k) % ('Z' - 'A' + 1)) + 'A'));
                }
                else
                {
                    output.Append(letter);
                }
            }

            return output.ToString();
        }

        public static void TestCase()
        {
            var res0 = caesarCipher("xyz", 1);

            var res = caesarCipher("There's-a-starman-waiting-in-the-sky", 3);
        }

        // 50 51 52 53
        // 50 51 50 51
    }
}
